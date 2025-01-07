using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCount : MonoBehaviour
{
    public static int currentCollisionCount = 0; // 現在衝突しているオブジェクトの数

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grass_0") || other.CompareTag("Grass_1"))
        {
            currentCollisionCount++;
        }
        Debug.Log("currentCollisionCount: " + currentCollisionCount);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Grass_0") || other.CompareTag("Grass_1"))
        {
            currentCollisionCount--;
        }
        Debug.Log("currentCollisionCount: " + currentCollisionCount);
    }
}
