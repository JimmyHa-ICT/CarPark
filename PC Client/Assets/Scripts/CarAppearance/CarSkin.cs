using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CarSkin : MonoBehaviourPun
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private bool isAI = false;


    // Start is called before the first frame update
    void Start()
    {
        if (isAI)
        {
            var skinList = LocalDataManager.Instance.CarSkinList.Skins;
            spriteRenderer.sprite = skinList[Random.Range(0, skinList.Length)];
            return;
        }

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
