using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Connect_Client : MonoBehaviour
{
    public TMP_Dropdown TeamDropdown;
    public TMP_InputField RobotId_Input, Frequency_Input, IP_Input, Port_Input;
    public static string serverIP;
    public static int serverPort;
    public static int RobotID;
    public static string frequency;
    public Button Connect_Button;
    // Start is called before the first frame update
    void Start()
    {
        IP_Input = GameObject.Find("IP_Input").GetComponent<TMP_InputField>();
        Port_Input = GameObject.Find("Port_Input").GetComponent<TMP_InputField>();
        RobotId_Input = GameObject.Find("RobotId_Input").GetComponent<TMP_InputField>();
        Frequency_Input = GameObject.Find("Frequency_Input").GetComponent<TMP_InputField>();
        Connect_Button.onClick.AddListener(ButtonOnClickEvent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ButtonOnClickEvent()
    {
        Connect_Gate.IP = IP_Input.text;
        int.TryParse(Port_Input.text, out Connect_Gate.Port);
        int.TryParse(RobotId_Input.text, out Connect_Gate.robotID);
        int.TryParse(Frequency_Input.text, out Connect_Gate.frequency);
        Connect_Gate.team = TeamDropdown.options[TeamDropdown.value].text;
        SceneManager.LoadScene("World");
    }


    }
