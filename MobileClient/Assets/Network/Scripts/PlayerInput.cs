using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance;
    public int PORT = 44231;
    public string IPString;
    private UdpClient udpClient;
    public IPEndPoint endPoint;

    public bool ThrottlePressed;
    public bool BrakePressed;
    public int Gear = 1;
    public float WheelInput;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        udpClient = new UdpClient();
        endPoint = new IPEndPoint(IPAddress.Broadcast, PORT); // Port for discovery
    }

    private void Update()
    {
        var message = new InputMessage(ThrottlePressed, BrakePressed, Gear, WheelInput);
        var messageData = Encoding.UTF8.GetBytes(JsonUtility.ToJson(message));

        udpClient.Send(messageData, messageData.Length, endPoint);
    }
}