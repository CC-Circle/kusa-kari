using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_Left : MonoBehaviour
{
    public SH_Left serialHandler;

    // 左の判定フラグ
    public bool Left_Flag = false;

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
        // Mキーで振動フラグを切り替える
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (Left_Flag)
            {
                Left_Flag = false;
            }
            else
            {
                Left_Flag = true;
            }
        }

        // Debug
        //Debug.Log(Left_Flag);
    }

    // Arduinoから受信したデータを処理する
    void OnDataReceived(string message)
    {
        try
        {
            if (message.StartsWith("{") && message.Contains("\"vibration\":true"))
            {
                if (Left_Flag)
                {
                    Left_Flag = false;
                }
                else
                {
                    Left_Flag = true;
                }
            }
            else
            {
                // 加速度取得用の処理
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
