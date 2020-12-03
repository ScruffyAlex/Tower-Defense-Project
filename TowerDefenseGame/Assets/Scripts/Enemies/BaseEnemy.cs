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
    public readonly float startSpeed;

    /// <summary>
    /// Determines how much damage this will do to the player if it reaches the end.
    /// </summary>
    public int Damage { get; set; }

    //TODO 
    //Later create a property that determines the enemy's favorite food, that will result in more damage taken.
    //Enum might be made in towers or here in enemy, or somewhere else entirely, consult with team.



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(1, 0, 0) * Time.deltaTime;
    }
}
