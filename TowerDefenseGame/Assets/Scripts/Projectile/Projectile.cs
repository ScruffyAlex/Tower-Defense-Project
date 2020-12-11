using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected float speed { get; set; } = 2.5f;
    protected float deathTimer { get; set; } = 1;
    protected int damage { get; set; } = 1;
    protected Vector3 targetDirection { get; set; }
    protected Vector3 direction { get; set; }

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
        this.transform.localScale = new Vector3(2, 2, 2);
        CircleCollider2D circleCollider2D = this.gameObject.AddComponent<CircleCollider2D>();
        circleCollider2D.isTrigger = true;
        circleCollider2D.radius = 0.3f;

        Rigidbody2D gameObjectsRigidBody = this.gameObject.AddComponent<Rigidbody2D>();
        gameObjectsRigidBody.gravityScale = 0;

        targetDirection = direction;
        this.direction = targetDirection - transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BaseEnemy>())
        {
            collision.gameObject.GetComponent<BaseEnemy>().Health -= damage;
            Debug.Log(collision.gameObject.GetComponent<BaseEnemy>().Health);
            //Destroy(this.gameObject);
        }
    }

}
