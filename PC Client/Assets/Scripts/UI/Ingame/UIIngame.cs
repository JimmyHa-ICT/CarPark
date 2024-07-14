using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class UIIngame : UiBase
{
    public UiPlayerSlot playerSlotPfb;
    public List<UiPlayerSlot> PlayerSlots;
    public Transform groupSlots;
    public Button BtnPause;

    private void Start()
    {
        InitializePlayerSlots();
        EventBroker.Instance.OnOtherPlayerEnterRoom.AddListener(AddSlot);
        EventBroker.Instance.OnOtherPlayerLeftRoom.AddListener(RemoveSlot);
        BtnPause.onClick.AddListener(OnClickBtnPause);
    }

    private void Update()
    {
        if (Time.frameCount % 60 == 0)
            UpdatePlayerSlots();
    }

    private void UpdatePlayerSlots()
    {
        var playerList = PhotonNetwork.PlayerList;
        var skins = LocalDataManager.Instance.CarSkinList.Skins;
        for (int i = 0; i < PlayerSlots.Count; i++)
        {
            PlayerSlots[i].username.text = playerList[i].NickName;
            if (playerList[i].ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
                PlayerSlots[i].username.color = Color.yellow;
            PlayerSlots[i].icon.sprite = skins[i % skins.Length];
        }
    }

    private void InitializePlayerSlots()
    {
        ClearAllSlots();
        var playerList = PhotonNetwork.PlayerList;
        for (int i = 0; i < playerList.Length; i++)
        {
            PlayerSlots.Add(Instantiate(playerSlotPfb, groupSlots));
        }
        UpdatePlayerSlots();
    }

    private void ClearAllSlots()
    {
        for (int i = 0; i < PlayerSlots.Count; i++)
        {
            Destroy(PlayerSlots[i].gameObject);
        }
        PlayerSlots.Clear();
    }

    private void RemoveSlot(Player player)
    {
        Debug.Log("Remove Slot");
        Destroy(PlayerSlots[0].gameObject);
        PlayerSlots.RemoveAt(0);
        UpdatePlayerSlots();
    }

    private void AddSlot(Player player)
    {
        PlayerSlots.Add(Instantiate(playerSlotPfb, groupSlots));
        UpdatePlayerSlots();
    }    

    private void OnClickBtnPause()
    {
        UiController.Instance.OpenUIPause();
    }
}
