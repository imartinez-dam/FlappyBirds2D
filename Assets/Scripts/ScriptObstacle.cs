using UnityEngine;

public class ScriptObstacle : MonoBehaviour
{
    // Velocitat vertical de l’obstacle amb un signe que indica el sentit inicial del moviment
    [SerializeField]
    float speed = 1.0f;

    // Temps en segons entre canvis de sentit, configurat a 2 al prefab i ajustat a 1 per als obstacles superiors
    [SerializeField]
    float timeBetweenReverse = 0f;

    // Rigidbody2D del prefab que controla el moviment de l’obstacle
    Rigidbody2D obstacleBody;

    // Indica si aquest obstacle ja s’ha comptat durant la partida
    bool counted = false;

    // Inicialitza el cos físic i programa els canvis de sentit
    void Start()
    {
        // Assignem la velocitat inicial al Rigidbody2D de l’obstacle
        obstacleBody = GetComponent<Rigidbody2D>();
        obstacleBody.linearVelocity = Vector2.up * speed;

        // Programa els canvis de sentit segons l’interval configurat
        InvokeRepeating("Reverse", timeBetweenReverse, timeBetweenReverse);
    }
    void Reverse()
    {
        // Invertim la direcció de la velocitat
        obstacleBody.linearVelocity *= -1;
    }

    // Fa que l’obstacle superior baixi i l’inferior pugi en el primer tram del moviment
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
    // Compta una sola vegada el pas de l'ocell pel segon collider, configurat com a trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!counted && collision.CompareTag("Player"))
        {
            counted = true;
            GameManager.instance.AddObstacle();
        }
    }
}
