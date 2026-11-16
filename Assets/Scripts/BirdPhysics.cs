using UnityEngine;
using TMPro;

public class BirdPhysics : MonoBehaviour
{
    // Movement speed
    [SerializeField]
    float speed = 2.0f;

    [SerializeField]
    float force = 200.0f;

    private float horizontalInput = 0;

    [SerializeField]
    float horizontalSpeedModifier = 2.0f;

    Rigidbody2D birdBody;

    [SerializeField]
    int maxHealth = 3;

    int currentHealth;

    [SerializeField]
    TMP_Text healthText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        birdBody = GetComponent<Rigidbody2D>();
        birdBody.linearVelocity = Vector2.right * speed;

        currentHealth = maxHealth;
        healthText.text = $"Vides: {currentHealth}";
    }

    // Update is called once per frame
    void Update()
    {
        // Bloqueja el moviment al menú inicial i a Game Over
        if (!GameManager.instance.IsPlaying)
        {
            horizontalInput = 0f;
        }

        else
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                //Flap cap amunt
                birdBody.AddForce(Vector2.up * force);
            }

            // Reinicia el moviment horitzontal a cada iteració
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
    private void FixedUpdate()
    {
        float horizontalSpeed = speed + horizontalInput * horizontalSpeedModifier;

        birdBody.linearVelocity = new Vector2(horizontalSpeed, birdBody.linearVelocity.y);
    }

    public void TakeDamage()
    {
        currentHealth--;
        healthText.text = $"Vides: {currentHealth}";

        if (currentHealth <= 0)
        {
            GameManager.instance.GameOver();
        }
    }

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.instance.GameOver();
    }
}
