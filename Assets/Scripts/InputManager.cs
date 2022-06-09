using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InputManager : MonoBehaviour, IPunObservable
{
   
    public float SteerInput;

    public int Throttle;

    public int Brake;

    public int Gear;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsReading)
        {
            SteerInput = (float) stream.ReceiveNext();
            Debug.Log($"Steer input {SteerInput}");
            Throttle = (int) stream.ReceiveNext();
            Brake = (int) stream.ReceiveNext();
            Gear = (int)stream.ReceiveNext();
            Debug.Log($"Steer input {SteerInput}");
        }
    }
}
