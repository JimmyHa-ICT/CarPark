using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CollisionHandler : MonoBehaviourPun
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!photonView.IsMine)
            return;

        if (collision.collider.CompareTag("Wall") || collision.collider.CompareTag("Car"))
        {
            if (collision.collider.CompareTag("Wall"))
            {
                int cls = Statistic.GetField("collision_wall");
                Statistic.SetField("collision_wall", cls + 1);
            }
            else if (collision.collider.CompareTag("Car"))
            {
                int cls = Statistic.GetField("collision_car");
                Statistic.SetField("collision_car", cls + 1);
            }
            else if (collision.collider.CompareTag("Human"))
            {
                int cls = Statistic.GetField("collision_human");
                Statistic.SetField("collision_human", cls + 1);
            }
            Debug.Log("Hit here!!!");
            //GetComponent<CarController>().PositionLogger.LogWinLose(0);
            //Launcher.ExitRoom();
            //UiController.Instance.OpenUiEndLose();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!photonView.IsMine)
            return;

        if (collision.CompareTag("Wall") || collision.CompareTag("Car") || collision.CompareTag("Human"))
        {
            if (collision.CompareTag("Wall"))
            {
                int cls = Statistic.GetField("collision_wall");
                Statistic.SetField("collision_wall", cls + 1);
            }    
            else if (collision.CompareTag("Car"))
            {
                int cls = Statistic.GetField("collision_car");
                Statistic.SetField("collision_car", cls + 1);
            }
            else if (collision.CompareTag("Human"))
            {
                int cls = Statistic.GetField("collision_human");
                Statistic.SetField("collision_human", cls + 1);
            }
            Debug.Log(Statistic.GetField("reason_lose"));
            //GetComponent<CarController>().PositionLogger.LogWinLose(0);
            //Launcher.ExitRoom();
            //UiController.Instance.OpenUiEndLose();
        }

        else if (collision.CompareTag("Gate"))
        {
            //GetComponent<CarController>().PositionLogger.LogWinLose(1);
            Launcher.ExitRoom();
            UiController.Instance.OpenUiEndWin();
        }    
    }
}
