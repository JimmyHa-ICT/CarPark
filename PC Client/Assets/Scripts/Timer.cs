using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float timeCount;

    // Start is called before the first frame update
    void Start()
    {
        timeCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;
    }

    private void OnDisable()
    {
        Statistic.SetField("time", (int) timeCount);
        Debug.Log(Statistic.GetField("time"));
    }
}
