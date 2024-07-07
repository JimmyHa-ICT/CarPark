using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIEndLose : UiBase
{
    public Button btnHome;
    public Text txtReason;

    // Start is called before the first frame update
    void Start()
    {
        btnHome.onClick.AddListener(OnClickButtonHome);
    }

    public override void Show()
    {
        base.Show();
        txtReason.gameObject.SetActive(false);
        DOVirtual.DelayedCall(1, ShowReasonLose);
    }

    private void ShowReasonLose()
    {
        Server.Instance.LogMetric();
        txtReason.gameObject.SetActive(true);
        switch (Statistic.GetField("reason_lose"))
        {
            case 1:
                txtReason.text = "You hit the wall!";
                break;
            case 2:
                txtReason.text = "You hit other car!";
                break;
            case 3:
                txtReason.text = "You hit the pedestrian!";
                break;
        }
    }

    private void OnClickButtonHome()
    {
        Hide();
        UiController.Instance.OpenUiLobby();
    }
}
