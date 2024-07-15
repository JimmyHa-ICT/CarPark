using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System;

public class ServerDiscovery : MonoBehaviour
{
    private UdpClient udpClient;
    private UdpClient udpClientListener;
    private IPEndPoint endPoint;
    private IPEndPoint endPointListener;
    private byte[] responseData;

    private bool isDiscovering;
    [SerializeField] private PlayerInput sender;
    private Coroutine broadcastCoroutine;

    void Start()
    {
        udpClient = new UdpClient();
        udpClient.EnableBroadcast = true;
        endPoint = new IPEndPoint(IPAddress.Broadcast, 47777); // Port for discovery
        responseData = Encoding.UTF8.GetBytes(SystemInfo.deviceName);
        broadcastCoroutine = StartCoroutine(IBroadCast());

        udpClientListener = new UdpClient(47778);
        udpClientListener.BeginReceive(new AsyncCallback(OnReceiveACK), null);

        //InvokeRepeating(nameof(Broadcast), 0f, 1f); // Broadcast every second
    }

    private IEnumerator IBroadCast()
    {
        isDiscovering = true;
        while (isDiscovering)
        {
            yield return new WaitForSeconds(1);
            Broadcast();
        }
        sender.endPoint = new IPEndPoint(endPointListener.Address, sender.PORT);
        sender.gameObject.SetActive(true);
    }

    void Broadcast()
    {
        Debug.Log("Finding server");
        udpClient.Send(responseData, responseData.Length, endPoint);
    }

    private void OnReceiveACK(IAsyncResult result)
    {
        byte[] receivedData = udpClient.EndReceive(result, ref endPointListener);
        string serverMessage = Encoding.UTF8.GetString(receivedData);
        Debug.Log("Receive ack " + serverMessage);
        try
        {
            isDiscovering = false;
            //StopCoroutine(broadcastCoroutine);
            //sender.endPoint = new IPEndPoint(endPointListener.Address, sender.PORT);
            //sender.gameObject.SetActive(true);
        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
        }
    }

    void OnApplicationQuit()
    {
        udpClient.Close();
    }
}
