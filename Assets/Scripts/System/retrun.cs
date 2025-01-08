using UnityEngine;
using UnityEngine.SceneManagement; // シーン管理のために追加

public class ReturnScript : MonoBehaviour
{
    void Update()
    {
        // スペースキーが押されたときにメインシーンに戻る
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("main"); // "MainScene" をメインシーンの名前に置き換えてください
        }
    }
}
