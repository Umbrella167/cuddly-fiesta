using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using Google.Protobuf;
using static Packet; // Assuming Packet.cs contains SSL_WrapperPacket, SSL_DetectionBall, SSL_DetectionRobot, etc.
using System.Linq; // Required for LINQ operations like Average

// Ensure your Param class exists and contains necessary constants (as described in the previous response)
// Ensure your Connect_Gate class exists and contains GAME_MODE (as described in the previous response)


public class MulticastReceiver : MonoBehaviour
{
    private Socket multicastSocket;

    // Buffer to hold the latest packet received for each camera ID
    // Protected by a lock for thread safety
    private SSL_WrapperPacket[] latestCameraPackets = new SSL_WrapperPacket[4];
    private readonly object packetLock = new object(); // Lock object for latestCameraPackets

    public string MCAST_GRP = Param.MCAST_GRP;
    public int MCAST_PORT;

    private const int NUM_ROBOTS = 16; // 每个队伍的机器人数量
    private const int DISAPPEARANCE_THRESHOLD = 30; // 消失次数阈值

    // --- EMA Smoothing Factor ---
    [Range(0.01f, 1.0f)] // Allow tuning in the inspector
    public float smoothingFactor = 0.5f; // Alpha value for EMA. Smaller = More smoothing, More lag.

    public GameObject ball_obj = null;

    // 存储机器人GameObject的字典
    private Dictionary<string, GameObject> blueRobots = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> yellowRobots = new Dictionary<string, GameObject>();

    // 存储机器人消失次数的字典
    private Dictionary<string, int> blueDisappearanceCounts = new Dictionary<string, int>();
    private Dictionary<string, int> yellowDisappearanceCounts = new Dictionary<string, int>();

    // --- Store Previous Filtered States for EMA ---
    private Vector3 filteredBallPosition = Vector3.zero;
    private float filteredBallOrientation = 0f;
    private bool ballFilteredInitialized = false; // Flag to initialize the first filtered value

    private Dictionary<uint, Vector3> filteredBlueRobotPositions = new Dictionary<uint, Vector3>();
    private Dictionary<uint, float> filteredBlueRobotOrientations = new Dictionary<uint, float>();
    private Dictionary<uint, bool> blueRobotFilteredInitialized = new Dictionary<uint, bool>();

    private Dictionary<uint, Vector3> filteredYellowRobotPositions = new Dictionary<uint, Vector3>();
    private Dictionary<uint, float> filteredYellowRobotOrientations = new Dictionary<uint, float>();
    private Dictionary<uint, bool> yellowRobotFilteredInitialized = new Dictionary<uint, bool>();


    // Thread for receiving data
    private Thread receiveThread;
    private bool isRunning = true; // Flag to control the receive thread loop

    void Start()
    {
        MCAST_GRP = Param.MCAST_GRP;
        MCAST_PORT = (Connect_Gate.GAME_MODE == Param.REAL) ? Param.MCAST_PORT_REAL : Param.MCAST_PORT_SIM;

        InitializeRobots(blueRobots, "blue_robot", blueDisappearanceCounts);
        InitializeRobots(yellowRobots, "yellow_robot", yellowDisappearanceCounts);

        // --- Initialize EMA state dictionaries for robots ---
        InitializeRobotFilteredStates(blueRobotFilteredInitialized);
        InitializeRobotFilteredStates(yellowRobotFilteredInitialized);


        ball_obj = GameObject.Find("Ball");
        if (ball_obj == null)
        {
            Debug.LogError("Ball GameObject not found! Make sure there is a GameObject named 'Ball' in the scene.");
            // If ball is missing, the script can't update it, but it can continue for robots
        }

        // Setup UDP socket
        try
        {
            multicastSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            multicastSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            IPEndPoint localEP = new IPEndPoint(IPAddress.Any, MCAST_PORT);
            multicastSocket.Bind(localEP);

            IPAddress multicastAddress = IPAddress.Parse(MCAST_GRP);
            MulticastOption multicastOption = new MulticastOption(multicastAddress, IPAddress.Any);
            multicastSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, multicastOption);

            isRunning = true;
            receiveThread = new Thread(ReceiveData);
            receiveThread.IsBackground = true;
            receiveThread.Start();

            Debug.Log($"Multicast Receiver started on {MCAST_GRP}:{MCAST_PORT}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to start Multicast Receiver: {ex.Message}");
            if (multicastSocket != null)
            {
                multicastSocket.Close();
                multicastSocket = null;
            }
            isRunning = false;
        }
    }

    // Initialize robots, their disappearance counts, and filtered state initialization flags
    private void InitializeRobots(Dictionary<string, GameObject> robotDict, string robotPrefix, Dictionary<string, int> disappearanceCounts)
    {
        for (uint i = 0; i < NUM_ROBOTS; i++)
        {
            string obj_name = robotPrefix + i.ToString();
            GameObject robot = GameObject.Find(obj_name);
            if (robot != null)
            {
                robotDict.Add(obj_name, robot);
                disappearanceCounts.Add(obj_name, 0);
            }
            else
            {
                Debug.LogError($"Robot GameObject not found during initialization: {obj_name}");
                disappearanceCounts.Add(obj_name, DISAPPEARANCE_THRESHOLD + 1);
            }
        }
    }

    // Initialize the EMA state initialization flags for robots
    private void InitializeRobotFilteredStates(Dictionary<uint, bool> initializedDict)
    {
        for (uint i = 0; i < NUM_ROBOTS; i++)
        {
            initializedDict[i] = false; // Not initialized yet
        }
    }

    void Update()
    {
        // --- Aggregation Phase ---
        List<SSL_DetectionBall> currentBallDetections = new List<SSL_DetectionBall>();
        Dictionary<uint, List<SSL_DetectionRobot>> currentBlueRobotDetections = new Dictionary<uint, List<SSL_DetectionRobot>>();
        Dictionary<uint, List<SSL_DetectionRobot>> currentYellowRobotDetections = new Dictionary<uint, List<SSL_DetectionRobot>>();

        lock (packetLock)
        {
            for (int i = 0; i < latestCameraPackets.Length; i++)
            {
                if (latestCameraPackets[i] != null && latestCameraPackets[i].Detection != null)
                {
                    currentBallDetections.AddRange(latestCameraPackets[i].Detection.Balls);

                    foreach (var robot in latestCameraPackets[i].Detection.RobotsBlue)
                    {
                        if (!currentBlueRobotDetections.ContainsKey(robot.RobotId))
                        {
                            currentBlueRobotDetections[robot.RobotId] = new List<SSL_DetectionRobot>();
                        }
                        currentBlueRobotDetections[robot.RobotId].Add(robot);
                    }

                    foreach (var robot in latestCameraPackets[i].Detection.RobotsYellow)
                    {
                        if (!currentYellowRobotDetections.ContainsKey(robot.RobotId))
                        {
                            currentYellowRobotDetections[robot.RobotId] = new List<SSL_DetectionRobot>();
                        }
                        currentYellowRobotDetections[robot.RobotId].Add(robot);
                    }

                    latestCameraPackets[i] = null; // Mark as processed
                }
            }
        } // Release lock

        // --- Processing and EMA Filtering Phase ---

        // Process Ball
        if (ball_obj != null) // Only process if ball GameObject exists
        {
            if (currentBallDetections.Count > 0)
            {
                // Calculate average measured position for this frame
                float measuredAvgX = currentBallDetections.Average(b => b.X) * Param.SCALE_COORDINATE;
                float measuredAvgY = currentBallDetections.Average(b => b.Y) * Param.SCALE_COORDINATE;
                Vector3 currentMeasuredBallPosition = new UnityEngine.Vector3(measuredAvgX, Param.BALL_Z, measuredAvgY);

                // Simple average orientation isn't available for the ball in the standard packet format,
                // so we only filter position.

                // Apply EMA
                if (!ballFilteredInitialized)
                {
                    // First detection, initialize filtered position directly
                    filteredBallPosition = currentMeasuredBallPosition;
                    ballFilteredInitialized = true;
                }
                else
                {
                    // Apply EMA formula
                    filteredBallPosition = Vector3.Lerp(filteredBallPosition, currentMeasuredBallPosition, smoothingFactor);
                    // Note: Vector3.Lerp(a, b, t) calculates a + (b - a) * t, which is equivalent to
                    // a * (1 - t) + b * t. So, previous * (1 - alpha) + current * alpha.
                    // Our formula is alpha * current + (1-alpha) * previous.
                    // We can achieve this with Lerp(previous, current, alpha)
                }

                // Apply the filtered position to the GameObject
                ball_obj.transform.position = filteredBallPosition;

                // Note: Ball disappearance is not handled here. You would need separate logic
                // to track if the ball is not detected for multiple frames.

            }
            // If ball is NOT detected in this frame's aggregation, we don't update its filtered position.
            // The ball might still be visible at its last filtered position until it's detected again
            // or a separate disappearance logic moves it.
        }


        // Process Blue Robots
        HashSet<uint> detectedBlueIdsThisFrame = new HashSet<uint>(currentBlueRobotDetections.Keys);
        for (uint i = 0; i < NUM_ROBOTS; i++) // Iterate through all possible robot IDs
        {
            string obj_name = "blue_robot" + i.ToString();

            if (detectedBlueIdsThisFrame.Contains(i)) // If robot i was detected by at least one camera
            {
                List<SSL_DetectionRobot> detections = currentBlueRobotDetections[i];

                // Calculate average measured position for this frame
                float measuredAvgX = detections.Average(r => r.X) * Param.SCALE_COORDINATE;
                float measuredAvgY = detections.Average(r => r.Y) * Param.SCALE_COORDINATE;
                Vector3 currentMeasuredPosition = new UnityEngine.Vector3(measuredAvgX, Param.ROBOT_Z, measuredAvgY);

                // Calculate average measured orientation for this frame
                float sumSin = detections.Sum(r => Mathf.Sin(r.Orientation));
                float sumCos = detections.Sum(r => Mathf.Cos(r.Orientation));
                float measuredAvgOrientationRad = Mathf.Atan2(sumSin, sumCos); // Radian

                // Apply EMA to Position
                if (!blueRobotFilteredInitialized.ContainsKey(i) || !blueRobotFilteredInitialized[i])
                {
                    // First detection, initialize filtered position
                    filteredBlueRobotPositions[i] = currentMeasuredPosition;
                    blueRobotFilteredInitialized[i] = true;
                }
                else
                {
                    // Apply EMA
                    filteredBlueRobotPositions[i] = Vector3.Lerp(filteredBlueRobotPositions[i], currentMeasuredPosition, smoothingFactor);
                }

                // Apply EMA to Orientation (needs careful handling)
                // Convert current measured angle and previous filtered angle to vectors
                Vector2 measuredVec = new Vector2(Mathf.Cos(measuredAvgOrientationRad), Mathf.Sin(measuredAvgOrientationRad));
                Vector2 prevFilteredVec = new Vector2(Mathf.Cos(filteredBlueRobotOrientations.ContainsKey(i) ? filteredBlueRobotOrientations[i] : measuredAvgOrientationRad),
                                                    Mathf.Sin(filteredBlueRobotOrientations.ContainsKey(i) ? filteredBlueRobotOrientations[i] : measuredAvgOrientationRad));

                Vector2 newFilteredVec = Vector2.Lerp(prevFilteredVec, measuredVec, smoothingFactor); // Apply EMA in vector form
                filteredBlueRobotOrientations[i] = Mathf.Atan2(newFilteredVec.y, newFilteredVec.x); // Convert back to angle (radian)

                // Convert filtered orientation (radian) to Unity rotation (degrees)
                float filteredAvgDir = -(filteredBlueRobotOrientations[i] + Mathf.PI / 2) * Mathf.Rad2Deg;

                // Apply the filtered state to the GameObject
                string robotObjName = "blue_robot" + i.ToString();
                if (blueRobots.ContainsKey(robotObjName) && blueRobots[robotObjName] != null)
                {
                    blueRobots[robotObjName].transform.position = filteredBlueRobotPositions[i];
                    blueRobots[robotObjName].transform.rotation = UnityEngine.Quaternion.Euler(0, filteredAvgDir, 0);
                    blueDisappearanceCounts[robotObjName] = 0; // Reset disappearance counter
                }
                else
                {
                    Debug.LogWarning($"Blue Robot GameObject not found in dictionary for ID {i} during update application: {robotObjName}");
                }
            }
            else // If robot i was NOT detected by *any* camera this frame
            {
                string robotObjName = "blue_robot" + i.ToString();
                if (blueDisappearanceCounts.ContainsKey(robotObjName))
                {
                    blueDisappearanceCounts[robotObjName]++; // Increase disappearance counter
                    if (blueDisappearanceCounts[robotObjName] > DISAPPEARANCE_THRESHOLD)
                    {
                        // If the robot has been missing for too many frames, move it out of sight
                        if (blueRobots.ContainsKey(robotObjName) && blueRobots[robotObjName] != null)
                        {
                            blueRobots[robotObjName].transform.position = new UnityEngine.Vector3(0, Param.OUT_OF_SIGHT_Y, 0);
                            // Optionally, you could also reset the filtered state initialization
                            // blueRobotFilteredInitialized[i] = false;
                        }
                        // If the GameObject wasn't found initially, this part still correctly increments the count.
                    }
                }
                // If the robot is not detected, its filtered position/orientation remains at the last known filtered value
                // until it's detected again or moved out of sight.
            }
        }

        // Process Yellow Robots (Similar logic as blue robots)
        HashSet<uint> detectedYellowIdsThisFrame = new HashSet<uint>(currentYellowRobotDetections.Keys);
        for (uint i = 0; i < NUM_ROBOTS; i++) // Iterate through all possible robot IDs
        {
            string obj_name = "yellow_robot" + i.ToString();

            if (detectedYellowIdsThisFrame.Contains(i)) // If robot i was detected by at least one camera
            {
                List<SSL_DetectionRobot> detections = currentYellowRobotDetections[i];

                // Calculate average measured position for this frame
                float measuredAvgX = detections.Average(r => r.X) * Param.SCALE_COORDINATE;
                float measuredAvgY = detections.Average(r => r.Y) * Param.SCALE_COORDINATE;
                Vector3 currentMeasuredPosition = new UnityEngine.Vector3(measuredAvgX, Param.ROBOT_Z, measuredAvgY);

                // Calculate average measured orientation for this frame
                float sumSin = detections.Sum(r => Mathf.Sin(r.Orientation));
                float sumCos = detections.Sum(r => Mathf.Cos(r.Orientation));
                float measuredAvgOrientationRad = Mathf.Atan2(sumSin, sumCos); // Radian


                // Apply EMA to Position
                if (!yellowRobotFilteredInitialized.ContainsKey(i) || !yellowRobotFilteredInitialized[i])
                {
                    // First detection, initialize filtered position
                    filteredYellowRobotPositions[i] = currentMeasuredPosition;
                    yellowRobotFilteredInitialized[i] = true;
                }
                else
                {
                    // Apply EMA
                    filteredYellowRobotPositions[i] = Vector3.Lerp(filteredYellowRobotPositions[i], currentMeasuredPosition, smoothingFactor);
                }

                // Apply EMA to Orientation (needs careful handling)
                Vector2 measuredVec = new Vector2(Mathf.Cos(measuredAvgOrientationRad), Mathf.Sin(measuredAvgOrientationRad));
                Vector2 prevFilteredVec = new Vector2(Mathf.Cos(filteredYellowRobotOrientations.ContainsKey(i) ? filteredYellowRobotOrientations[i] : measuredAvgOrientationRad),
                                                    Mathf.Sin(filteredYellowRobotOrientations.ContainsKey(i) ? filteredYellowRobotOrientations[i] : measuredAvgOrientationRad));

                Vector2 newFilteredVec = Vector2.Lerp(prevFilteredVec, measuredVec, smoothingFactor); // Apply EMA in vector form
                filteredYellowRobotOrientations[i] = Mathf.Atan2(newFilteredVec.y, newFilteredVec.x); // Convert back to angle (radian)


                // Convert filtered orientation (radian) to Unity rotation (degrees)
                float filteredAvgDir = -(filteredYellowRobotOrientations[i] + Mathf.PI / 2) * Mathf.Rad2Deg;

                // Apply the filtered state to the GameObject
                string robotObjName = "yellow_robot" + i.ToString();
                if (yellowRobots.ContainsKey(robotObjName) && yellowRobots[robotObjName] != null)
                {
                    yellowRobots[robotObjName].transform.position = filteredYellowRobotPositions[i];
                    yellowRobots[obj_name].transform.rotation = UnityEngine.Quaternion.Euler(0, filteredAvgDir, 0);
                    yellowDisappearanceCounts[obj_name] = 0; // Reset disappearance counter
                }
                else
                {
                    Debug.LogWarning($"Yellow Robot GameObject not found in dictionary for ID {i} during update application: {robotObjName}");
                }
            }
            else // If robot i was NOT detected by *any* camera this frame
            {
                string robotObjName = "yellow_robot" + i.ToString();
                if (yellowDisappearanceCounts.ContainsKey(robotObjName))
                {
                    yellowDisappearanceCounts[robotObjName]++; // Increase disappearance counter
                    if (yellowDisappearanceCounts[obj_name] > DISAPPEARANCE_THRESHOLD)
                    {
                        // If the robot has been missing for too many frames, move it out of sight
                        if (yellowRobots.ContainsKey(obj_name) && yellowRobots[obj_name] != null)
                        {
                            yellowRobots[obj_name].transform.position = new UnityEngine.Vector3(0, Param.OUT_OF_SIGHT_Y, 0);
                            // Optionally, reset initialization flag
                            // yellowRobotFilteredInitialized[i] = false;
                        }
                    }
                }
            }
        }

        // --- Process Geometry Data (Optional, as before) ---
        // ... (Geometry processing logic remains the same as in the previous response if needed)
    }

    void ReceiveData()
    {
        // This method runs on a separate thread.
        byte[] buffer = new byte[65535]; // Maximum UDP data packet size
        EndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

        while (isRunning)
        {
            try
            {
                int bytesReceived = multicastSocket.ReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref remoteEndPoint);

                if (bytesReceived > 0)
                {
                    byte[] data = new byte[bytesReceived];
                    Array.Copy(buffer, data, bytesReceived);

                    try
                    {
                        SSL_WrapperPacket newPacket = SSL_WrapperPacket.Parser.ParseFrom(data);

                        if (newPacket.Detection != null)
                        {
                            lock (packetLock)
                            {
                                if (newPacket.Detection.CameraId < latestCameraPackets.Length)
                                {
                                    latestCameraPackets[newPacket.Detection.CameraId] = newPacket;
                                }
                                else
                                {
                                    Debug.LogWarning($"Received packet with invalid CameraId: {newPacket.Detection.CameraId}");
                                }
                            }
                        }
                        // Handle Geometry packets if needed - store them similarly if they have CameraId
                        // or in a separate variable/list if not.
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"Failed to parse packet: {ex.Message}");
                    }
                }
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode == SocketError.TimedOut)
                {
                    // Debug.Log("Multicast receive timed out.");
                }
                else if (ex.SocketErrorCode == SocketError.Interrupted || ex.SocketErrorCode == SocketError.OperationAborted)
                {
                    Debug.Log("Multicast socket operation aborted.");
                    isRunning = false;
                }
                else
                {
                    Debug.LogError("Socket error during receive: " + ex.Message);
                }
            }
            catch (ObjectDisposedException)
            {
                Debug.Log("Multicast socket disposed.");
                isRunning = false;
            }
            catch (Exception ex)
            {
                Debug.LogError("Error in receive thread: " + ex.Message);
            }
            // Thread.Sleep(1); // Optional pause
        }
        Debug.Log("Receive thread exiting.");
    }

    void OnApplicationQuit()
    {
        isRunning = false;
        if (multicastSocket != null)
        {
            try
            {
                multicastSocket.Close();
                Debug.Log("Multicast socket closed during application quit.");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error closing socket during quit: {ex.Message}");
            }
            multicastSocket = null;
        }
        // Join is less reliable in Unity's quit process than relying on socket close
        // if (receiveThread != null && receiveThread.IsAlive)
        // {
        //     receiveThread.Join(100);
        // }
        Debug.Log("OnApplicationQuit finished.");
    }
}