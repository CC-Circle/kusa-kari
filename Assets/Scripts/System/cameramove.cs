using UnityEngine;
using System;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0.5f;

    [SerializeField]
    private float moveDistance = 1f;

    private bool isMoving = false;

    public bool IsMoving => isMoving;

    public void MoveForward()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveForwardCoroutine());
        }
    }

    private System.Collections.IEnumerator MoveForwardCoroutine()
    {
        isMoving = true;

        Vector3 startPosition = transform.position;
        Debug.Log("現在座標： "+startPosition);
        Vector3 targetPosition = startPosition + new Vector3(0, 0, moveDistance);
        Debug.Log("次点座標： "+targetPosition);
        float duration = 1f / moveSpeed;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration); // 常に0〜1の範囲に制限
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        // 最後に正確にターゲット位置に合わせる
        transform.position = targetPosition;
        isMoving = false;
    }
}
