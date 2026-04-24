using UnityEngine;
using TMPro;

public class ScoreBalloon : MonoBehaviour
{
    [Header("Score Manager")]
    [SerializeField] private ScoreCount scoreManager;

    [Header("吹き出し")]
    [SerializeField] private GameObject balloonObject;
    [SerializeField] private TextMeshProUGUI balloonText;

    [Header("SE")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip se2000;
    [SerializeField] private AudioClip se5000;
    [SerializeField] private AudioClip se10000;

    private bool is2000Played;
    private bool is5000Played;
    private bool is10000Played;

    private void Start()
    {
        balloonObject.SetActive(false);

        // Debug.Log($"[ScoreBalloon] scoreManager null? {scoreManager == null}");
    }

    private void Update()
    {
        if (scoreManager == null) return;

        int score = scoreManager.score;
        // Debug.Log($"[ScoreBalloon] 参照スコア={score}");

        if (score >= 2000 && !is2000Played)
        {
            ShowBalloon(2000, se2000);
            is2000Played = true;
        }

        if (score >= 5000 && !is5000Played)
        {
            ShowBalloon(5000, se5000);
            is5000Played = true;
        }

        if (score >= 10000 && !is10000Played)
        {
            ShowBalloon(10000, se10000);
            is10000Played = true;
        }
    }

    private void ShowBalloon(int displayScore, AudioClip se)
    {
        Debug.Log($"[ScoreBalloon] SE 再生 {displayScore}");

        balloonText.text = displayScore.ToString()+"!";
        balloonObject.SetActive(true);

        audioSource.PlayOneShot(se);

        CancelInvoke(nameof(HideBalloon));
        Invoke(nameof(HideBalloon), 1.5f);
    }

    private void HideBalloon()
    {
        balloonObject.SetActive(false);
    }
}
