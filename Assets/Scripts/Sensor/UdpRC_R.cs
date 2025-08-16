using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class UdpRC_R : MonoBehaviour
{
    
    public  UdpHD_R udpHandler; // UdpHandlerのインスタンス
    public int imu;
    public string R_message;

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
        // int型に変換してimuに格納
        int.TryParse(message, out int value);
        imu = value;

        // 受信したメッセージを格納
        R_message = message;

        Debug.Log($"IMU Received: {message}");
    }
}
