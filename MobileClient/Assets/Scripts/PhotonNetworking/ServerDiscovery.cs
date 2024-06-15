using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class ServerDiscovery : MonoBehaviour
{
    private UdpClient udpClient;
    private IPEndPoint endPoint;
    private byte[] responseData;

    void Start()
    {
        udpClient = new UdpClient();
        udpClient.EnableBroadcast = true;
        endPoint = new IPEndPoint(IPAddress.Broadcast, 47777); // Port for discovery
        responseData = Encoding.UTF8.GetBytes(SystemInfo.deviceName);

        InvokeRepeating(nameof(Broadcast), 0f, 1f); // Broadcast every second
    }

    void Broadcast()
    {
        Debug.Log("Finding server");
        udpClient.Send(responseData, responseData.Length, endPoint);
    }

    void OnApplicationQuit()
    {
        udpClient.Close();
    }
}
