using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{
    public UiLobby UILobby;
    public UIEndWin UIEndWin;
    public UIEndLose UIEndLose;
    public UIIngame UIIngame;

    public static UiController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void CloseAll()
    {
        UILobby.Hide();
        UIEndWin.Hide();
        UIEndLose.Hide();
        UIIngame.Hide();
    }

    public void OpenUiLobby()
    {
        CloseAll();
        UILobby.Show();
    }

    public void OpenUiEndWin()
    {
        CloseAll();
        UIEndWin.Show();
    }

    public void OpenUiEndLose()
    {
        CloseAll();
        UIEndLose.Show();
    }

    public void OpenUiIngame()
    {
        CloseAll();
        UIIngame.Show();
    }
}
