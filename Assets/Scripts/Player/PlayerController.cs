using UnityEngine;
using Pathfinding;

public class PlayerController : MonoBehaviour
{
    [Header("PlayerComponents")]
    [SerializeField] private RichAI agent;
    public Animator animator;

    [Header("Dependences")]  
    public Camera cam;

    [Header("Other")]
    [SerializeField] private LayerMask FloorLayer;

    [HideInInspector] public bool isAttacking;
    private AttackScript attack;
    private bool initialIddle;
    private bool moving;

    private void Awake()
    {
        attack = GetComponent<AttackScript>();
    }

    private void Start()
    {
        initialIddle = true;
    }

    void FixedUpdate()
    {
        if (attack.Combat()) 
            return;

        PlayerMovement();
        PlayerRotation();
    }

    //Movement actions
    public void PlayerMovement()
    {
        Movement();

        if (initialIddle)
            return;

        if (!isAttacking)
        {
            if (transform.hasChanged)
            {
                animator.SetBool("isWalking", true);
                moving = true;
            }
        }     

        if (agent.reachedEndOfPath)
        {
            animator.SetBool("isWalking", false);

            moving = false;
        }
    }

    public void Movement()
    { 
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, FloorLayer))
            {
                isAttacking = false;
                StartMoveAction(hit.point);
                initialIddle = false;
            }
        }
    }

    public void StartMoveAction(Vector3 destination)
    {
        attack.CancelCombat();
        Move(destination);
    }

    //Set target
    public void Move(Vector3 target)
    {
        agent.isStopped = false;
        agent.destination = target;      
    }

    //Stop movement
    public void Stop()
    {
        animator.SetBool("isWalking", false);
        moving = false;
        agent.isStopped = true;
    }

    //Stop movement without the mouse control
    public void StopV2()
    {
        animator.SetBool("isWalking", false);
        agent.isStopped = true;
    }

    public void SetMoving(bool state)
    {
        moving = state;
    }

    //Player facing the mouse when not moving
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
