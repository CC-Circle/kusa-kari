using UnityEngine;
using TMPro;  // TextMeshProを使うために必要

public class ScoreCount : MonoBehaviour
{
    public int score = 0;  // 現在のスコア
    [SerializeField] 
    private TextMeshProUGUI scoreText;  // スコア表示用のTextMeshProUGUI

    // スコアを加算するメソッド
    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;  // スコアを加算
        UpdateScoreText();  // スコア表示を更新
    }

    // スコアのテキストを更新するメソッド
    private void UpdateScoreText()
    {
        scoreText.text = score.ToString() + "g";  // スコアを表示
    }

    // ゲーム開始時にスコアを初期化
    private void Start()
    {
        score = 0;  // 初期スコア
        UpdateScoreText();  // 最初のスコアを表示
    }
}
