using UnityEngine;

public class GameManager : MonoBehaviour
{
    private (int, int)[,] kusaGrid = new (int, int)[50, 5]; // 縦50、横5のタプル型配列
    private (int, int)[] zColumn = new (int, int)[5]; // z0x0〜z0x4 を格納する配列
    private CameraMove cameraMove;
    private int currentZIndex = 0; // 現在処理中のz列
    private bool isWaitingForMove = false; // 移動完了を待機するフラグ

    void Start()
    {
        InitializeKusaGrid();
        InitializeZColumn(currentZIndex);
        PrintKusaGrid();

        cameraMove = Camera.main.GetComponent<CameraMove>();
        if (cameraMove == null)
        {
            Debug.LogError("CameraMove script not found on the Main Camera.");
        }
    }

    void Update()
    {
        // カメラの移動中は処理を中断
        if (cameraMove != null && cameraMove.IsMoving)
        {
            return; // カメラが移動している間、処理を中断
        }

        // キー入力による信号処理
        if (Input.GetKeyDown(KeyCode.Alpha1)) ReceiveSignal(zColumn[0]);
        if (Input.GetKeyDown(KeyCode.Alpha2)) ReceiveSignal(zColumn[1]);
        if (Input.GetKeyDown(KeyCode.Alpha3)) ReceiveSignal(zColumn[2]);
        if (Input.GetKeyDown(KeyCode.Alpha4)) ReceiveSignal(zColumn[3]);
        if (Input.GetKeyDown(KeyCode.Alpha5)) ReceiveSignal(zColumn[4]);

        if (IsZColumnAllZero())
        {
            Debug.Log($"All elements in z{zColumn[0].Item1}x{zColumn[0].Item2} column are zero.");
            cameraMove?.MoveForward();  // カメラを前進させる

            if (currentZIndex < 99)
            {
                currentZIndex++;
                InitializeZColumn(currentZIndex);
                Debug.Log($"Moving to the next column: z{currentZIndex}.");
            }
        }
    }

    void InitializeKusaGrid()
    {
        for (int z = 0; z < kusaGrid.GetLength(0); z++)
        {
            for (int x = 0; x < kusaGrid.GetLength(1); x++)
            {
                kusaGrid[z, x] = (z, x); // タプルとして配列に格納
            }
        }
    }

    void InitializeZColumn(int zIndex)
    {
        for (int x = 0; x < 5; x++)
        {
            zColumn[x] = (zIndex, x); // z0x0〜z0x4 を格納
        }
    }

    void PrintKusaGrid()
    {
        for (int z = 0; z < kusaGrid.GetLength(0); z++)
        {
            for (int x = 0; x < kusaGrid.GetLength(1); x++)
            {
                //Debug.Log($"z{z}x{x}: ({kusaGrid[z, x].Item1}, {kusaGrid[z, x].Item2})"); // タプルの内容をデバッグ出力
            }
        }
    }

    void ReceiveSignal((int, int) signal)
    {
        // 信号を解析して該当する位置の配列を 0 に変更
        for (int z = 0; z < kusaGrid.GetLength(0); z++)
        {
            for (int x = 0; x < kusaGrid.GetLength(1); x++)
            {
                if (kusaGrid[z, x] == signal)
                {
                    // Debug.Log($"z{z}x{x}");
                    // オブジェクト名に基づいて該当するオブジェクトを削除
                    GameObject objToDelete = GameObject.Find($"z{z}x{x}");
                    if (objToDelete != null)
                    {
                        Destroy(objToDelete); // オブジェクトを削除
                        // Debug.Log($"Object {objToDelete.name} deleted.");
                    }

                    // 該当する配列の要素を 0 に設定
                    kusaGrid[z, x] = (0, 0); // (0,0) タプルを設定
                    
                    // Debug.Log($"Signal received: ({signal.Item1}, {signal.Item2}). Updated kusaGrid[{z}, {x}] to (0, 0).");
                    return;
                }
            }
        }

        Debug.LogWarning($"Signal ({signal.Item1}, {signal.Item2}) not found in kusaGrid.");
    }

    bool IsZColumnAllZero()
    {
        for (int i = 0; i < 5; i++)
        {
            // zColumn 配列のすべての要素が (0, 0) であれば true を返す
            if (kusaGrid[zColumn[i].Item1, zColumn[i].Item2] != (0, 0))
            {
                return false;
            }
        }
        return true; // zColumn の全てが(0,0)の場合
    }

    void OnCameraMoveComplete()
    {
        // カメラの移動が完了したら次の処理を開始
        isWaitingForMove = false;
    }
}
