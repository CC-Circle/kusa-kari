using UnityEngine;

public class KusaGridGenerator : MonoBehaviour
{
    public GameObject cylinderPrefab;
    public GameObject kusalongPrefab; // 別のプレハブを追加
    public int rows = 100;
    public int columns = 5;
    public float spacing = 1.0f;

    [SerializeField]
    private GameObject kusaStart;
    private int randnum;

    public (int, int)[,] kusaGrid = new (int, int)[100, 5];
    public int[,] kusaHP = new int[100, 5];

    void Awake()
    {
        if (kusaStart != null)
        {
            
            InitializeKusaGrid();
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

                // プレハブを選択
                GameObject prefabToInstantiate = (kusaHP[z,x] == 2) ? kusalongPrefab : cylinderPrefab;

                // プレハブをインスタンス化
                GameObject instance = Instantiate(prefabToInstantiate, position, Quaternion.identity);

                // kusalongPrefab が選ばれた場合、yスケールを0.6に設定
                if (prefabToInstantiate == kusalongPrefab)
                {
                    instance.transform.localScale = new Vector3(instance.transform.localScale.x, 0.6f, instance.transform.localScale.z);
                }

                // kusastartの子オブジェクトとして設定（親設定）
                instance.transform.SetParent(kusaStart.transform);

                // シリンダーに一意の名前を付ける
                instance.name = $"z{z}x{x}";
            }
        }
    }



    public void InitializeKusaGrid()
    {

        for (int z = 0; z < kusaGrid.GetLength(0); z++)
        {
            for (int x = 0; x < kusaGrid.GetLength(1); x++)
            {
                randnum = Random.Range(1, 5); // 1〜4のランダムな整数を生成
                if(x != 2){
                    if(randnum == 1)
                    {
                        kusaHP[z,x] = 2;
                    }else{
                        kusaHP[z,x] = 1;
                    }
                    kusaGrid[z, x] = (z, x);
                }else{
                    kusaHP[z,x] = 1;
                    kusaGrid[z, x] = (z, x);
                }
                
            }
        }
    }
}
