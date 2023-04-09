using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CarSkin : MonoBehaviourPun
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        var playerList = PhotonNetwork.PlayerList;
        for (int i = 0; i < playerList.Length; i++)
        {
            if (playerList[i].ActorNumber == photonView.Owner.ActorNumber)
            {
                var skinList = LocalDataManager.Instance.CarSkinList.Skins;
                spriteRenderer.sprite = skinList[i % skinList.Length];
            }
        }
    }
}
