using UnityEngine;
using TMPro;
using System.Collections;

[RequireComponent(typeof(TMP_Text))]
public class TMP_RedBlackBlink : MonoBehaviour
{
    public Color colorA = Color.red;
    public Color colorB = Color.black;

    [Tooltip("点滅間隔（秒）")]
    public float blinkInterval = 0.5f;

    private TMP_Text tmpText;
    private bool isRed = true;

    void Awake()
    {
        tmpText = GetComponent<TMP_Text>();
        StartCoroutine(BlinkRoutine());
    }

    IEnumerator BlinkRoutine()
    {
        while (true)
        {
            tmpText.color = isRed ? colorA : colorB;
            isRed = !isRed;

            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
