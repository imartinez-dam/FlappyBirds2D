using UnityEngine;

public class LoopingGround : MonoBehaviour
{
    // Transform de la càmera principal que permet consultar-ne la posició horitzontal
    private Transform cameraTransform;
    // Càmera principal que proporciona la mida ortogràfica i la relació d’aspecte
    private Camera mainCamera;

    // Amplada d’un segment de terra o de sostre, configurada a 64 unitats
    [SerializeField]
    float segmentWidth = 64.0f;

    // Nombre de segments per cada seqüència, configurat a 2 per al terra i a 2 per al sostre
    [SerializeField]
    int segmentCount = 2;

    // Obté la càmera principal i en desa el Transform
    void Start()
    {
        mainCamera = Camera.main;
        cameraTransform = mainCamera.transform;
    }

    // Comprova si el segment de terra o de sostre ha quedat fora de la vista
    void Update()
    {
        // Calcula la vora dreta del segment i la vora esquerra visible de la càmera
        float segmentRight = transform.position.x + segmentWidth / 2;

        float cameraLeft = cameraTransform.position.x - mainCamera.orthographicSize * mainCamera.aspect;

        // Quan el segment queda fora de la vista, el col·loca al final de la seva seqüència
        if (segmentRight < cameraLeft)
        {
            transform.position += Vector3.right * segmentWidth * segmentCount;
        }
    }
}
