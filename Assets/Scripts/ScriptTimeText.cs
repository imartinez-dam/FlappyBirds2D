using UnityEngine;
using TMPro;

public class ScriptTimeText : MonoBehaviour
{
    [SerializeField]
    TMP_Text timeText;

    float elapsedTime;

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        timeText.text = $"Temps: {elapsedTime:F1} s";
    }
}
