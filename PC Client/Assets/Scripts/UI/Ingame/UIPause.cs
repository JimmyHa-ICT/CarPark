using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPause : UiBase
{
    public Button BtnResume;
    public Button BtnQuit;

    private void Start()
    {
        BtnResume.onClick.AddListener(OnClickBtnResume);
        BtnQuit.onClick.AddListener(OnClickBtnQuit);
    }

    private void OnClickBtnResume()
    {
        Hide();
    }

    private void OnClickBtnQuit()
    {
        Launcher.ExitRoom();
        Hide();
        UiController.Instance.OpenUiLobby();
    }
}
