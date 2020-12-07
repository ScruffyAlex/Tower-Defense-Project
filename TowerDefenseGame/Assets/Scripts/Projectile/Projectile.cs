using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 0.01f;
    private float deathTimer = 5;
    private Vector3 targetDirection;
    private Vector3 direction;

    public void Awake()
    {
        
    }

    public void Update()
    {
        deathTimer -= Time.deltaTime;
        if (deathTimer <= 0)
        {
            Destroy(this.gameObject);
            deathTimer = 5;
        }

        if (targetDirection != null)
        {
            Vector3 newPosition = direction.normalized * speed * Time.deltaTime;
            transform.Translate(newPosition);
        }
    }

    public void Create(Vector3 direction)
    {
        targetDirection = direction;
        this.direction = targetDirection - transform.position;
    }

}
