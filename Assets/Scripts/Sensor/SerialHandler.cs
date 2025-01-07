using UnityEngine;
using System.IO.Ports;
using System.Threading;

public class SerialHandler : MonoBehaviour
{
    // デリゲート型を宣言し、シリアルデータ受信イベントを定義します
    public delegate void SerialDataReceivedEventHandler(string message);
    public event SerialDataReceivedEventHandler OnDataReceived;

    // シリアルポート名
    // ls /dev/tty* | grep usb
    [SerializeField] private string portName = "/dev/cu.wchusbserial55910045011";
    public int baudRate = 115200; // ボーレート（通信速度）

    private SerialPort serialPort; // シリアルポートのインスタンス

    //M5Stackgaつながっているかのフラグ
    public bool Settingsflag;

    // MonoBehaviourのAwakeメソッドはオブジェクトの初期化時に呼ばれます
    private void Awake()
    {
        Open(); // シリアルポートを開く
    }

    // MonoBehaviourのOnDestroyメソッドはオブジェクトが破棄されるときに呼ばれます
    private void OnDestroy()
    {
        Close(); // シリアルポートを閉じる
    }

    // シリアルポートを開くメソッド
    private void Open()
    {
        try
        {
            serialPort = new SerialPort(portName, baudRate); // シリアルポートを初期化
            serialPort.Open(); // シリアルポートを開く
            serialPort.DiscardInBuffer(); // 受信バッファをクリア
            serialPort.DiscardOutBuffer(); // 送信バッファをクリア
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("Failed to open serial port: " + e.Message); // エラーメッセージを表示
            return;
        }

        if (serialPort.IsOpen)
        {
            Debug.Log("Serial port opened successfully."); // 成功メッセージを表示
        }
        else
        {
            Debug.LogError("Failed to open serial port."); // 失敗メッセージを表示
        }

        // シリアルポートからのデータ読み取りを別のスレッドで開始
        Thread thread = new Thread(Read);
        thread.Start();
    }

    // シリアルポートを閉じるメソッド
    private void Close()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close(); // シリアルポートを閉じる
            serialPort.Dispose(); // シリアルポートを解放
            Debug.Log("Serial port closed."); // 閉じたことを確認
        }
    }

    // シリアルポートからデータを読み取るメソッド
    private void Read()
    {
        while (serialPort != null && serialPort.IsOpen)
        {
            try
            {
                string message = serialPort.ReadLine(); // データを一行読み取る
                OnDataReceived?.Invoke(message); // データ受信イベントを発火
                Settingsflag = true;//つながっている
            }
            catch (System.Exception e)
            {
                Debug.LogWarning("Error reading from serial port: " + e.Message); // エラーメッセージを表示
                Settingsflag = false;//つながっていないのでマウスに切り替え
            }
        }
    }

    // シリアルポートにデータを書き込むメソッド
    public void Write(string message)
    {
        try
        {
            serialPort.Write(message); // メッセージを書き込む 
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error writing to serial port: " + e.Message); // エラーメッセージを表示
        }
    }
}
