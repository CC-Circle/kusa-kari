using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialReceive : MonoBehaviour
{
    public SerialHandler serialHandler;

    public bool vibrationFlag = false;

    void Start()
    {
        serialHandler.OnDataReceived += OnDataReceived;
    }

    void Update()
    {
        // SerialPort が開いていない場合のチェック
        if (!serialHandler.IsOpen)
        {
            Debug.LogWarning("Serial port is not open.");
        }

        // Mキーで振動フラグを切り替える
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (vibrationFlag)
            {
                vibrationFlag = false;
            }
            else
            {
                vibrationFlag = true;
            }
        }

        // Debug
        Debug.Log(vibrationFlag);
    }

    // Arduinoから受信したデータを処理する
    void OnDataReceived(string message)
    {
        try
        {
            if (message.StartsWith("{") && message.Contains("\"vibration\":true"))
            {
                if (vibrationFlag)
                {
                    vibrationFlag = false;
                }
                else
                {
                    vibrationFlag = true;
                }
            }
            else
            {
                string[] values = message.Split(',');
                if (values.Length == 3)
                {
                    float accX = float.Parse(values[0]);
                    float accY = float.Parse(values[1]);
                    float accZ = float.Parse(values[2]);
                    Debug.Log($"Acceleration Data: X={accX}, Y={accY}, Z={accZ}");
                }
                else
                {
                    Debug.LogWarning($"Unexpected data format: {message}");
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning($"Error parsing data: {e.Message}");
        }
    }
}
