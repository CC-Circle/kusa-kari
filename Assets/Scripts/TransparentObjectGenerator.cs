using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentObjectGenerator : MonoBehaviour
{
    [SerializeField] private Transform blockParent;
    [SerializeField] private GameObject transparentObject;
    [SerializeField] private const int MAP_HEIGHT = 100;

    void Start()
    {
        Vector3 defaultPos = new Vector3(0.0f, 0.7f / 2, 0.0f);
        defaultPos.x = 0;
        defaultPos.z = 1f / 2;

        for (int j = 0; j < MAP_HEIGHT; j += 1)
        {
            Vector3 pos = defaultPos;
            pos.z += j * 0.7f;

            GameObject obj = Instantiate(transparentObject, pos, Quaternion.identity, blockParent);
        }
    }
}
