using UnityEngine;
using TMPro;

public class ScoreCount : MonoBehaviour
{
    public int score = 0;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    private void Start()
    {
        score = 0;
        UpdateScoreText();
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = score.ToString() + "g";
    }
}
