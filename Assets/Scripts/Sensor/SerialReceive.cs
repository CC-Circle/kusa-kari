using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialReceive : MonoBehaviour
{
    public SerialHandler serialHandler;

    public int Flag_view = 0;
    public int Flag_button = 0;
    public int left_count = 0;
    public int right_count = 0;

    void Start()
    {
        serialHandler.OnDataReceived += OnDataReceived;
    }

    // Arduinoから受信したデータを処理する
    void OnDataReceived(string message)
    {
        // 受信したデータをデバッグログに表示
        // Debug.Log($"Received data: {message}");
        // Debug.Log($"Flag_view: {Flag_view}");
        // Debug.Log($"left_count: {left_count}");
        // Debug.Log($"right_count: {right_count}");
        try
        {
            // 受信したデータをint型に変換
            int flag = int.Parse(message);

            if (flag == 1) // 左回転のフラグ
            {
                left_count++;
                if (left_count > 5)
                {
                    Flag_view = 1;
                    left_count = 0;
                }
            }
            else if (flag == 2) // 右回転のフラグ
            {
                right_count++;
                if (right_count > 5)
                {
                    Flag_view = 2;
                    right_count = 0;
                }
            }
            else if (flag == 0) // 停止のフラグ
            {
                Flag_view = 0;
            }
            else if (flag == 10)
            {
                Flag_button = 1;
            }
            else if (flag == -1)
            {
                Flag_button = 0;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning($"Error parsing data: {e.Message}");
        }
    }
}
