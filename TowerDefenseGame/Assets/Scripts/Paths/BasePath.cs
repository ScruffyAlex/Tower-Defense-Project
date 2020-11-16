using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePath : MonoBehaviour
{
    //Definitions ----------------------------------------------------------------------------

    /// <summary>
    /// Determines what will happen when the object arrives at the end of the path
    /// </summary>
    enum eEndPathEvent
    {
        STOP,
        REPEAT,
        RETURN
    }

    /// <summary>
    /// Determines if the object follows a relative or absolute path
    /// </summary>
    enum ePathBehaviour
    {
        ABSOLUTE,
        RELATIVE
    }

    public struct Follower
    {
        /// <summary>
        /// The object that this Follower represents
        /// </summary>
        GameObject followerObject;
        /// <summary>
        /// Determines how far in a path it is, if needed (0-1)
        /// </summary>
        float pathProgress;
        /// <summary>
        /// Deterimines following behaviour for this follower
        /// </summary>
        ePathBehaviour behaviour;
        /// <summary>
        /// Determines what happens when this Follower reaches the end of the path
        /// </summary>
        eEndPathEvent endEvent;
        /// <summary>
        /// The speed the follower goes through the path (World Units per second)
        /// </summary>
        float speed;
        /// <summary>
        /// Creates a Follower with the object passed in with default settings: (0 progress, ABSOLUTE path, STOP endEvent, 1.0 speed)
        /// </summary>
        /// <param name="newObject">The GameObject that this Follower will represent</param>
        Follower(GameObject newObject)
        {
            followerObject = newObject;
            pathProgress = 0;
            behaviour = BasePath.ePathBehaviour.ABSOLUTE;
            endEvent = BasePath.eEndPathEvent.STOP;
            speed = 1.0f;
        }

        /// <summary>
        /// Instantiates a Follower with specified object and speed, the rest are default
        /// </summary>
        /// <param name="newObject">The GameObject that this Follower will represent</param>
        /// <param name="newSpeed">The speed this Follower will have</param>
        Follower(GameObject newObject, float newSpeed)
        {
            followerObject = newObject;
            pathProgress = 0;
            behaviour = BasePath.ePathBehaviour.ABSOLUTE;
            endEvent = BasePath.eEndPathEvent.STOP;
            speed = newSpeed;
        }

        /// <summary>
        /// Instantiates a Follower with all of the specified parameters
        /// </summary>
        /// <param name="newObject">The GameObject that this Follower will represent</param>
        /// <param name="newPathProgress">Where (from 0-1) the object will start</param>
        /// <param name="newBehaviour">The following behaviour this Follower will use</param>
        /// <param name="newEndEvent">The EndPathEvent this Follower will use</param>
        /// <param name="newSpeed">The speed this Follower will have</param>
        Follower(GameObject newObject, float newPathProgress, ePathBehaviour newBehaviour, eEndPathEvent newEndEvent, float newSpeed)
        {
            followerObject = newObject;
            pathProgress = newPathProgress;
            behaviour = newBehaviour;
            endEvent = newEndEvent;
            speed = newSpeed;
        }
    }

    // Properties and fields -----------------------------------------------------------

    /// <summary>
    /// A list of objects using the path
    /// </summary>
    protected List<Follower> followers = new List<Follower>();





    /// <summary>
    /// abstract function that will iterate through all of the objects the path has control of, and updates their position
    /// </summary>
    abstract public void Follow();
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
