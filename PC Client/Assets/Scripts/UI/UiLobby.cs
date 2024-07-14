using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class UiLobby : UiBase
{
    public Button StartButton;
    public Button LogoutButton;
    public Text usernameTxt;

    private void Awake()
    {
        //usernameInput.text = "User" + Random.Range(1000, 10000);
        usernameTxt.text =  "Welcome, " + Server.Instance.UserName;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartButton.onClick.AddListener(OnClickBtnStart);
        LogoutButton.onClick.AddListener(OnClickBtnLogout);
    }

    public override void Show()
    {
        base.Show();
        Statistic.Init();
        StartButton.gameObject.SetActive(PhotonNetwork.IsConnectedAndReady);
    }

    private void OnClickBtnStart()
    {
        Launcher.JoinRoom();
        Server.Instance.LogNewSession();
        Hide();
    }

    private void OnClickBtnLogout()
    {
        PlayerPrefs.DeleteKey("username");
        SceneManager.LoadSceneAsync(0);
    }
}
