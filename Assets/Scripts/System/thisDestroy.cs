using UnityEngine;

public class thisDestroy : MonoBehaviour
{
    public int scoreToAdd = 100;  // 加算するスコア
    private ScoreCount scoreManager;  // ScoreCountスクリプトを参照するための変数
    public GameObject catkusaPrefab;  // catkusaプレハブをInspectorから設定するための変数
    public GameObject grassEffectPrefab;  // grass_effectプレハブをInspectorから設定するための変数
    private Transform dynamicObjectsParent;  // DynamicObjectsオブジェクトを参照する変数

    // ゲームが開始された時にScoreManagerとDynamicObjectsを自動で設定
    void Start()
    {
        // ScoreManagerオブジェクトをシーン内で検索して取得
        scoreManager = FindObjectOfType<ScoreCount>();

        // ScoreManagerが見つからない場合は警告を表示
        if (scoreManager == null)
        {
            Debug.LogWarning("ScoreManager not found in the scene!");
        }

        // DynamicObjectsオブジェクトをシーン内で検索して取得
        GameObject dynamicObjectsObj = GameObject.Find("DynamicObjects");

        if (dynamicObjectsObj != null)
        {
            dynamicObjectsParent = dynamicObjectsObj.transform;  // DynamicObjectsオブジェクトのTransformを取得
        }
        else
        {
            Debug.LogWarning("DynamicObjects object not found in the scene!");
        }
    }

    // オブジェクトを破壊し、スコアを追加するメソッド
    public void DestroyObjectAndAddScore()
    {
        // catkusaプレハブを自分の位置に生成
        if (catkusaPrefab != null)
        {
            // プレハブを生成し、DynamicObjectsオブジェクトの子として設定
            GameObject catkusa = Instantiate(catkusaPrefab, transform.position, Quaternion.identity);
            
            if (dynamicObjectsParent != null)
            {
                catkusa.transform.SetParent(dynamicObjectsParent);  // DynamicObjectsオブジェクトの子に設定
            }
            else
            {
                Debug.LogWarning("DynamicObjects parent is not assigned.");
            }
        }

        // grass_effectプレハブを自分の位置に生成
        if (grassEffectPrefab != null)
        {
            // プレハブを生成し、DynamicObjectsオブジェクトの子として設定
            GameObject grassEffect = Instantiate(grassEffectPrefab, transform.position, Quaternion.identity);
            
            if (dynamicObjectsParent != null)
            {
                grassEffect.transform.SetParent(dynamicObjectsParent);  // DynamicObjectsオブジェクトの子に設定
            }
            else
            {
                Debug.LogWarning("DynamicObjects parent is not assigned.");
            }
        }

        // スコアの加算
        if (scoreManager != null)
        {
            scoreManager.AddScore(scoreToAdd);  // スコアを加算
        }

        // オブジェクトを破壊
        Destroy(gameObject);
    }
}
