using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool cameraMove = false;

    private float distance;

    [SerializeField] private Transform target;

    private void Update()
    {
        if (cameraMove)
        {
            transform.position = new Vector3(transform.position.x, target.position.y + distance, transform.position.z);
        }
    }

    public void Init(Transform _target)
    {
        target = _target;

        distance = transform.position.y - target.position.y;
    }

    public void StartMove()
    {
        cameraMove = true;
    }

    public void UndoMove()
    {
        cameraMove = false;

        transform.position = new Vector3(0f, 0f, transform.position.z);
    }
}
