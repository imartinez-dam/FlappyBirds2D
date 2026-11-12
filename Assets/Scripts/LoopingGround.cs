using UnityEngine;

public class LoopingGround : MonoBehaviour
{
    private Transform cameraTransform;
    private Camera mainCamera;

    [SerializeField]
    float segmentWidth = 64.0f;

    [SerializeField]
    int segmentCount = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
        cameraTransform = mainCamera.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float segmentRight = transform.position.x + segmentWidth / 2;

        float cameraLeft = cameraTransform.position.x - mainCamera.orthographicSize * mainCamera.aspect;

        if (segmentRight < cameraLeft)
        {
            transform.position += Vector3.right * segmentWidth * segmentCount;
        }
    }
}
