using UnityEngine;

public class Pickups : MonoBehaviour
{
    // Indica si el premi gira, amb el valor activat al prefab de la moneda i desactivat al del préssec
    [SerializeField]
    bool rotate = false;
    // Velocitat de rotació en graus per segon
    [SerializeField]
    float rotationSpeed = 25f;

    // Gira el premi al voltant de l’eix Y si té la rotació activada i la partida està en curs
    void Update()
    {
        if (rotate && GameManager.instance.IsPlaying)
        {
            transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
        }
    }
    // Comprova si l’objecte que ha entrat al trigger té l’etiqueta del jugador
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Component BirdPhysics de l’ocell que permet comprovar si pot recuperar una vida
            BirdPhysics playerBird = collision.GetComponent<BirdPhysics>();

            // Suma un punt i elimina la moneda quan es recull
            if (CompareTag("coin"))
            {
                GameManager.instance.AddPoint();
                Destroy(gameObject);
            }
            // El préssec només es consumeix si l’ocell recupera una vida
            else if (CompareTag("peach"))
            {
                if (playerBird.Heal())
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
