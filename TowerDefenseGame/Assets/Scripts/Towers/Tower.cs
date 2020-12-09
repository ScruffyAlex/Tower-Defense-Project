using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    protected float shootSpeed { get; set; } = 3.0f;
    protected float shootTimer { get; set; } = 0.0f;
    protected float range { get; set; } = 5;
    protected BaseEnemy target;
    protected List<BaseEnemy> enemies = new List<BaseEnemy>();

    public GameObject projectile;

    /// <summary>
    /// Update the towers
    /// Set target from the list of targets
    /// If target exists, shoot with the specified speed
    /// </summary>
    public virtual void Update()
    {
        if (target == null && enemies.Count >= 1)
        {
            target = enemies[0];
        }

        if (target != null && target.Health <= 0)
        {
            target = null;
        }

        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0 && target != null)
        {
            Shoot();
            shootTimer = shootSpeed;
        }
    }

    /// <summary>
    /// Create the objects for towers
    /// Create the rigid body that lets the tower detect objects
    /// </summary>
    public virtual void Create()
    {
        CircleCollider2D circleCollider2D = this.gameObject.AddComponent<CircleCollider2D>();
        circleCollider2D.isTrigger = true;
        circleCollider2D.radius = range;

        Rigidbody2D gameObjectsRigidBody = this.gameObject.AddComponent<Rigidbody2D>();
        gameObjectsRigidBody.gravityScale = 0;
        
    }

    /// <summary>
    /// Declare Upgrades for towers
    /// Currently nothing
    /// </summary>
    public virtual void Upgrade()
    {
        //check if rounds are valid
        //check if currency is equal to or over amount
        //subtract currency then upgrade
        //determine which upgrade they choose...
    }

    /// <summary>
    /// Shoot Projectiles from towers
    /// </summary>
    public virtual void Shoot()
    {
        GameObject projectileClone = Instantiate(projectile, transform.position, Quaternion.identity);
        projectileClone.gameObject.GetComponent<Projectile>().Create(target.gameObject.transform.position);
    }

    /// <summary>
    /// Check if something enters the collider radius
    /// check if it's an enemy and add to the list of enemies
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.GetComponent<BaseEnemy>())
        {
            enemies.Add(collider.gameObject.GetComponent<BaseEnemy>());
        }
    }

    /// <summary>
    /// Check if something exits the collider radius
    /// check if it's an enemy and remove it from the list of enemies
    /// </summary>
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<BaseEnemy>())
        {
            if (target == collider.gameObject.GetComponent<BaseEnemy>())
            {
                target = null;
            }
            enemies.Remove(collider.gameObject.GetComponent<BaseEnemy>());
        }
    }
}
