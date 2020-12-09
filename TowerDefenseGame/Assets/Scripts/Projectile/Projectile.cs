using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected float speed { get; set; } = 0.01f;
    protected float deathTimer { get; set; } = 5;
    protected Vector3 targetDirection { get; set; }
    private Vector3 direction { get; set; }

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
