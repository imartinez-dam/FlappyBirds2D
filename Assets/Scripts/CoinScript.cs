using UnityEngine;

public class CoinScript : MonoBehaviour
{
    ScoreManager scoreManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreManager = FindFirstObjectByType<ScoreManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<BirdPhysics>())
        {
            scoreManager.AddPoint();
            Destroy(gameObject);
        }
    }
}
