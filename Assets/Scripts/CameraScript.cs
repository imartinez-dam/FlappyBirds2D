using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Transform de l’objecte bird_0 assignat a l’Inspector per seguir-ne la posició horitzontal
    [SerializeField]
    Transform targetBird;
    // Segueix bird_0 en l’eix X a LateUpdate i desplaça el fons i la zona de neteja perquè són fills de la càmera
    void LateUpdate()
    {
        // Manté les coordenades Y i Z de la càmera
        transform.position = new Vector3(targetBird.position.x,
                                            transform.position.y,
                                            transform.position.z);
    }
}
