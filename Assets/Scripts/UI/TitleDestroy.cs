using UnityEngine;

public class TitleDestroy : MonoBehaviour
{
    [SerializeField]
    private GameObject startCountObject; // StartCountスクリプトがアタッチされたオブジェクト

    private GameObject managersObject;

    void Awake()
    {
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

    void Update()
    {
        // スペースキーが押された場合に処理を実行
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DestroyTitleAndActivateStartCount(); // 自身を破壊し、StartCountを有効化
        }
    }

    void DestroyTitleAndActivateStartCount()
    {
        // 自身を破壊
        Destroy(gameObject);

        // StartCountスクリプトを有効化
        if (startCountObject != null)
        {
            StartCount startCount = startCountObject.GetComponent<StartCount>();
            if (startCount != null)
            {
                startCount.enabled = true; // StartCountスクリプトを有効化
            }
            else
            {
                Debug.LogWarning("StartCount script not found on the specified object.");
            }
        }
        else
        {
            Debug.LogWarning("StartCount object not assigned.");
        }
    }
}
