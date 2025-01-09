using UnityEngine;

public class GameManager : MonoBehaviour
{
    // KusaGridGeneratorから取得するための2次元配列
    private (int, int)[,] kusaGrid;

    // 現在のZ軸列を格納する配列
    private int[] zColumn = new int[5];
    // 現在のZ軸HP配列を格納する配列
    private int[] HPColumn = new int[5];


    // 各位置の草のHPを格納する2次元配列
    private int[,] kusaHP = new int[100, 5];
    // カメラを動かすためのスクリプト参照
    private CameraMove cameraMove;
    // 現在のZ軸インデックス
    private int currentZIndex = 0;
    // KusaGridGeneratorの参照
    private KusaGridGenerator kusaGridGenerator;
    // 移動待機状態のフラグ（現在未使用）
    private bool isWaitingForMove = false;

    void Start()
    {
        kusaGridGenerator = FindObjectOfType<KusaGridGenerator>();

        if (kusaGridGenerator != null)
        {
            kusaGrid = kusaGridGenerator.kusaGrid; // kusaGridの初期化
            kusaHP = kusaGridGenerator.kusaHP; // kusaHPの参照を取得
        }
        else
        {
            Debug.LogError("KusaGridGenerator not found.");
        }

        InitializeZColumn(currentZIndex);

        cameraMove = Camera.main.GetComponent<CameraMove>();
        if (cameraMove == null)
        {
            Debug.LogError("CameraMove script not found on the Main Camera.");
        }

        

        LogKusaHP();


        // 現在のZインデックスに基づいてzColumnを初期化
        InitializeZColumn(currentZIndex);

        // CameraMoveスクリプトの参照を取得
        cameraMove = Camera.main.GetComponent<CameraMove>();
        if (cameraMove == null)
        {
            Debug.LogError("CameraMove script not found on the Main Camera.");
        }
    }

    void Update()
    {
        // カメラが移動中であれば、処理をスキップ
        if (cameraMove != null && cameraMove.IsMoving)
        {
            return;
        }

        ///***柴田用
        ///***この下がシグナルを送信してる部分です。
        
        // キー入力に応じてzColumnの信号を処理
        if (Input.GetKeyDown(KeyCode.Alpha1)) ReceiveSignal(zColumn[0]);
        if (Input.GetKeyDown(KeyCode.Alpha2)) ReceiveSignal(zColumn[1]);
        if (Input.GetKeyDown(KeyCode.Alpha3)) ReceiveSignal(zColumn[2]);
        if (Input.GetKeyDown(KeyCode.Alpha4)) ReceiveSignal(zColumn[3]);
        if (Input.GetKeyDown(KeyCode.Alpha5)) ReceiveSignal(zColumn[4]);

        ///***とりあえずこの中で[1]と[3]をセンサーに変えてみて欲しい
        ///***プログラムがクチャクチャだから今回は延命で
        ///***あと出来そうならBGMとSE適当でいいからぶち込んで
        
        // zColumnが全てゼロなら、カメラを進めて次のZインデックスを設定
        if (IsZColumnAllZero())
        {
            cameraMove?.MoveForward();

            if (currentZIndex < kusaGrid.GetLength(0) - 1)
            {
                currentZIndex++;
                InitializeZColumn(currentZIndex); // 次のzColumnを初期化
            }
        }
    }

    // 指定されたZインデックスに基づいてzColumnを初期化
    void InitializeZColumn(int zIndex)
    {
        for (int x = 0; x < 5; x++)
        {
            zColumn[x] = kusaGrid[zIndex, x].Item2; // kusaGridから対応する値を取得
            HPColumn[x] = kusaHP[zIndex,x]; // kusaHPから取得
        }
    }

    // zColumnの信号を受け取り、対応するオブジェクトを破壊してスコアを加算
    // Signalの処理
    void ReceiveSignal(int signal)
    {
        if (kusaGrid == null)
        {
            Debug.LogError("kusaGrid is not initialized.");
            return;
        }

        if (kusaHP == null)
        {
            Debug.LogError("kusaHP is not initialized.");
            return;
        }

        if (zColumn[signal] == signal)
        {
            GameObject objToDelete = GameObject.Find($"z{currentZIndex}x{signal}");
            if (objToDelete != null)
            {   
                //Debug.Log("tuuka");
                thisDestroy destroyScript = objToDelete.GetComponent<thisDestroy>();

                if (destroyScript != null)
                {
                    destroyScript.DestroyObjectAndAddScore();
                    
                }
                else
                {
                    Debug.LogWarning($"No thisDestroy script found on {objToDelete.name}");
                }
            }
            else
            {
                Debug.Log($"Object z{currentZIndex}x{signal} not found.");
            }

            if(HPColumn[signal]  > 0)//草が存在する時
            {
                //Debug.Log("削除機能は実行しています");
                HPColumn[signal] = HPColumn[signal] - 1; // HPを減らす
            }

            // zColumnの内容をデバッグ表示
            for (int i = 0; i < 5; i++)
            {
                //Debug.Log($"HPColumn[{i}] = {HPColumn[i]}");
                Debug.Log($"zColumn[{i}] = {zColumn[i]}");
            }
            return; // 処理終了
        }

        Debug.LogWarning($"Signal {signal} not found in kusaGrid.");
    }


    // zColumnがすべてゼロかどうかをチェック
    bool IsZColumnAllZero()
    {
        for (int i = 0; i < 5; i++)
        {
            if (HPColumn[i] != 0)
            {
                return false; // ゼロでない要素があればfalse
            }
        }
        return true; // 全てゼロならtrue
    }

    void LogKusaHP()
    {
        for (int z = 0; z < kusaHP.GetLength(0); z++)
        {
            for (int x = 0; x < kusaHP.GetLength(1); x++)
            {
                //Debug.Log($"kusaHP[{z}, {x}] = {kusaHP[z, x]}");
            }
        }
    }
}
