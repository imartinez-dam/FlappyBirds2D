using UnityEngine;
using TMPro;

public class ObstacleCounter : MonoBehaviour
{
    // Nombre d’obstacles que el jugador ha superat a la partida actual
    int obstaclesPassed = 0;

    // Text del Canvas on es mostra el recompte d’obstacles superats
    [SerializeField]
    TMP_Text obstaclesText;

    // Inicialitza el marcador d’obstacles superats
    void Start()
    {
        obstaclesText.text = $"Obstacles: {obstaclesPassed}";
    }

    // Permet consultar el total d’obstacles superats des d’altres scripts
    public int ObstaclesPassed { get => obstaclesPassed; }

    // Incrementa el recompte i actualitza el text que veu el jugador
    public void AddObstacle()
    {
        obstaclesPassed++;
        obstaclesText.text = $"Obstacles: {obstaclesPassed}";
    }
}
