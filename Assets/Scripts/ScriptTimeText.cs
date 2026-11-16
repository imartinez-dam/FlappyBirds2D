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

        int minuts = Mathf.FloorToInt(elapsedTime / 60f);
        int segons = Mathf.FloorToInt(elapsedTime % 60f);

        timeText.text = $"Temps: {minuts:00}:{segons:00}";
    }

    public float ElapsedTime { get => elapsedTime; }
}
