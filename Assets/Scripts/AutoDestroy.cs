using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float delay = 0.2f;  // 自動削除までの遅延時間（秒）

    void Start()
    {
        // 1秒後に自身を削除
        Destroy(gameObject, delay);
    }
}
