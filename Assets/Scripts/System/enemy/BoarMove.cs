using UnityEngine;

public class BoarMove : MonoBehaviour
{
    private float moveSpeed = 10.5f;    // 移動速度
    private float startDistance = 8.0f; // 動き出す距離（XZ平面上）

    private Transform mainCamera;

    void Start()
    {
        // メインカメラ取得
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

        // XZ平面での距離計算（Y軸無視）
        Vector3 boarPos = transform.position;
        Vector3 camPos = mainCamera.position;
        boarPos.y = 0f;
        camPos.y = 0f;

        float distance = Vector3.Distance(boarPos, camPos);

        // 距離が一定以下なら前進
        if (distance <= startDistance)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }
}
