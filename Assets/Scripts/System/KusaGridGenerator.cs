using UnityEngine;

public class KusaGridGenerator : MonoBehaviour
{
    public GameObject cylinderPrefab;
    public GameObject kusalongPrefab; // 別のプレハブを追加
    private int rows = 100;
    private int columns = 3;
    private float spacing1 = 2.5f; //横幅
    private float spacing2 = 1.25f; //縦幅

    [SerializeField]
    private GameObject kusaStart;
    private int randnum;
    public (int, int)[,] kusaGrid = new (int, int)[100, 3];
    public int[,] kusaHP = new int[100, 3];

    [SerializeField] private EnemyGenerator enemyGenerator;

    // 敵のプレハブを1〜3に対応して登録
    [SerializeField] private GameObject enemy1Prefab;
    [SerializeField] private GameObject enemy2Prefab;
    [SerializeField] private GameObject enemy3Prefab;

    void Awake()
    {
        if (kusaStart != null)
        {
            // 正しいサイズで初期化
            kusaGrid = new (int, int)[rows, columns];
            kusaHP = new int[rows, columns];

            InitializeKusaGrid();
            GenerateCylinderGrid();
        }
        else
        {
            Debug.LogError("kusastart オブジェクトがアサインされていません");
        }

        //敵生成用
        enemyGenerator = GetComponent<EnemyGenerator>();

    }

    void GenerateCylinderGrid()
    {
        Vector3 origin = kusaStart.transform.position;

        for (int z = 0; z < rows; z++)
        {
            for (int x = 0; x < columns; x++)
            {
                Vector3 position = origin + new Vector3(x * spacing1, 0, z * spacing2);

                GameObject prefabToInstantiate = (kusaHP[z, x] == 2) ? kusalongPrefab : cylinderPrefab;

                GameObject instance = Instantiate(prefabToInstantiate, position, Quaternion.identity);

                if (prefabToInstantiate == kusalongPrefab)
                {
                    instance.transform.localScale = new Vector3(instance.transform.localScale.x, 0.6f, instance.transform.localScale.z);
                }

                instance.transform.SetParent(kusaStart.transform);

                // シリンダーに一意の名前を付ける
                string baseName = $"z{z}x{x}";
                instance.name = baseName;

                // 敵タイプを取得
                int enemyType = enemyGenerator.GenerateEnemy();

                if (enemyType != 0)
                {
                    // 敵を同じ位置に生成
                    // switch (enemyType)
                    // {
                    //     case 1:
                    //         Instantiate(enemy1Prefab, position, Quaternion.identity);
                    //         break;
                    //     case 2:
                    //         Instantiate(enemy2Prefab, position, Quaternion.identity);
                    //         break;
                    //     case 3:
                    //         Instantiate(enemy3Prefab, position, Quaternion.identity);
                    //         break;
                    // }

                    // kusaHPを0に
                    //kusaHP[z, x] = 0;

                    // オブジェクト名の末尾に "-1" を追加
                    //instance.name = baseName + "-1";
                }
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
