using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIEndWin : UiBase
{
    public Button btnHome;
    public Text txtTime;
    public Text txtCollision;

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
        Server.Instance.LogMetric();
        txtTime.gameObject.SetActive(true);
        txtCollision.gameObject.SetActive(true);
        txtTime.text = $"Time take {Statistic.GetField("time")} seconds";
        int collision_wall = Statistic.GetField("collision_wall");
        int collision_car = Statistic.GetField("collision_car");
        int collision_human = Statistic.GetField("collision_human");
        int collision = collision_wall + collision_car + collision_human;
        if (collision == 0)
            txtCollision.text = $"Collision {collision}\n";
        else
        {
            string str = $"Total collision {collision}\n";
            if (collision_human > 0)
                str += $"\tCollide with human: {collision_human}\n";
            if (collision_car > 0)
                str += $"\tCollide with car: {collision_car}\n";
            if (collision_wall > 0)
                str += $"\tCollide with wall: {collision_wall}\n";
            txtCollision.text = str;
        }
    }    

    private void OnClickButtonHome()
    {
        Hide();
        UiController.Instance.OpenUiLobby();
    }    
}
