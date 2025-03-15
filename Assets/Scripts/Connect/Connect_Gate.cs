using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static packet;

public class Connect_Gate : MonoBehaviour
{
    
    public TMP_Dropdown ConnectMode;
    public TMP_Dropdown GameMode;
    public GameObject Serial_Mode;
    public GameObject Socket_Mode;
    static public string GAME_CONNECT_MODE;
    static public string GAME_MODE;
    static public int robotID = 0;
    static public int frequency = 0;
    static public int Port;
    static public string IP;
    static public string team = null;
    static public RadioPacket packet = null;

    void Start()
    {
        InitConnectModeDropdown();
        InitGameModeDropdown();
        UpdateModeVisibility();
    }

    private void InitConnectModeDropdown()
    {
        ConnectMode.ClearOptions();
        ConnectMode.AddOptions(new List<string> { "Serial", "Client" });
    }
    private void InitGameModeDropdown()
    {
        GameMode.ClearOptions();
        GameMode.AddOptions(new List<string> { "Real", "Simulate" });
    }
    private void Update()
    {
        UpdateModeVisibility();
    }

    private void UpdateModeVisibility()
    {
        Serial_Mode.SetActive(ConnectMode.options[ConnectMode.value].text == "Serial");
        Socket_Mode.SetActive(ConnectMode.options[ConnectMode.value].text == "Client");
        GAME_CONNECT_MODE = ConnectMode.options[ConnectMode.value].text;
        GAME_MODE = GameMode.options[GameMode.value].text;
    }
}