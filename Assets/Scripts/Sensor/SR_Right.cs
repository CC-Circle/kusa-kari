using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_Right : MonoBehaviour
{
    public SH_Right serialHandler;

    // 右の判定フラグ
    public bool Right_Flag = false;

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
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (Right_Flag)
            {
                Right_Flag = false;
            }
            else
            {
                Right_Flag = true;
            }
        }

        // Debug
        Debug.Log(Right_Flag);
    }

    // Arduinoから受信したデータを処理する
    void OnDataReceived(string message)
    {
        try
        {
            if (message.StartsWith("{") && message.Contains("\"vibration\":true"))
            {
                if (Right_Flag)
                {
                    Right_Flag = false;
                }
                else
                {
                    Right_Flag = true;
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
                // 不要なデータをスキップ
                else if (message.Contains("imu_flag:-1IMU_MPU6886"))
                {
                    return;
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