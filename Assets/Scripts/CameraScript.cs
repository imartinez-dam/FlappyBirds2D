using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    Transform targetBird;
    void LateUpdate()
    {
        transform.position = new Vector3(targetBird.position.x,
                                            transform.position.y,
                                            transform.position.z);
    }
}
