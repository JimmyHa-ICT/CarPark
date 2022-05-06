using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall") || collision.CompareTag("Car"))
        {
            GetComponent<CarController>().PositionLogger.LogWinLose(0);
            SceneController.SharedInstance.ChangeScene(2);
        }

        else if (collision.CompareTag("Gate"))
        {
            GetComponent<CarController>().PositionLogger.LogWinLose(1);
            SceneController.SharedInstance.ChangeScene(3);
        }    
    }
}
