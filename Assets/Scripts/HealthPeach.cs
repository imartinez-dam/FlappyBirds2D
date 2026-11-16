using UnityEngine;

public class HealthPeach : MonoBehaviour
{
    BirdPhysics playerBird;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        playerBird = collision.GetComponent<BirdPhysics>();

        if (playerBird)
        {
            if(playerBird.Heal())
            {
                Destroy(gameObject);
            }
        }
    }
}
