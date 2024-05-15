using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MobileLauncher : MonoBehaviourPunCallbacks
{
    [SerializeField] private PhotonView InputManager;
    void Start()
    {
        // try to connect
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        PhotonNetwork.JoinRandomOrCreateRoom();
        Debug.Log("Connect to master!!!");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        var input = PhotonNetwork.Instantiate(InputManager.name, Vector3.zero, Quaternion.identity);
        input.transform.SetParent(transform);
        Debug.Log("Join a room successfully!!!");
    }
}
