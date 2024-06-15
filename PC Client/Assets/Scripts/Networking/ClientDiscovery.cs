using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

public class ClientDiscovery : MonoBehaviour
{
    private UdpClient udpClient;
    private IPEndPoint endPoint;

    public PlayerInput receiver;

    void Start()
    {
        Reset();
    }

    void OnReceive(IAsyncResult result)
    {
        byte[] receivedData = udpClient.EndReceive(result, ref endPoint);
        string serverMessage = Encoding.UTF8.GetString(receivedData);
        //Debug.Log("Discovered server: " + serverMessage + " at " + endPoint.Address.ToString());
        receiver.endPoint = new IPEndPoint(endPoint.Address, receiver.PORT);
        Debug.Log("Connect to " + serverMessage);
        NotificationText.Instance.Play($"Connect to {serverMessage}");
        //udpClient.BeginReceive(new AsyncCallback(OnReceive), null); // Listen for next broadcast
        //NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = endPoint.Address.ToString();
        //NetworkManager.Singleton.StartClient();
    }

    public void Reset()
    {
        udpClient = new UdpClient(47777); // Port for discovery
        endPoint = new IPEndPoint(IPAddress.Any, 0);

        udpClient.BeginReceive(new AsyncCallback(OnReceive), null);
    }

    void OnApplicationQuit()
    {
        udpClient.Close();
    }
}
