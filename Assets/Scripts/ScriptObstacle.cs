using UnityEngine;

public class ScriptObstacle : MonoBehaviour
{
    [SerializeField]
    float speed = 1.0f;

    [SerializeField]
    float timeBetweenReverse = 0f;

    Rigidbody2D obstacleBody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Assignem velocitat al rigidBody de l'obstacle
        obstacleBody = GetComponent<Rigidbody2D>();
        obstacleBody.linearVelocity = Vector2.up * speed;

        InvokeRepeating("Reverse", timeBetweenReverse, timeBetweenReverse);
    }
    void Reverse()
    {
        // Invertim la direcció de la velocitat. És com canviar de sentit
        obstacleBody.linearVelocity *= -1;
    }
}
