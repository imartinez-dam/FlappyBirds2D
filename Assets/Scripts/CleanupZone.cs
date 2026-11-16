using UnityEngine;

public class CleanupZone : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("coin") ||
            collision.CompareTag("peach") ||
            collision.CompareTag("dragon") ||
            collision.CompareTag("obstacle"))
        {
            Destroy(collision.gameObject);
        }
    }
}
