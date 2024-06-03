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
                Statistic.SetField("reason_lose", 0);
            }
            else if (collision.collider.CompareTag("Car"))
            {
                Statistic.SetField("reason_lose", 1);
            }
            else if (collision.collider.CompareTag("Human"))
            {
                Statistic.SetField("reason_lose", 2);
            }
            Debug.Log("Hit here!!!");
            //GetComponent<CarController>().PositionLogger.LogWinLose(0);
            Launcher.ExitRoom();
            UiController.Instance.OpenUiEndLose();
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
                Statistic.SetField("reason_lose", 0);
            }    
            else if (collision.CompareTag("Car"))
            {
                Statistic.SetField("reason_lose", 1);
            }
            else if (collision.CompareTag("Human"))
            {
                Statistic.SetField("reason_lose", 2);
            }
            Debug.Log(Statistic.GetField("reason_lose"));
            //GetComponent<CarController>().PositionLogger.LogWinLose(0);
            Launcher.ExitRoom();
            UiController.Instance.OpenUiEndLose();
        }

        else if (collision.CompareTag("Gate"))
        {
            //GetComponent<CarController>().PositionLogger.LogWinLose(1);
            Launcher.ExitRoom();
            UiController.Instance.OpenUiEndWin();
        }    
    }
}
