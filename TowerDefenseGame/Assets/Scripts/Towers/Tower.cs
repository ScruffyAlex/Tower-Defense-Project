using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    float shootSpeed;
    //make a enemy variable?
    //make a projectile variable

    /// <summary>
    /// Update the towers
    /// </summary>
    public abstract void Update();
    //find enemy(closest)
    //float timer += Time.deltaTime; 
    //determine time to shoot

    /// <summary>
    /// Create the objects for towers
    /// </summary>
    public abstract void Create();
    //determine if needed later
    //determine shootspeed for each turret

    /// <summary>
    /// Declare Upgrades for towers
    /// </summary>
    public abstract void Upgrade();
    //check if rounds are valid
    //check if currency is equal to or over amount
    //subtract currency then upgrade
    //determine which upgrade they choose...

    /// <summary>
    /// Shoot Projectiles from towers
    /// </summary>
    public abstract void Shoot();
    //pass in enemy?
    //fire projectiles
}
