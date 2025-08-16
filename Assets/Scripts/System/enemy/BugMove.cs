using UnityEngine;

public class BugMove : MonoBehaviour
{
    private float moveSpeed = 2.0f;   // 移動速度
    private float startDistance = 20.0f; // 動き出す距離

    private Transform mainCamera;

    void Start()
    {
        // メインカメラを取得
        if (Camera.main != null)
        {
            mainCamera = Camera.main.transform;
        }
        else
        {
            Debug.LogError("Main Camera が見つかりません");
        }
    }

    void Update()
    {
        if (mainCamera == null) return;

        // Y軸を無視して距離を計算
        Vector3 bugPos = transform.position;
        Vector3 camPos = mainCamera.position;
        bugPos.y = 0f;
        camPos.y = 0f;

        float distance = Vector3.Distance(bugPos, camPos);

        // 距離が一定以下なら移動
        if (distance <= startDistance)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }
}
