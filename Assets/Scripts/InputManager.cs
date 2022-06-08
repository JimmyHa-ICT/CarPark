using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InputManager : MonoBehaviour, IPunObservable
{
   
    public float SteerInput;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsReading)
        {
            SteerInput = (float) stream.ReceiveNext();
            Debug.Log($"Steer input {SteerInput}");
        }
    }
}
