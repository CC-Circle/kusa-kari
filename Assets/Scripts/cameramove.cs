using UnityEngine;
using System;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1f;

    [SerializeField]
    private float moveDistance = 1f;

    private bool isMoving = false;

    public bool IsMoving => isMoving;  // 外部から移動状態を確認できるプロパティ

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
        Vector3 targetPosition = startPosition + new Vector3(0, 0, moveDistance);
        float elapsedTime = 0f;
        float duration = 1f / moveSpeed;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        isMoving = false;
    }
}