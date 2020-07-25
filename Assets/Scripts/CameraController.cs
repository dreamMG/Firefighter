using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0.0f, 2.5f, -5f);
    [SerializeField] private float smoothTime = 0.25f;
    private Vector3 pos;

    private void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
    }

    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref pos, smoothTime);
    }
}
