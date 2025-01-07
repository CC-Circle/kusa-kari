using UnityEngine;

public class MouseMove : MonoBehaviour
{
    [SerializeField] private Transform playerTransform; // プレイヤーのTransform
    // [SerializeField] private float zOffset = 1f;        // カメラからの距離

    void Start()
    {
        // playerTransformが設定されていない場合、シーンから探す
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // "Player"タグを持つオブジェクトを探す

            if (playerTransform == null)
            {
                Debug.LogError("Player Transform not assigned and no object with tag 'Player' found!");
                enabled = false; // スクリプトを無効化
                return;
            }
        }
    }


    public void move()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.WorldToScreenPoint(playerTransform.position).z;  // プレイヤーのZ座標を使用

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.y = playerTransform.position.y; // Y座標は維持

        playerTransform.position = worldPos;
    }
}