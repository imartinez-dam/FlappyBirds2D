using UnityEngine;

public class CleanupZone : MonoBehaviour
{
    CleanupTarget cleanupTarget;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        cleanupTarget = collision.GetComponent<CleanupTarget>();

        if(cleanupTarget)
        {
            Destroy(cleanupTarget.gameObject);
        }
    }
}
