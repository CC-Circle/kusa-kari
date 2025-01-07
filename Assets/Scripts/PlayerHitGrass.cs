// PlayerController.cs
using UnityEngine;

public class PlayerHitGrass : MonoBehaviour
{
    private GameObject player; // プレイヤーオブジェクトを保持する変数

    public void SetPlayer(GameObject playerObject)
    {
        player = playerObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        // player が設定されているか確認
        if (player == null)
        {
            Debug.LogError("Player object is not set in PlayerHitGrass!");
            return;
        }


        // 衝突したオブジェクトが "Grass_0" または "Grass_1" タグを持っているかチェック
        if (other.CompareTag("Grass_0") || other.CompareTag("Grass_1"))
        {
            // GrassHP コンポーネントを取得
            GrassHP grassHP = other.GetComponent<GrassHP>();

            // GrassHP コンポーネントが存在するかチェック
            if (grassHP != null)
            {
                // ダメージを与える
                grassHP.TakeDamage(1);
            }
        }
    }
}