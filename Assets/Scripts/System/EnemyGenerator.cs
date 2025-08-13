using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    // 読み込まれたときに自動で実行される
    void Start()
    {
        int enemyType = GenerateEnemy();
        //Debug.Log("生成された敵タイプ: " + enemyType);
    }

    /// <summary>
    /// 10%の確率で敵（1～3の整数）を返す。それ以外は0。
    /// </summary>
    /// <returns>0（なし）、または1～3の敵ID</returns>
    public int GenerateEnemy()
    {
        float randomValue = Random.Range(0f, 1f); // 0.0 ～ 1.0未満
        if (randomValue < 0.1f) // 10%の確率
        {
            return Random.Range(1, 4); // 1 ～ 3 の整数（上限は4未満）
        }
        else
        {
            return 0; // 何も生成されない
        }
    }
}
