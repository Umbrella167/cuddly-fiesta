using UnityEngine;
using System.IO; // Required for File operations
// ConfigModels.cs (or within ParamLoader.cs)
using System;

[Serializable]
public class VisionConfig
{
    public string MCAST_GRP;
    public int MCAST_PORT_SIM;
    public int MCAST_PORT_REAL;
}

[Serializable]
public class RefBoxConfig
{
    public string REF_MCAST_GRP;
    public int REF_MCAST_PORT;
}

[Serializable]
public class RealControlUDPConfig
{
    public int CONTROL_SERVERPORT;
}

[Serializable]
public class RealParamsConfig
{
    // Names here should match JSON keys under "RealParams"
    public float PIDRotation_KP;
    public float PIDRotation_KI;
    public float PIDRotation_KD;

    public float POWERSET_RATE_CHIP;
    public float POWERSET_MIN_CHIP;
    public float POWERSET_MAX_CHIP;

    public float POWERSET_RATE_FLAT;
    public float POWERSET_MIN_FLAT;
    public float POWERSET_MAX_FLAT;

    public float DRIBBLING_ACC;
    public float UNDRIBBLING_ACC;

    public float NORMAL_SPEED; // Note: Key in JSON is "NORMAL_SPEED"
    public float SLOW_SPEED;
    public float MAX_SPEED;
}

[Serializable]
public class SimParamsConfig
{
    // Names here should match JSON keys under "SimParams"
    public float PIDROTATION_KP; // Note: Key in JSON is "PIDROTATION_KP"
    public float PIDROTATION_KI;
    public float PIDROTATION_KD;

    public float POWERSET_RATE_CHIP;
    public float POWERSET_MIN_CHIP;
    public float POWERSET_MAX_CHIP;

    public float POWERSET_RATE_FLAT;
    public float POWERSET_MIN_FLAT;
    public float POWERSET_MAX_FLAT;

    public float DRIBBLING_ACC;
    public float UNDRIBBLING_ACC;

    public float NORMAL_SPEED; // Note: Key in JSON is "NORMAL_SPEED"
    public float SLOW_SPEED;
    public float MAX_SPEED;
}

[Serializable]
public class AppConfig // This is the root object for your JSON
{
    public VisionConfig Vision;
    public RefBoxConfig RefBox;
    public RealControlUDPConfig RealControlUDP;
    public RealParamsConfig RealParams;
    public SimParamsConfig SimParams;
}
public class ParamLoader : MonoBehaviour
{
    // You can keep the ConfigModels (AppConfig, VisionConfig, etc.) in a separate file
    // or define them above this class if they are only used here.

    void Awake() // Awake is called before any Start methods
    {
        LoadParamsFromJson();
    }

    public static void LoadParamsFromJson() // Made static so it can be called from elsewhere if needed
    {
        string configPath = Path.Combine(Application.dataPath, "config.json");

        if (File.Exists(configPath))
        {
            string jsonString = File.ReadAllText(configPath);
            AppConfig config = JsonUtility.FromJson<AppConfig>(jsonString);

            if (config != null)
            {
                // ////////////////////////////////////////////////////////////JSON READ////////////////////////////////////////////////////////////////////////////////////
                if (config.Vision != null)
                {
                    Param.MCAST_GRP = config.Vision.MCAST_GRP;
                    Param.MCAST_PORT_SIM = config.Vision.MCAST_PORT_SIM;
                    Param.MCAST_PORT_REAL = config.Vision.MCAST_PORT_REAL;
                }
                else
                {
                    Debug.LogError("Vision config section is missing in config.json!");
                }

                if (config.RefBox != null)
                {
                    Param.REF_MCAST_GRP = config.RefBox.REF_MCAST_GRP;
                    Param.REF_MCAST_PORT = config.RefBox.REF_MCAST_PORT;
                }
                else
                {
                    Debug.LogError("RefBox config section is missing in config.json!");
                }

                if (config.RealControlUDP != null)
                {
                    Param.CONTROL_SERVERPORT = config.RealControlUDP.CONTROL_SERVERPORT;
                }
                else
                {
                    Debug.LogError("RealControlUDP config section is missing in config.json!");
                }


                /////////////////////////////////////////////////////////////////////////////REAL//////////////////////////////////////////////////////////////////////////
                if (config.RealParams != null)
                {
                    // Assuming you want to map the consolidated PID values
                    Param.REAL_PIDROTATION_KP = config.RealParams.PIDRotation_KP;
                    // The original Param class has two sets of KI/KD for REAL. I'm using the second set's variable names.
                    // Adjust if your original `EAL_PIDRotation_KI` (which I assume is a typo for REAL_PIDRotation_KI) 
                    // and the first `REAL_PIDRotation_KD` were meant for something else.
                    // For now, I'll map to the second, more complete set.
                    Param.REAL_PIDROTATION_KI = config.RealParams.PIDRotation_KI;
                    Param.REAL_PIDROTATION_KD = config.RealParams.PIDRotation_KD;


                    Param.REAL_POWERSET_RATE_CHIP = config.RealParams.POWERSET_RATE_CHIP;
                    Param.REAL_POWERSET_MIN_CHIP = config.RealParams.POWERSET_MIN_CHIP;
                    Param.REAL_POWERSET_MAX_CHIP = config.RealParams.POWERSET_MAX_CHIP;

                    Param.REAL_POWERSET_RATE_FLAT = config.RealParams.POWERSET_RATE_FLAT;
                    Param.REAL_POWERSET_MIN_FLAT = config.RealParams.POWERSET_MIN_FLAT;
                    Param.REAL_POWERSET_MAX_FLAT = config.RealParams.POWERSET_MAX_FLAT;

                    // AUTO ACC (Mapping to DRIBBLING_ACC and UNDRIBBLING_ACC in Param.cs that were under REAL comment)
                    // Assuming the Param class had DRIBBLING_ACC and UNDRIBBLING_ACC fields that were intended for REAL
                    // If they were shared with SIM, this is fine. If they were separate (e.g. REAL_DRIBBLING_ACC), adjust Param.cs
                    Param.DRIBBLING_ACC = config.RealParams.DRIBBLING_ACC; // This will set the shared DRIBBLING_ACC
                    Param.UNDRIBBLING_ACC = config.RealParams.UNDRIBBLING_ACC; // This will set the shared UNDRIBBLING_ACC

                    Param.REAL_NROMAL_SPEED = config.RealParams.NORMAL_SPEED; // Corrected to NORMAL_SPEED in JSON
                    Param.REAL_SLOW_SPEED = config.RealParams.SLOW_SPEED;
                    Param.REAL_MAX_SPEED = config.RealParams.MAX_SPEED;
                }
                else
                {
                    Debug.LogError("RealParams config section is missing in config.json!");
                }

                /////////////////////////////////////////////////////////////////////////////SIM//////////////////////////////////////////////////////////////////////////
                if (config.SimParams != null)
                {
                    Param.SIM_PIDROTATION_KP = config.SimParams.PIDROTATION_KP;
                    Param.SIM_PIDROTATION_KI = config.SimParams.PIDROTATION_KI;
                    Param.SIM_PIDROTATION_KD = config.SimParams.PIDROTATION_KD;

                    Param.SIM_POWERSET_RATE_CHIP = config.SimParams.POWERSET_RATE_CHIP;
                    Param.SIM_POWERSET_MIN_CHIP = config.SimParams.POWERSET_MIN_CHIP;
                    Param.SIM_POWERSET_MAX_CHIP = config.SimParams.POWERSET_MAX_CHIP;

                    Param.SIM_POWERSET_RATE_FLAT = config.SimParams.POWERSET_RATE_FLAT;
                    Param.SIM_POWERSET_MIN_FLAT = config.SimParams.POWERSET_MIN_FLAT;
                    Param.SIM_POWERSET_MAX_FLAT = config.SimParams.POWERSET_MAX_FLAT;

                    // AUTO ACC
                    Param.SIM_DRIBBLING_ACC = config.SimParams.DRIBBLING_ACC;
                    Param.SIM_UNDRIBBLING_ACC = config.SimParams.UNDRIBBLING_ACC;

                    // CONTROL SPEED
                    Param.SIM_NROMAL_SPEED = config.SimParams.NORMAL_SPEED; // Corrected to NORMAL_SPEED in JSON
                    Param.SIM_SLOW_SPEED = config.SimParams.SLOW_SPEED;
                    Param.SIM_MAX_SPEED = config.SimParams.MAX_SPEED;
                }
                else
                {
                    Debug.LogError("SimParams config section is missing in config.json!");
                }

                Debug.Log("Parameters loaded from config.json successfully.");
            }
            else
            {
                Debug.LogError("Failed to parse config.json. Check JSON structure and helper classes.");
            }
        }
        else
        {
            Debug.LogError("config.json not found at path: " + configPath);
        }
    }
}