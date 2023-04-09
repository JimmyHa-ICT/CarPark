using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MobileInputSender : MonoBehaviour, IPunObservable
{
    public static MobileInputSender Instance;

    public float Steering = 0;

    public int Throttle = 0;

    public int Brake = 0;

    public int Gear = 1;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(Steering);
            stream.SendNext(Throttle);
            stream.SendNext(Brake);
            stream.SendNext(Gear);
        }
        else
        {
            this.Steering = (float) stream.ReceiveNext();
            this.Throttle = (int) stream.ReceiveNext();
            this.Brake = (int) stream.ReceiveNext();
            this.Gear = (int) stream.ReceiveNext();
        }
    }
}
