using UnityEngine;

public class GrassGenerator : MonoBehaviour
{
    [SerializeField] private Transform blockParent;
    [SerializeField] private GameObject[] blockPrefabs; // 草ブロックのバリエーションを配列で指定
    [SerializeField] private float[] prefabYOffsets; // 各プレハブに対応するYオフセット

    private const int MAP_WIDTH = 5;
    [SerializeField] private const int MAP_HEIGHT = 100;

    [Range(0f, 1f)]
    [SerializeField] private float skipProbability = 0.2f;

    [SerializeField] private GameObject blockPrefab_Skip;
    // 生成されたGrassオブジェクトを保持する2次元配列
    private GameObject[,] generatedGrass = new GameObject[MAP_WIDTH, MAP_HEIGHT];



    // 外部から呼び出せる関数 (GrassのPrefabをランダムに決定)
    public (GameObject, int) GetRandomGrassPrefab(out float yOffset)
    {
        if (blockPrefabs == null || blockPrefabs.Length == 0)
        {
            Debug.LogError("blockPrefabs is not assigned or empty!");
            yOffset = 0f; // デフォルト値
            return (null, -1);
        }

        int randomIndex = Random.Range(0, blockPrefabs.Length);

        // yOffsetも返す
        if (prefabYOffsets != null && prefabYOffsets.Length > randomIndex)
        {
            yOffset = prefabYOffsets[randomIndex];
        }
        else
        {
            yOffset = 0f; // オフセットが未定義の場合は0
            Debug.LogWarning("Y offset not defined for prefab at index " + randomIndex + ". Using 0.");
        }

        return (blockPrefabs[randomIndex], randomIndex);
    }



    void Start()
    {
        Vector3 defaultPos = new Vector3(0.0f, 0.7f / 2, 0.0f);
        defaultPos.x = -2f * 0.7f;
        defaultPos.z = 1f / 2;

        for (int i = 0; i < MAP_WIDTH; i += 1)
        {
            for (int j = 0; j < MAP_HEIGHT; j += 1)
            {
                Vector3 pos = defaultPos;
                pos.x += i * 0.7f;
                pos.z += j * 0.7f;

                GameObject obj;

                if (Random.value < skipProbability)
                {
                    if (blockPrefab_Skip != null)
                    {
                        obj = Instantiate(blockPrefab_Skip, blockParent);
                        obj.transform.position = pos;
                    }
                }
                else
                {
                    float yOffset; // yオフセットを受け取る変数
                    (GameObject selectedPrefab, int index) = GetRandomGrassPrefab(out yOffset);

                    if (selectedPrefab != null)
                    {
                        obj = Instantiate(selectedPrefab, blockParent);
                        Vector3 posWithOffset = pos;
                        posWithOffset.y += yOffset; // オフセットを追加
                        obj.transform.position = posWithOffset;
                        obj.name = "Grass_" + +index + "_" + j;
                        obj.tag = "Grass_" + +index;

                        // 生成されたGrassオブジェクトを配列に格納
                        generatedGrass[i, j] = obj;
                    }
                    else
                    {
                        Debug.LogError("Failed to get a random grass prefab.");
                    }

                }
            }
        }
    }

    // 指定したz行にGrassオブジェクトが存在するかどうかをチェック
    public bool CheckGrassInRow(int z)
    {
        if (z < 0 || z >= MAP_HEIGHT)
        {
            return false; // 範囲外の場合はfalse
        }

        for (int i = 0; i < MAP_WIDTH; i++)
        {
            if (generatedGrass[i, z] != null && generatedGrass[i, z].activeSelf)
            {
                // Debug.Log("Grass found at row " + z);
                return true; // オブジェクトが存在し、アクティブであればtrue
            }
        }

        // Debug.Log("No grass found at row " + z);
        return false; // すべてnullまたは非アクティブであればfalse
    }
}