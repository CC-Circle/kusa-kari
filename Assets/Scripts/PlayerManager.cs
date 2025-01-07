using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerHitGrass playerHitGrass; // PlayerHitGrassコンポーネント
    [SerializeField] private MouseMove mouseMove; // MouseMoveコンポーネント
    [SerializeField] private Transform playerTransform; // プレイヤーのTransform
    [SerializeField] private GrassGenerator grassGenerator;
    private int currentRow = 0; // 現在の行


    void Start()
    {
        if (playerHitGrass == null)
        {
            // PlayerControllerコンポーネントが設定されていない場合、シーンから探す
            playerHitGrass = FindObjectOfType<PlayerHitGrass>();

            if (playerHitGrass == null)
            {
                Debug.LogError("Player Controller not found!");
                return;
            }
        }

        if (mouseMove == null)
        {
            // MouseMoveコンポーネントが設定されていない場合、シーンから探す
            mouseMove = FindObjectOfType<MouseMove>();

            if (mouseMove == null)
            {
                Debug.LogError("Mouse Move not found!");
                return;
            }
        }


        playerHitGrass.SetPlayer(playerHitGrass.gameObject);
    }

    void Update()
    {
        mouseMove.move();

        if (!grassGenerator.CheckGrassInRow(currentRow))
        {
            playerTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z + 1f);
            Debug.Log("No grass in row " + currentRow);
            // 現在の行に草がない場合、次の行に移動
            currentRow++;
        }
    }
}
