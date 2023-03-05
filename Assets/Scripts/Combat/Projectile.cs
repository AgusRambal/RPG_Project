using UnityEngine;
using RPG.Core;

public class Projectile : MonoBehaviour
{ 
    [SerializeField] private float speed = 1f;
    [SerializeField] private bool isHoming = false;

    private Health target = null;
    private float damage = 0;
    private float timer;

    private void Start()
    {
        transform.LookAt(target.transform.position + Vector3.up);
    }

    private void Update()
    {
        if (target == null)
            return;

        if (isHoming && !target.IsDead())
        {
            transform.LookAt(target.transform.position + Vector3.up); //Sumo 1 porque el modelo mide 2 (RichAI)
        }

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        timer += Time.deltaTime;

        if (timer > 3f)
        {
            Destroy(gameObject);
        }
    }

    public void SetTarget(Health target, float damage)
    {
        this.target = target;
        this.damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Health>() != target) 
            return;

        if (target.IsDead())
            return;

        target.TakeDamage(damage);
        Destroy(gameObject);
    }

    //Comentario para agregar en un futuro, para que el enemigo no tengo punteria maxima podemos hacer que
    //el mismo tenga una posibilidad de sacar daño, entonces ahi esta la Missfire chance
}
