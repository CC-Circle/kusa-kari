using UnityEngine;

public class CylinderGridGenerator : MonoBehaviour
{
    public GameObject cylinderPrefab;
    public int rows = 10;
    public int columns = 5;
    public float spacing = 1.0f; // 生成間隔を狭く設定

    [SerializeField]
    private GameObject kusaStart; // シリアライズフィールドとして宣言

    void Awake()
    {
        if (kusaStart != null)
        {
            GenerateCylinderGrid();
        }
        else
        {
            Debug.LogError("kusastart オブジェクトがアサインされていません");
        }
    }

    void GenerateCylinderGrid()
    {
        Vector3 origin = kusaStart.transform.position; // kusastartの座標を取得

        for (int z = 0; z < rows; z++)
        {
            for (int x = 0; x < columns; x++)
            {
                Vector3 position = origin + new Vector3(x * spacing, 0, z * spacing);
                GameObject cylinder = Instantiate(cylinderPrefab, position, Quaternion.identity);

                // kusastartの子オブジェクトとして設定
                cylinder.transform.parent = kusaStart.transform;

                // シリンダーに一意の名前を付ける
                cylinder.name = $"z{z}x{x}";

                // シリンダーのスケールを変更
                cylinder.transform.localScale = new Vector3(0.15f, 0.2f, 0.15f);
            }
        }
    }
}
