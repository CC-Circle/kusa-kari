using UnityEngine;

public class BGMManager : MonoBehaviour
{
    [SerializeField] private AudioClip initialBGM;    // 最初に再生するBGM
    [SerializeField] private AudioClip countdownBGM;  // カウントダウンBGM
    [SerializeField] private AudioClip mainBGM;       // メインBGM

    [SerializeField] private AudioClip catBGMClip;  // catBGMのAudioClip
    private AudioSource audioSource;

    void Start()
    {
        // このオブジェクトにアタッチされているAudioSourceを取得
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = initialBGM;  // 初期状態で最初のBGMを設定
        audioSource.Play();
    }

    void Update()
    {
        // スペースキーが押されたときに現在のオーディオを停止し、カウントダウンBGMを再生
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
                PlayCountdownBGM();
            }
        }
    }

    private void PlayCountdownBGM()
    {
        // カウントダウンBGMを設定し、再生
        audioSource.clip = countdownBGM;
        audioSource.Play();
        StartCoroutine(WaitForCountdownToEnd());
    }

    private System.Collections.IEnumerator WaitForCountdownToEnd()
    {
        // カウントダウンBGMの再生終了を待ってメインBGMを再生
        yield return new WaitForSeconds(countdownBGM.length);
        PlayMainBGM();
    }

    private void PlayMainBGM()
    {
        // メインBGMを設定し、再生
        audioSource.clip = mainBGM;
        audioSource.Play();
    }

    // catBGMを再生するメソッド
    public void catBGM()
    {
        if (catBGMClip != null)
        {
            audioSource.PlayOneShot(catBGMClip);
        }
        else
        {
            Debug.LogWarning("catBGMClip is not assigned!");
        }
    }
}
