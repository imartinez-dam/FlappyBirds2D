using UnityEngine;

public class ScriptObstacle : MonoBehaviour
{
    [SerializeField]
    float speed = 1.0f;

    [SerializeField]
    float timeBetweenReverse = 0f;

    Rigidbody2D obstacleBody;

    ObstacleCounter obstacleCounter;
    bool counted = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Assignem velocitat al rigidBody de l'obstacle
        obstacleBody = GetComponent<Rigidbody2D>();
        obstacleBody.linearVelocity = Vector2.up * speed;

        InvokeRepeating("Reverse", timeBetweenReverse, timeBetweenReverse);

        obstacleCounter = FindFirstObjectByType<ObstacleCounter>();
    }
    void Reverse()
    {
        // Invertim la direcció de la velocitat. És com canviar de sentit
        obstacleBody.linearVelocity *= -1;
    }

    public void SetDirection(bool isUpper)
    {
        if(isUpper)
        {
            speed = Mathf.Abs(speed) * -1; // Agafa el valor absolut per fer-lo sempre negatiu
            timeBetweenReverse = 1f;
        }
        else
        {
            speed = Mathf.Abs(speed);   // Agafa el valor absolut que sempre és positiu
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!counted && collision.CompareTag("Player"))
        {
            counted = true;
            obstacleCounter.AddObstacle();
        }
    }
}
