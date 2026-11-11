using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    int score = 0;
    
    [SerializeField]
    TMP_Text scoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText.text = $"Puntuació: {score}";
    }

    public void AddPoint()
    {
        score++;
        scoreText.text = $"Puntuació: {score}";
    }
}
