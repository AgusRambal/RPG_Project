using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] private float chaseDistance = 5f;

    private Health player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Health>();
    }

    private void Update()
    {
        if (DistanceToPlayer())
        {
            Debug.Log("Chase");
        }
    }

    private bool DistanceToPlayer()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        return distance < chaseDistance;
    }
}
