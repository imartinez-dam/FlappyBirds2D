using UnityEngine;

public class Pickups : MonoBehaviour
{
    // Controla la rotació, els préssecs no giren, les monedes si
    [SerializeField]
    bool rotate = false;
    // Velocitat de la rotació per controlar-la amb Time.deltaTime
    [SerializeField]
    float rotationSpeed = 25f;

    // Update is called once per frame
    // A cada update comprovem si l'objecte té la bool en true li donem rotació
    void Update()
    {
        if (rotate && GameManager.instance.IsPlaying)
        {
            transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BirdPhysics playerBird = collision.GetComponent<BirdPhysics>();

            if (CompareTag("coin"))
            {
                GameManager.instance.AddPoint();
                Destroy(gameObject);
            }
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
