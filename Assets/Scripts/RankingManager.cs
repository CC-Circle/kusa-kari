using UnityEngine;
using TMPro;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class RankingManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI leftRankingText;
    [SerializeField]
    private TextMeshProUGUI rightRankingText;

    private string filePath;
    private int currentScore = -1; // 今回のスコア

    void Start()
    {
        filePath = Path.Combine(Application.dataPath, "Scores.csv");

        // 今回のスコアを取得
        currentScore = GetCurrentScore();

        DisplayTopScores();
    }

    private void DisplayTopScores()
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError("Scores.csvが見つかりません。");
            return;
        }

        List<int> scores = new List<int>();

        // CSV読み込み
        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(',');
                if (parts.Length > 1 && int.TryParse(parts[1], out int score))
                {
                    scores.Add(score);
                }
            }
        }

        // 上位10件
        scores = scores
            .OrderByDescending(score => score)
            .Take(10)
            .ToList();

        string leftRanking = "\n";
        string rightRanking = "\n";

        for (int i = 0; i < scores.Count; i++)
        {
            bool isCurrentScore = (scores[i] == currentScore);

            string scoreText = isCurrentScore
                ? $"<color=yellow>{i + 1}: {scores[i]}g</color>\n"
                : $"{i + 1}: {scores[i]}g\n";

            if (i < 5)
                leftRanking += scoreText;
            else
                rightRanking += scoreText;
        }

        leftRankingText.richText = true;
        rightRankingText.richText = true;

        leftRankingText.text = leftRanking;
        rightRankingText.text = rightRanking;
    }

    // =========================
    // 今回のスコア取得処理
    // =========================

    private int GetCurrentScore()
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError("Scores.csvが見つかりません。");
            return -1;
        }

        string lastLine = GetLastLineOfCSV(filePath);

        if (string.IsNullOrEmpty(lastLine))
        {
            Debug.LogError("CSVの最終行が取得できません。");
            return -1;
        }

        return ExtractScore(lastLine);
    }

    // CSVの最後の行を取得
    private string GetLastLineOfCSV(string path)
    {
        string lastLine = null;

        using (var reader = new StreamReader(path))
        {
            while (!reader.EndOfStream)
            {
                lastLine = reader.ReadLine();
            }
        }

        return lastLine;
    }

    // CSV1行からスコアを取り出す
    private int ExtractScore(string line)
    {
        string[] parts = line.Split(',');

        if (parts.Length > 1 && int.TryParse(parts[1], out int score))
        {
            return score;
        }

        return -1;
    }
}
