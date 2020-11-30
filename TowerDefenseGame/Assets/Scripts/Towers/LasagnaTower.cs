using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LasagnaTower : Tower
{
    public GameObject projectile;
    public GameObject target;
    float timer = 2.5f;
    float shootTimer = 3.0f;

    Collider2D[] colliders = new Collider2D[100];
    List<GameObject> enemies = new List<GameObject>();
    public void GetEnemies()
    {

    }

    public override void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Physics2D.OverlapCircle(new Vector2(1,1), 5, new ContactFilter2D(), colliders);

            foreach(Collider2D collider in colliders)
            {
                if(collider != null)
                { 
                    if (collider.GetComponent<BaseEnemy>() != null)
                    {
                        enemies.Add(collider.gameObject);
                    }
                }
            }

            if(enemies[0] != null)
            {
                target = enemies[0].gameObject;
            }
            
            timer = 2.5f;
        }

        shootTimer -= Time.deltaTime;
        if(shootTimer <= 0 && target != null)
        {
            Shoot();
            Debug.Log("Shoot");
        }
    }

    public override void Create()
    {

    }

    public override void Upgrade()
    {

    }

    public override void Shoot()
    {
        GameObject gameObject = (GameObject)Instantiate(projectile, transform.position, Quaternion.identity);
        Component comp = target.GetComponent<BaseEnemy>();
        gameObject.GetComponent<Projectile>().target = comp.transform;
    }

    //void OnTriggerEnter2D(Collider2D co)
    //{
    //    if (co.gameObject.GetComponent<BaseEnemy>())
    //    {
    //        GameObject gameObject = (GameObject)Instantiate(projectile, transform.position, Quaternion.identity);
    //        gameObject.GetComponent <Projectile>().target = co.transform;

    //        Debug.Log("Hit");
    //    }
    //    Debug.Log("Entered");
    //}
}
