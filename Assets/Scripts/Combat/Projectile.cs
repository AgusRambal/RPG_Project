using UnityEngine;
using RPG.Core;

public class Projectile : MonoBehaviour
{ 
    [SerializeField] private float speed = 1f;

    private Health target = null;
    private float damage = 0;
    private float timer;

    private void Update()
    {
        if (target == null)
            return;

        transform.LookAt(target.transform.position + Vector3.up); //Sumo 1 porque el modelo mide 2 (RichAI)
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

        target.TakeDamage(damage);
        Destroy(gameObject);
    }

    //Comentario para agregar en un futuro, para que el enemigo no tengo punteria maxima podemos hacer que
    //el mismo tenga una posibilidad de sacar daño, entonces ahi esta la Missfire chance
}
