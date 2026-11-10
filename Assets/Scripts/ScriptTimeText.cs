using UnityEngine;
using TMPro;

public class ScriptTimeText : MonoBehaviour
{
    [SerializeField]
    TMP_Text timeText;

    float elapsedTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        timeText.text = $"Temps: {elapsedTime:F1} s";
    }
}
