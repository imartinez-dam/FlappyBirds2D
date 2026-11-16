using UnityEngine;

public class Pickups : MonoBehaviour
{
    [SerializeField]
    bool rotate = false;

    [SerializeField]
    float rotationSpeed = 12f;

    private ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = FindFirstObjectByType<ScoreManager>();
    }

    // Update is called once per frame
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
                scoreManager.AddPoint();
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
