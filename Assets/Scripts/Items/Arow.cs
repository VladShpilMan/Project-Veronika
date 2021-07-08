using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arow : MonoBehaviour
{
    private GameObject parent;
    private Vector3 direction;
    private int attackDamage = 1;
    private float repulsion = 0;

    public Vector3 Direction { set { direction = value; } }
    public GameObject Parent { set { parent = value; } get { return parent; } }

    [SerializeField] private float speed = 10.0F;

    private void Start()
    {
        Destroy(gameObject, 1.4F);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();

      
        if (unit && unit.gameObject != parent && unit is Character)
        {
            collider.GetComponent<Character>().TakeDamage(attackDamage, repulsion);

            Destroy(gameObject);
        }
    }
}
