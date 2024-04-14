using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiLobby : UiBase
{
    public Button StartButton;
    public InputField usernameInput;

    private void Awake()
    {
        usernameInput.text = "User" + Random.Range(1000, 10000);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartButton.onClick.AddListener(OnClickBtnStart);
    }

    private void OnClickBtnStart()
    {
        Launcher.JoinRoom();
        Hide();
    }
}
