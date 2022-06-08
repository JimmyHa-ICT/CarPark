using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MobileInputSender : MonoBehaviour, IPunObservable
{
    public SteeringWheel SteeringWheel;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(SteeringWheel.SteerInput);
        }
#if UNITY_EDITOR
        else
        {
            Debug.Log($"Steering wheel {(float)stream.ReceiveNext()}");
        }
#endif
    }
}
