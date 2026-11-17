using UnityEngine;
using TMPro;

public class BirdPhysics : MonoBehaviour
{
    // Velocitat horitzontal base de l’ocell
    [SerializeField]
    float speed = 2.0f;

    // Força vertical que s’aplica en cada aleteig
    [SerializeField]
    float force = 200.0f;

    // Entrada horitzontal del jugador que val −1 per frenar, 0 per mantenir la velocitat i 1 per accelerar
    private float horizontalInput = 0;

    // Variació de la velocitat horitzontal quan el jugador accelera o frena
    [SerializeField]
    float horizontalSpeedModifier = 2.0f;

    // Rigidbody2D de l’ocell que controla el moviment i rep la força de l’aleteig
    Rigidbody2D birdBody;

    // Nombre màxim de vides amb què comença la partida, configurat a 3 a MainScene
    [SerializeField]
    int maxHealth = 3;

    // Nombre de vides que li queden a l’ocell durant la partida
    int currentHealth;

    // Text del Canvas on es mostra el nombre de vides actual
    [SerializeField]
    TMP_Text healthText;

    // Font d’àudio del mateix objecte que reprodueix el so de l’aleteig
    private AudioSource audioSource;

    // Inicialitza el cos físic, el marcador de vides i la font d’àudio
    void Start()
    {
        birdBody = GetComponent<Rigidbody2D>();
        birdBody.linearVelocity = Vector2.right * speed;

        currentHealth = maxHealth;
        healthText.text = $"Vides: {currentHealth}";

        audioSource = GetComponent<AudioSource>();
    }

    // Llegeix les entrades del jugador mentre la partida està activa
    void Update()
    {
        // Anul·la l’entrada horitzontal al menú inicial i a Game Over
        if (!GameManager.instance.IsPlaying)
        {
            horizontalInput = 0f;
        }

        else
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                // Aplica una força cap amunt i reprodueix el so de l’aleteig
                birdBody.AddForce(Vector2.up * force);
                audioSource.Play();
            }

            // Reinicia el valor de l’entrada horitzontal abans de comprovar les tecles
            horizontalInput = 0;

            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                horizontalInput++;
            }

            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                horizontalInput--;
            }
        }
    }
    // Actualitza la velocitat horitzontal a cada pas de física i conserva la velocitat vertical
    private void FixedUpdate()
    {
        // Velocitat horitzontal resultant de combinar la velocitat base amb l’entrada del jugador
        float horizontalSpeed = speed + horizontalInput * horizontalSpeedModifier;

        birdBody.linearVelocity = new Vector2(horizontalSpeed, birdBody.linearVelocity.y);
    }

    // Resta una vida quan l’ocell toca un drac i acaba la partida quan no en queden
    public void TakeDamage()
    {
        currentHealth--;
        healthText.text = $"Vides: {currentHealth}";

        if (currentHealth <= 0)
        {
            GameManager.instance.GameOver();
        }
    }

    // Recupera una vida si no s’ha arribat al màxim i retorna si la recuperació s’ha aplicat
    public bool Heal()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth++;
            healthText.text = $"Vides: {currentHealth}";
            return true;
        }
        else
        { return false; }
    }

    // Finalitza la partida si l’ocell xoca amb la part sòlida d’un obstacle, amb el terra o amb el sostre
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.instance.GameOver();
    }
}
