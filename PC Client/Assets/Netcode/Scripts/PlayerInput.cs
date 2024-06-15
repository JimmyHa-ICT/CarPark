using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class PlayerInput : MonoBehaviour
{
    public int PORT = 44231;
    public static PlayerInput Instance;
    public bool ThrottlePressed;
    public bool BrakePressed;
    public int Gear;
    public float WheelInput;
    
    private UdpClient udpClient;
    public IPEndPoint endPoint;

    public float TIMEOUT = 60;
    private float time;

    [SerializeField] private ClientDiscovery clientDiscovery;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }


    void Start()
    {
        udpClient = new UdpClient(PORT); // Port for discovery
        endPoint = new IPEndPoint(IPAddress.Any, 0);
        udpClient.BeginReceive(new AsyncCallback(OnReceive), null);
    }

    private void Update()
    {
        if (endPoint.Address != IPAddress.Any)
        {
            time += Time.deltaTime;
            if (time > TIMEOUT)
            {
                endPoint = new IPEndPoint(IPAddress.Any, 0);
                clientDiscovery.Reset();
                Debug.Log("Reset connection to client");
            }    
        }    
    }

    void OnReceive(IAsyncResult result)
    {
        byte[] receivedData = udpClient.EndReceive(result, ref endPoint);
        string serverMessage = Encoding.UTF8.GetString(receivedData);
        InputMessage message = JsonUtility.FromJson<InputMessage>(serverMessage);
        UpdateInput(message);
        udpClient.BeginReceive(new AsyncCallback(OnReceive), null); // Listen for next broadcast
        time = 0;
    }

    void OnApplicationQuit()
    {
        udpClient.Close();
    }

    private void UpdateInput(InputMessage message)
    {
        ThrottlePressed = message.ThrottlePressed;
        BrakePressed = message.BrakePressed;
        Gear = message.Gear;
        WheelInput = message.WheelInput;
    }
}