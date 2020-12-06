using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LasagnaTower : Tower
{
    //private float timer = 2.5f;
    public GameObject projectile;

    private BaseEnemy target;
    private List<BaseEnemy> enemies = new List<BaseEnemy>();

    public override void Update()
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
        if(shootTimer <= 0 && target != null)
        {
            Shoot();
            shootTimer = shootSpeed;
        }

    }

    public override void Create()
    {
        shootSpeed = 5;
    }

    public override void Upgrade()
    {

    }

    public override void Shoot()
    {
        GameObject projectileClone = Instantiate(projectile, transform.position, Quaternion.identity);
        projectileClone.gameObject.GetComponent<Projectile>().Create(target.gameObject.transform.position);
        //target.gameObject.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        
        if (collider.gameObject.GetComponent<BaseEnemy>())
        {
            Debug.Log("Enemy entered");
            enemies.Add(collider.gameObject.GetComponent<BaseEnemy>());
        }

        foreach (var enemy in enemies)
        {
            Debug.Log(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<BaseEnemy>())
        {
            Debug.Log("Enemy exited");
            enemies.Remove(collider.gameObject.GetComponent<BaseEnemy>());
        }
    }

}

