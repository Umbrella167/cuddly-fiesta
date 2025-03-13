using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Connect_Gate : MonoBehaviour
{
    public TMP_Dropdown ConnectMode;
    public GameObject Serial_Mode;
    public GameObject Socket_Mode;

    void Start()
    {
        InitConnectModeDropdown();
        UpdateModeVisibility();
    }

    private void InitConnectModeDropdown()
    {
        ConnectMode.ClearOptions();
        ConnectMode.AddOptions(new List<string> { "Serial", "Socket" });
    }

    private void Update()
    {
        UpdateModeVisibility();
    }

    private void UpdateModeVisibility()
    {
        Serial_Mode.SetActive(ConnectMode.options[ConnectMode.value].text == "Serial");
        Socket_Mode.SetActive(ConnectMode.options[ConnectMode.value].text == "Socket");
    }
}