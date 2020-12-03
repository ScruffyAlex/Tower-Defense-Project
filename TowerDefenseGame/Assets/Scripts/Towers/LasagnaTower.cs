using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LasagnaTower : Tower
{
    //public GameObject target;
    float timer = 2.5f;
    float shootSpeed = 3;
    float shootTimer = 3.0f;
    private float range = 5;
    public GameObject projectile;

    List<GameObject> enemies = new List<GameObject>();
    public void GetEnemies()
    {

    }

    public override void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            //timer = shootTimer;
            //Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), range);

            //foreach(Collider2D collider in colliders)
            //{
            //    if(collider != null)
            //    { 
            //        if (collider.GetComponent<BaseEnemy>() != null)
            //        {
            //            enemies.Add(collider.gameObject);
            //        }
            //    }
            //}

            //if(enemies[0] != null)
            //{
            //    target = enemies[0].gameObject;
            //}
            
            //timer = 2.5f;
        }

        shootTimer -= Time.deltaTime;
        if(shootTimer <= 0)// && target != null)
        {
            Shoot();
            Debug.Log("Shoot");
            shootTimer = shootSpeed;
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
        //Projectile projectile = new Projectile { };
        //projectile.Create(target.transform.position);

        //projectile.target = transform;

        //Projectile projectile = new Projectile { };
        GameObject projectileClone = Instantiate(projectile, transform.position, Quaternion.identity);
        //projectileClone.Create(new Vector3( 1, 1, 1 ));


        //GameObject gameObject = (GameObject)Instantiate(projectile, transform.position, Quaternion.identity);
        //Component comp = target.GetComponent<BaseEnemy>();
        //gameObject.GetComponent<Projectile>().target = comp.transform;
    }
}
