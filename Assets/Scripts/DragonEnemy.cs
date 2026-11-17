using UnityEngine;

public class DragonEnemy : MonoBehaviour
{
    // Transform de l’ocell que es fa servir per calcular la distància fins al drac
    Transform player;

    // Distància màxima a la qual el drac comença a moure’s, configurada a 6 unitats al prefab
    [SerializeField]
    float activationDistance = 6.0f;

    // Velocitat amb què el drac es mou cap a l’esquerra un cop activat
    [SerializeField]
    float speed = 2.0f;

    // Indica si el drac ja ha començat a moure’s
    bool activated = false;

    // Component BirdPhysics de l’objecte que entra al trigger, si en té
    BirdPhysics playerBird;

    // Localitza l’ocell a l’escena per calcular la distància fins al drac
    void Start()
    {
        player = FindFirstObjectByType<BirdPhysics>().transform;
    }

    // Comprova la distància fins a l’ocell i actualitza el moviment del drac
    void Update()
    {
        // Activa el drac quan l’ocell entra dins del radi d’activació
        if (!activated && (Vector2.Distance(transform.position, player.position) <= activationDistance))
        {
            activated = true;
        }

        // Un cop activat, el drac es desplaça cap a l’esquerra
        if (activated)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
    }

    // Resta una vida a l’ocell quan el seu collider entra al trigger del drac
    public void OnTriggerEnter2D(Collider2D collision)
    {
        playerBird = collision.GetComponent<BirdPhysics>();

        if (playerBird)
        {
            playerBird.TakeDamage();
        }
    }
}
