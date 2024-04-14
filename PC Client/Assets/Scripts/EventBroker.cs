using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;
using Photon.Realtime;

public class EventBroker : MonoBehaviour
{
    [HideInInspector] public UnityEvent<Player> OnOtherPlayerEnterRoom;
    [HideInInspector] public UnityEvent<Player> OnOtherPlayerLeftRoom;

    public static EventBroker Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    public void EmitOtherPlayerJoinRoom(Player player)
    {
        OnOtherPlayerEnterRoom?.Invoke(player);
    }

    public void EmitOtherPlayerLeftRoom(Player player)
    {
        OnOtherPlayerLeftRoom?.Invoke(player);
    }
}
