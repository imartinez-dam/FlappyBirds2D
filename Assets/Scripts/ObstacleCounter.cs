using UnityEngine;
using TMPro;

public class ObstacleCounter : MonoBehaviour
{
    int obstaclesPassed = 0;

    [SerializeField]
    TMP_Text obstaclesText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        obstaclesText.text = $"Obstacles: {obstaclesPassed}";
    }

    public int ObstaclesPassed { get => obstaclesPassed; }

    public void AddObstacle()
    {
        obstaclesPassed++;
        obstaclesText.text = $"Obstacles: {obstaclesPassed}";
    }
}
