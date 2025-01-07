using UnityEngine;
using TMPro; // TextMeshProを使うために追加

public class StartCount : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI countdownText; // カウントダウンを表示するTextMeshProUGUI
    private bool isCounting = true; // カウントダウン中かどうかのフラグ
    private float countdownTime = 3f; // カウントダウン開始の時間（3秒から）
    private string[] countdownMessages = { "3", "2", "1", "Start!!" }; // 表示するメッセージ

    private GameObject managersObject;

    void Start()
    {
        // スクリプトを開始時に無効化
        this.enabled = false; // 自身のスクリプトを無効化

        // Managersオブジェクトを探してその子のスクリプトを無効化
        managersObject = GameObject.Find("Managers");

        if (managersObject != null)
        {
            MonoBehaviour[] managerScripts = managersObject.GetComponentsInChildren<MonoBehaviour>();
            foreach (var script in managerScripts)
            {
                script.enabled = false; // スクリプトを無効化
            }
        }
    }

    // ゲーム開始のサインを受け取ってカウントダウンを開始
    public void StartCountdown()
    {
        StartCoroutine(CountdownCoroutine()); // カウントダウンを開始
    }

    private System.Collections.IEnumerator CountdownCoroutine()
    {
        for (int i = 0; i < countdownMessages.Length; i++)
        {
            countdownText.text = countdownMessages[i]; // TextMeshProにカウントダウンメッセージを表示
            yield return new WaitForSeconds(1f); // 1秒待機
        }

        // カウントダウンが終わったら、Managersオブジェクトのスクリプトを再有効化
        if (managersObject != null)
        {
            MonoBehaviour[] managerScripts = managersObject.GetComponentsInChildren<MonoBehaviour>();
            foreach (var script in managerScripts)
            {
                script.enabled = true; // スクリプトを有効化
            }
        }

        countdownText.text = ""; // カウントダウンが終了したらテキストを消す
        isCounting = false; // カウントダウン終了
    }

    public bool IsCounting
    {
        get { return isCounting; } // カウントダウン中かどうかを外部から確認できるようにする
    }
}

