using UnityEngine;

public class HealthPeach : MonoBehaviour
{
    PlayerHealth playerHealth;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        playerHealth = collision.GetComponent<PlayerHealth>();

        if (playerHealth)
        {
            if(playerHealth.Heal())
            {
                Destroy(gameObject);
            }
        }
    }
}
