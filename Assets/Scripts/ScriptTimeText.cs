using UnityEngine;
using TMPro;

public class ScriptTimeText : MonoBehaviour
{
    // Text del Canvas on es mostra el temps de partida en format MM:SS
    [SerializeField]
    TMP_Text timeText;

    // Temps de joc acumulat en segons des que es carrega l’escena
    float elapsedTime;

    // Acumula el temps transcorregut segons el temps de joc de cada fotograma
    void Update()
    {
        elapsedTime += Time.deltaTime;

        // Converteix el temps acumulat a minuts i segons i el mostra en format MM:SS
        int minuts = Mathf.FloorToInt(elapsedTime / 60f);
        int segons = Mathf.FloorToInt(elapsedTime % 60f);

        timeText.text = $"Temps: {minuts:00}:{segons:00}";
    }

    // Exposa el temps acumulat en segons per al resum de final de partida
    public float ElapsedTime { get => elapsedTime; }
}
