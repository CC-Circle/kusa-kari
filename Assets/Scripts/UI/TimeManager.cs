using UnityEngine;
using TMPro; // TextMeshProを使うために追加

public class TimeManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timerText; // 残り時間を表示するTextMeshProUGUI
    private float totalTime = 10f; // 60秒のタイマー
    private string formattedTime;

    void Awake()
    {
        // 最初はスクリプト自体を無効化
        this.enabled = false; // TimeManagerスクリプトを無効化
    }

    void Update()
    {
        if (!this.enabled) return; // スクリプトが無効化されている場合は処理をスキップ

        if (totalTime > 0)
        {
            // 時間が残っている場合、タイマーをカウントダウン
            totalTime -= Time.deltaTime;

            // 時間が0になった場合、タイマーを停止
            if (totalTime <= 0)
            {
                totalTime = 0;
                // ゲーム終了処理などを追加できます
            }

            // 残り時間を秒とミリ秒でフォーマットして表示
            formattedTime = FormatTime(totalTime);
            timerText.text = formattedTime; // TextMeshProに時間を表示
        }
    }

    // タイマーの開始
    public void StartTimer()
    {
        this.enabled = true; // スクリプトを有効化
    }

    // 時間を「秒:ミリ秒」形式にフォーマット
    private string FormatTime(float time)
    {
        int seconds = Mathf.FloorToInt(time);
        int milliseconds = Mathf.FloorToInt((time - seconds) * 1000); // ミリ秒を計算

        return string.Format("{0:00}:{1:000}", seconds, milliseconds); // 秒:ミリ秒形式で表示
    }
}
