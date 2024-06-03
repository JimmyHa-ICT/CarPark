using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    public GameObject car;

    void Start()
    {
        // try to connect
        PhotonNetwork.ConnectUsingSettings();
        UiController.Instance.OpenUiLobby();
    }


    #region PUN Callbacks
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connect to master!!!");
        UiController.Instance.UILobby.StartButton.gameObject.SetActive(true);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        var loadScene = SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
        loadScene.completed += LoadScene_completed;
        Debug.Log("Join a room successfully!!!");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        EventBroker.Instance.EmitOtherPlayerJoinRoom(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        EventBroker.Instance.EmitOtherPlayerLeftRoom(otherPlayer);
    }
    #endregion

    public static void JoinRoom()
    {
        PhotonNetwork.LocalPlayer.NickName = Server.Instance.UserName;
        PhotonNetwork.JoinRandomOrCreateRoom();
        //PhotonNetwork.JoinOrCreateRoom(roomname);   // get room name from server response
    }

    public static void ExitRoom()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.UnloadSceneAsync(2);
    }

    private void LoadScene_completed(AsyncOperation obj)
    {
        //PhotonNetwork.Instantiate(car.name, Vector3.one * Random.Range(-10, 10), Quaternion.identity, 0);
        GetRandomCarInRoom();
        UiController.Instance.OpenUiIngame();
    }

    #region Car instantiation
    private void GetRandomCarInRoom()
    {
        var allCars = WorldAPI.Instance.CarPositions;
        var carPos = allCars[Random.Range(0, allCars.Length)];

        while (CheckOverlapOtherPlayerPosition(carPos.position))
            carPos = allCars[Random.Range(0, allCars.Length)];

        PhotonNetwork.Instantiate(car.name, carPos.position, carPos.rotation, 0);
    }

    private bool CheckOverlapOtherPlayerPosition(Vector3 pos)
    {
        //var playerList = PhotonNetwork.PlayerList;
        var cars = GameObject.FindGameObjectsWithTag("Car");
        for (int i = 0; i < cars.Length; i++)
        {
            if (Vector3.SqrMagnitude(pos - cars[i].transform.position) < 1)
                return true;
        }
        return false;
    }
    #endregion
}
