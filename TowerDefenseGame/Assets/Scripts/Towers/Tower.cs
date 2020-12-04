using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    protected float shootSpeed = 3.0f;
    protected float shootTimer = 1.0f;
    protected float range = 5;
    public bool isDestroyed { get; set; } = false;
    //make a enemy variable?
    //make a projectile variable

    /// <summary>
    /// Update the towers
    /// </summary>
    public virtual void Update()
    { 
        //find enemy(closest)
        //float timer += Time.deltaTime; 
        //determine time to shoot
    }

    /// <summary>
    /// Create the objects for towers
    /// </summary>
    public virtual void Create()
    {
        //determine if needed later
        //determine shootspeed for each turret
    }

    /// <summary>
    /// Declare Upgrades for towers
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
        //pass in enemy?
        //fire projectiles
    }
}
