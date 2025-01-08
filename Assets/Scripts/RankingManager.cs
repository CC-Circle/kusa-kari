using UnityEngine;
using TMPro; // TextMeshProを使うために追加
using System.IO; // ファイル操作のために追加
using System.Collections.Generic;
using System.Linq;

public class RankingManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI leftRankingText; // 左側のランキング表示用TextMeshProUGUI
    [SerializeField]
    private TextMeshProUGUI rightRankingText; // 右側のランキング表示用TextMeshProUGUI
    private string filePath;

    void Start()
    {
        filePath = Path.Combine(Application.dataPath, "Scores.csv"); // CSVファイルのパス
        DisplayTopScores(); // ランキングを表示
    }

    private void DisplayTopScores()
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError("Scores.csvが見つかりません。");
            return;
        }

        List<int> scores = new List<int>();

        // CSVファイルを読み込む
        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(',');
                if (parts.Length > 1 && int.TryParse(parts[1], out int score))
                {
                    scores.Add(score); // スコアをリストに追加
                }
            }
        }

        // スコアを昇順で並び替え
        scores = scores.OrderByDescending(score => score).Take(10).ToList();

        // 上位10個のスコアを表示
        string leftRanking = "Rankings\n";
        string rightRanking = "\n";

        for (int i = 0; i < scores.Count; i++)
        {
            if (i < 5)
            {
                leftRanking += $"{i + 1}: {scores[i]}\n";
            }
            else
            {
                rightRanking += $"{i + 1}: {scores[i]}\n";
            }
        }

        leftRankingText.text = leftRanking;
        rightRankingText.text = rightRanking;
    }
}
