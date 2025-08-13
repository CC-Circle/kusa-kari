using UnityEngine;

public class BugMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.0f; // 移動速度（単位: m/s）

    void Update()
    {
        // 正面方向（Z軸方向）に進む
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
