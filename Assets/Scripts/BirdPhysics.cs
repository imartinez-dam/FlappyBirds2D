using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdPhysics : MonoBehaviour
{
    // Movement speed
    [SerializeField]
    float speed = 2.0f;

    [SerializeField]
    float force = 200.0f;

    private float horizontalInput = 0;

    [SerializeField]
    float horizontalSpeedModifier = 2.0f;

    Rigidbody2D birdBody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        birdBody = GetComponent<Rigidbody2D>();
        birdBody.linearVelocity = Vector2.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            //Flap cap amunt
            birdBody.AddForce(Vector2.up * force);
        }

        horizontalInput = 0;

        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            horizontalInput++;
        }

        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            horizontalInput--;
        }
    }
    private void FixedUpdate()
    {
        float horizontalSpeed = speed + horizontalInput * horizontalSpeedModifier;

        birdBody.linearVelocity = new Vector2(horizontalSpeed, birdBody.linearVelocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
