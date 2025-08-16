using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class UdpRC_C : MonoBehaviour
{
    
    public  UdpHD_C udpHandler; // UdpHandlerのインスタンス
    public int imu;

    void Start()
    {
        udpHandler.OnDataReceived += OnDataReceived; // データ受信イベントにハンドラを登録
    }

    void Update()
    {
        if (!udpHandler.IsOpen)
        {
            Debug.LogWarning("UDP port is not open.");
        }
    }

    void OnDataReceived(string message)
    {
        message = message.Trim();  // 改行などを除去
        imu = int.Parse(message);

        Debug.Log($"IMU Received: {message}");
    }
}
