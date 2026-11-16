using UnityEngine;

public class DragonEnemy : MonoBehaviour
{
    Transform player;

    [SerializeField]
    float activationDistance = 6.0f;

    [SerializeField]
    float speed = 2.0f;

    bool activated = false;

    BirdPhysics playerBird;

    void Start()
    {
        player = FindFirstObjectByType<BirdPhysics>().transform;
    }

    // Update is called once per frame    
    void Update()
    {
        if (!activated && (Vector2.Distance(transform.position, player.position) <= activationDistance))
        {
            activated = true;
        }

        if (activated)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        playerBird = collision.GetComponent<BirdPhysics>();

        if (playerBird)
        {
            playerBird.TakeDamage();
        }
    }
}
