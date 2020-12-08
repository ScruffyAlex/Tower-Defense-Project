using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{

    /// <summary>
    /// Property that stores the amount of health of the enemy
    /// </summary>
    public int Health { get; set; }

    /// <summary>
    /// Value used to determine the starting speed when attached to a path
    /// </summary>
    public float startSpeed;

    /// <summary>
    /// Determines how much damage this will do to the player if it reaches the end.
    /// </summary>
    public int Damage { get; set; }


    public BaseEnemy(int newHealth, float newSpeed, int newDamage)
    {
        Health = newHealth;
        startSpeed = newSpeed;
        Damage = newDamage;
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
            Destroy(this);
        }
    }
}
