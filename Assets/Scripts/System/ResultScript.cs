using UnityEngine;
using System.IO;
using TMPro;  // TextMeshProを使用するために追加
using System.Collections;

public class ScoreProcessor : MonoBehaviour
{
    public GameObject catkusaPrefab;  // プレハブをInspectorで設定
    public GameObject instansPoint;   // 生成する位置の参照
    public TextMeshProUGUI scoreText; // TextMeshProUGUIを使用してUIに表示
    public float spawnInterval = 0.5f;  // 各プレハブ生成の間隔（秒）

    void Start()
    {
        // CSVファイルを読み込み、最後の行を取得
        string path = "Assets/scores.csv";  // CSVファイルのパス
        string lastLine = GetLastLineOfCSV(path);
        
        if (!string.IsNullOrEmpty(lastLine))
        {
            Debug.Log("Last Line: " + lastLine);

            // 最後の行からスコアを取り出して100で割る
            int score = ExtractScore(lastLine);
            int spawnCount = score / 50;

            // 割っていない元のスコアを表示
            DisplayScore(score);

            // `catkusaPrefab`を`spawnCount`回生成
            StartCoroutine(SpawnCatkusaSlowly(spawnCount));
        }
        else
        {
            Debug.LogError("No data found in scores.csv or unable to read the file.");
        }
    }

    // CSVファイルの最後の行を取得するメソッド
    string GetLastLineOfCSV(string filePath)
    {
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            if (lines.Length > 0)
            {
                return lines[lines.Length - 1];  // 最後の行を返す
            }
        }
        return null;
    }

    // スコアを抽出するメソッド
    int ExtractScore(string line)
    {
        string[] parts = line.Split(',');
        if (parts.Length > 1 && int.TryParse(parts[1], out int score))
        {
            return score;
        }
        Debug.LogError("Failed to parse score from line: " + line);
        return 0;
    }

    // 割っていない元のスコアを画面に表示するメソッド
    void DisplayScore(int score)
    {
        // コンソールに出力
        Debug.Log("Original Score: " + score);

        // テキストUIに出力（scoreTextが設定されていれば）
        if (scoreText != null)
        {
            scoreText.text = "Score:" + score.ToString();
        }
        else
        {
            Debug.LogWarning("scoreText is not assigned in the Inspector!");
        }
    }

    // `catkusaPrefab`を指定回数、一定間隔で生成するコルーチン
    IEnumerator SpawnCatkusaSlowly(int count)
    {
        if (catkusaPrefab != null && instansPoint != null)
        {
            Vector3 spawnPosition = instansPoint.transform.position;

            for (int i = 0; i < count; i++)
            {
                Instantiate(catkusaPrefab, spawnPosition, Quaternion.identity);
                yield return new WaitForSeconds(spawnInterval);  // 指定した時間待つ
            }
        }
        else
        {
            Debug.LogError("catkusaPrefab or instansPoint is not assigned.");
        }
    }
}
