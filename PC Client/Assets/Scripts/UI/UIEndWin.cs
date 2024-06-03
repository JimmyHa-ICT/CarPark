using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIEndWin : UiBase
{
    public Button btnHome;
    public Text txtTime;

    // Start is called before the first frame update
    void Start()
    {
        btnHome.onClick.AddListener(OnClickButtonHome);
    }

    public override void Show()
    {
        base.Show();
        txtTime.gameObject.SetActive(false);
        DOVirtual.DelayedCall(0.8f, ShowTimeWin);
    }

    private void ShowTimeWin()
    {
        txtTime.gameObject.SetActive(true);
        txtTime.text = $"You take {Statistic.GetField("time")} seconds to win";
    }    

    private void OnClickButtonHome()
    {
        Hide();
        UiController.Instance.OpenUiLobby();
    }    
}
