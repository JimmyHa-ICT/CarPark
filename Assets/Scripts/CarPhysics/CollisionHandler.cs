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
