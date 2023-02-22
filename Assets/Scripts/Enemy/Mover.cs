using Pathfinding;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [Header("PlayerComponents")]
    [SerializeField] private RichAI agent;
    public Animator animator;

    [Header("Dependences")]
    public Camera cam;

    private bool initialIddle;
    private bool moving;

    private void Start()
    {
        initialIddle = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            MoveToCursor();
            PlayerRotation();
        }
    }

    private void MoveToCursor()
    {
        Movement();
        MovementActions();
    }

    public void Movement()
    { 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool hasHit = Physics.Raycast(ray, out RaycastHit hit);

        if (hasHit)
        {
            GetComponent<RichAI>().destination = hit.point;
            initialIddle = false;
        }
    }

    private void MovementActions()
    { 
        if (initialIddle)
            return;

        //if (!isAttacking)
        //{
        if (transform.hasChanged)
        {
            animator.SetBool("isWalking", true);
            moving = true;
        }
        //}

        if (agent.reachedEndOfPath)
        {
            animator.SetBool("isWalking", false);
            moving = false;
        }
    }

    public void PlayerRotation()
    {
        if (moving)
            return;

        Vector3 positionOnScreen = cam.WorldToViewportPoint(transform.position);
        Vector3 mouseOnScreen = (Vector2)cam.ScreenToViewportPoint(Input.mousePosition);

        Vector3 direction = mouseOnScreen - positionOnScreen;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90.0f;
        transform.rotation = Quaternion.Euler(new Vector3(0, -angle, 0));
    }
}
