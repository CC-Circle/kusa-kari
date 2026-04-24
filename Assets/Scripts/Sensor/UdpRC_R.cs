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

    public float lastReceiveTime = -1f;
    public bool isReceiving = false;

    void Awake()
    {
        udpHandler.OnDataReceived += OnDataReceived; // データ受信イベントにハンドラを登録
    }

    void Update()
    {
        if (lastReceiveTime < 0)
        {
            Debug.Log("UDP: まだ一度も受信していない");
            return;
        }

        float elapsed = Time.time - lastReceiveTime;

        if (elapsed < 0.5f)
        {
            Debug.Log($"UDP: 受信中（{elapsed:F2}s前）");
        }
        else
        {
            isReceiving = false;
            Debug.LogWarning($"UDP: 途切れた（最後 {elapsed:F2}s 前）");
        }
    }


    void OnDataReceived(string message)
    {
        lastReceiveTime = Time.time;
        isReceiving = true;

        int.TryParse(message, out int value);
        imu = value;
        R_message = message;

        Debug.Log($"IMU Received: {message}");
    }

}
