using UnityEngine;

public class ObstaclePassTrigger : MonoBehaviour
{
    ObstacleCounter obstacleCounter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        obstacleCounter = FindFirstObjectByType<ObstacleCounter>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<BirdPhysics>())
        {
            obstacleCounter.AddObstacle();
            Destroy(gameObject);
        }
    }
}
