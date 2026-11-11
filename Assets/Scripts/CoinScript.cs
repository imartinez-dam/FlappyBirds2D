using UnityEngine;

public class CoinScript : MonoBehaviour
{
    ScoreManager scoreManager;
    Vector3 movement = new Vector3(0f, 0.2f, 0f);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreManager = FindFirstObjectByType<ScoreManager>();
    }
    void Update()
    {
        transform.Rotate(movement);
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
