using UnityEngine;
using TMPro; // TextMeshProを使うために追加
using System.IO; // ファイル操作のために追加
using UnityEngine.SceneManagement; // シーン管理のために追加
using System.Collections; // コルーチンを使うために追加

public class TimeManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timerText; // 残り時間を表示するTextMeshProUGUI
    private float totalTime = 5.0f; // 60秒のタイマー
    private string formattedTime;

    [SerializeField]
    private GameObject managersObject; // Managersオブジェクト
    [SerializeField]
    private GameObject finishObject; // Finishオブジェクト

    private ScoreCount scoreCount; // ScoreCountの参照

    void Awake()
    {
        // ScoreCountコンポーネントを取得
        scoreCount = FindObjectOfType<ScoreCount>();

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

                // Managersオブジェクトの中身のスクリプトを無効化
                if (managersObject != null)
                {
                    MonoBehaviour[] managerScripts = managersObject.GetComponentsInChildren<MonoBehaviour>();
                    foreach (var script in managerScripts)
                    {
                        script.enabled = false; // スクリプトを無効化
                    }
                }

                // Finishオブジェクトを有効化
                if (finishObject != null)
                {
                    finishObject.SetActive(true); // Finishオブジェクトを有効化
                }

                // スコアを書き込み処理を呼び出し
                SaveScoreToCSV();
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

    // スコアをCSVファイルに保存し、3秒後にシーンを移動
    private void SaveScoreToCSV()
    {
        if (scoreCount != null)
        {
            string path = Path.Combine(Application.dataPath, "Scores.csv");
            int currentScore = scoreCount.score; // ScoreCountからスコアを取得

            // CSVファイルに書き込み
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine("Score," + currentScore);
            }

            Debug.Log("スコア: " + currentScore);

            // 3秒後にシーンを移動するコルーチンを開始
            StartCoroutine(WaitAndLoadScene());
        }
        else
        {
            Debug.LogError("ScoreCountが見つかりませんでした。");
        }
    }

    // 3秒待機してからシーンを移動するコルーチン
    private IEnumerator WaitAndLoadScene()
    {
        yield return new WaitForSeconds(3); // 3秒待機
        SceneManager.LoadScene("rank"); // rankシーンに移動
    }
}
