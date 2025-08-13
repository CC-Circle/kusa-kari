using UnityEngine;

public class MoleMovement : MonoBehaviour
{
    private float moveDistance = 1.0f;   // 上に出る距離
    private float moveSpeed = 3.0f;      // 移動速度
    private float minWaitTime = 1.0f;    // 最小待機時間
    private float maxWaitTime = 1.0f;    // 最大待機時間

    private Vector3 startPos;
    private Vector3 upPos;
    private bool isUp = false;
    private bool isMoving = false;

    void Start()
    {
        startPos = transform.position;
        upPos = startPos + Vector3.up * moveDistance;
        StartCoroutine(MoveRoutine());
    }

    private System.Collections.IEnumerator MoveRoutine()
    {
        while (true)
        {
            // 上下の状態を切り替える
            Vector3 targetPos = isUp ? startPos : upPos;
            isMoving = true;

            // 移動処理
            while (Vector3.Distance(transform.position, targetPos) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                yield return null;
            }

            transform.position = targetPos;
            isUp = !isUp;
            isMoving = false;

            // ランダム時間待機
            float waitTime = Random.Range(minWaitTime, maxWaitTime);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
