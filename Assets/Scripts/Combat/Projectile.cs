using UnityEngine;
using RPG.Core;

public class Projectile : MonoBehaviour
{ 
    [SerializeField] private float speed = 1f;

    private Health target = null;

    private void Update()
    {
        if (target == null)
            return;

        transform.LookAt(target.transform.position + Vector3.up); //Sumo 1 porque el modelo mide 2 (RichAI)
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void SetTarget(Health target)
    {
        this.target = target;
    }
}
