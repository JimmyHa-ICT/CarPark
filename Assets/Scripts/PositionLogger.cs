using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionLogger : MonoBehaviour
{
    private string Data;
    float width, height;

    // Start is called before the first frame update
    void Start()
    {
        var collider = GetComponent<BoxCollider2D>();
        width = collider.size.x;
        height = collider.size.y;
        Data = $"{width} {height}\n";
        Data += JsonUtility.ToJson(transform.position) + " " + JsonUtility.ToJson(transform.eulerAngles) + "\n";
    }

    public void LogPosition()
    {
        Data += JsonUtility.ToJson(transform.position) + " " + JsonUtility.ToJson(transform.eulerAngles) + "\n";
    }

    public void LogWinLose(int state)
    {
        Data += state;
    }

    private void OnDestroy()
    {
        Debug.Log(Data);
        FileManager.WriteToFile("data.dat", Data);
    }
}
