using UnityEngine;
using TMPro; // TextMeshProを使うために追加

public class StartCount : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI countdownText; // カウントダウンを表示するTextMeshProUGUI
    private bool isCounting = false; // カウントダウン中かどうかのフラグ
    private string[] countdownMessages = { "3", "2", "1", "Start!!" }; // 3秒のカウントダウンメッセージ

    private GameObject managersObject;
    [SerializeField]
    private GameObject timeManagerObject; // TimeManagerオブジェクト

    void Awake()
    {
        // 最初はスクリプトを無効化
        this.enabled = false; // 自身のスクリプトを無効化
    }

    void OnEnable()
    {
        // スクリプトが有効化されたタイミングでカウントダウンを開始
        if (!isCounting) // カウントダウンがまだ行われていなければ開始
        {
            StartCountdown();
        }
    }

    // ゲーム開始のサインを受け取ってカウントダウンを開始
    public void StartCountdown()
    {
        isCounting = true; // カウントダウン開始状態

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

        StartCoroutine(CountdownCoroutine()); // カウントダウンを開始
    }

    private System.Collections.IEnumerator CountdownCoroutine()
    {
        for (int i = 0; i < countdownMessages.Length; i++)
        {
            countdownText.text = countdownMessages[i]; // TextMeshProにカウントダウンメッセージを表示
            yield return new WaitForSeconds(1f); // 1秒待機（各メッセージ表示後）
        }

        // カウントダウンが終わったら、Managersオブジェクトのスクリプトを再有効化
        if (managersObject != null)
        {
            MonoBehaviour[] managerScripts = managersObject.GetComponentsInChildren<MonoBehaviour>();
            foreach (var script in managerScripts)
            {
                script.enabled = true; // スクリプトを有効化
            }
            // TimeManagerを有効化
        if (timeManagerObject != null)
        {
            TimeManager timeManager = timeManagerObject.GetComponent<TimeManager>();
            if (timeManager != null)
            {
                timeManager.enabled = true; // TimeManagerを有効化
            }
            else
            {
                Debug.LogWarning("TimeManager script not found on the specified object.");
            }
        }
        else
        {
            Debug.LogWarning("TimeManager object not assigned.");
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
