using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

[System.Serializable]
public class InputMessage
{
    public bool ThrottlePressed;
    public bool BrakePressed;
    public int Gear;
    public float WheelInput;

    public InputMessage(bool throttle, bool brake, int gear, float WheelInput)
    {
        ThrottlePressed = throttle;
        BrakePressed = brake;
        Gear = gear;
        this.WheelInput = WheelInput;
    }
}
