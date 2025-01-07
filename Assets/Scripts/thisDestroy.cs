using UnityEngine;

public class thisDestroy : MonoBehaviour
{
    public int scoreToAdd = 100;  // 加算するスコア
    private ScoreCount scoreManager;  // ScoreCountスクリプトを参照するための変数

    // ゲームが開始された時にScoreManagerを自動で設定
    void Start()
    {
        // ScoreManagerオブジェクトをシーン内で検索して取得
        scoreManager = FindObjectOfType<ScoreCount>();

        // ScoreManagerが見つからない場合は警告を表示
        if (scoreManager == null)
        {
            Debug.LogWarning("ScoreManager not found in the scene!");
        }
    }

    // オブジェクトを破壊し、スコアを追加するメソッド
    public void DestroyObjectAndAddScore()
    {
        if (scoreManager != null)
        {
            scoreManager.AddScore(scoreToAdd);  // スコアを加算
        }

        // オブジェクトを破壊
        Destroy(gameObject);
    }
}
