using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private Transform follow;
    [SerializeField] private float distance;
    [SerializeField] private Vector2 sensitivity;

    private Vector2 angle = new Vector2(90 * Mathf.Deg2Rad, 0);
    private Camera cam;
    private Vector2 nearPlaneSize;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Start()
    {
        CalculateNearPlane();
    }

    void Update()
    {
        SetAngles();
        MouseZoom();
    }

    void LateUpdate()
    {
        SetPosition();
    }

    //Camera moving
    public void SetAngles()
    {
        if (Input.GetMouseButton(2))
        {
            float hor = Input.GetAxis("Mouse X");

            if (hor != 0)
            {
                angle.x += hor * Mathf.Deg2Rad * sensitivity.x;
            }

            float ver = Input.GetAxis("Mouse Y");

            if (ver != 0)
            {
                angle.y += ver * Mathf.Deg2Rad * sensitivity.y;
                angle.y = Mathf.Clamp(angle.y, -30 * Mathf.Deg2Rad, 10 * Mathf.Deg2Rad);
            }

            Cursor.lockState = CursorLockMode.Locked;
        }

        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    //Camera follow player and rotation
    public void SetPosition()
    {
        Vector3 direction = new Vector3(Mathf.Cos(angle.x), -Mathf.Sin(angle.y), -Mathf.Sin(angle.x));

        float dist = distance;
        Vector3[] points = GetCameraColisionPoints(direction);

        foreach (Vector3 point in points)
        {
            if (Physics.Raycast(point, direction, out RaycastHit hit, distance))
            {
                dist = Mathf.Min((hit.point - follow.position).magnitude, dist);
            }
        }

        transform.SetPositionAndRotation(follow.position + direction * dist, Quaternion.LookRotation(follow.position - transform.position));
    }

    //Far or near to the player
    public void MouseZoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            distance--;

            if (distance <= 5)
            {
                distance = 5;
            }
        }

        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            distance++;

            if (distance >= 20)
            {
                distance = 20;
            }
        }
    }

    //Calculate cameraPlane
    private void CalculateNearPlane()
    {
        float height = Mathf.Tan(cam.fieldOfView * Mathf.Deg2Rad / 2) * cam.nearClipPlane;
        float width = height * cam.aspect;

        nearPlaneSize = new Vector2(width, height);
    }

    //Calculate colision of the camera for not to go trough a gameObject
    private Vector3[] GetCameraColisionPoints(Vector3 direction)
    {
        Vector3 position = follow.position;
        Vector3 center = position + direction * (cam.nearClipPlane + 0.2f);

        Vector3 right = transform.right * nearPlaneSize.x;
        Vector3 up = transform.up * nearPlaneSize.y;

        return new Vector3[] { center - right + up, center + right + up, center -right -up, center +right -up};
    }
}
