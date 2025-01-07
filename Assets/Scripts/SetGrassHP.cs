using UnityEngine;

// 優先度
[DefaultExecutionOrder(1)]
public class SetGrassHP : MonoBehaviour
{
    [SerializeField] private int grass0HP = 1; // Grass_0 のHP
    [SerializeField] private int grass1HP = 2; // Grass_1 のHP


    void Start()
    {
        SetHPToTaggedObjects("Grass_0", grass0HP);
        SetHPToTaggedObjects("Grass_1", grass1HP);
    }

    private void SetHPToTaggedObjects(string tag, int hp)
    {
        GameObject[] grasses = GameObject.FindGameObjectsWithTag(tag);
        Debug.Log(tag + " count: " + grasses.Length);

        foreach (GameObject grass in grasses)
        {
            GrassHP grassHPComponent = grass.AddComponent<GrassHP>(); // GrassHPコンポーネントを追加
            grassHPComponent.HP = hp; // HPを設定
        }
    }
}