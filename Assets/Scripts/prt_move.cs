using UnityEngine;

public class prt_move : MonoBehaviour
{
    public GameObject[] cylinders; // 5本の円柱を設定する配列
    private bool isRightDeleted = false;
    private bool isLeftDeleted = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            DeleteRightCylinders();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            DeleteLeftCylinders();
        }

        if (isRightDeleted && isLeftDeleted)
        {
            DeleteCentralCylinder();
        }
    }

    void DeleteRightCylinders()
    {
        if (!isRightDeleted)
        {
            // 右の2本を削除
            Destroy(cylinders[3]);
            Destroy(cylinders[4]);
            isRightDeleted = true;
        }
    }

    void DeleteLeftCylinders()
    {
        if (!isLeftDeleted)
        {
            // 左の2本を削除
            Destroy(cylinders[0]);
            Destroy(cylinders[1]);
            isLeftDeleted = true;
        }
    }

    void DeleteCentralCylinder()
    {
        // 中央の1本を削除
        Destroy(cylinders[2]);
    }
}

