using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    int score = 0;
    
    [SerializeField]
    TMP_Text scoreText;

    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText.text = $"Puntuació: {score}";
        audioSource = GetComponent<AudioSource>();
    }

    public void AddPoint()
    {
        score++;
        scoreText.text = $"Puntuació: {score}";
        audioSource.Play();
    }
}
