using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassHP : MonoBehaviour
{
    public int HP;

    public void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Debug.Log("Grass destroyed!");
            Destroy(gameObject);
        }
    }
}