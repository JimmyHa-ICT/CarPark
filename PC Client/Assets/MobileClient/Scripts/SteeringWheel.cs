using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SteeringWheel : MonoBehaviour
{
    public float SteerInput = 0;
    [SerializeField] private float currentAngle;
    [SerializeField] private readonly float BOUND = 300;

    [HideInInspector] public float Angle
    {
        get
        {
            return transform.eulerAngles.z;
        }
    }
    
    private void Start()
    {
        SteerInput = 0;
        currentAngle = 0;
    }

    public void OnUpdate(float SteerInput)
    {
        currentAngle = SteerInput * BOUND;
        transform.eulerAngles = new Vector3(0, 0, currentAngle);
        //if (MobileInputSender.Instance != null)
        //    MobileInputSender.Instance.Steering = SteerInput;
    }
}
