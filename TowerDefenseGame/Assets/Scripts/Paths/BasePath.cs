using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePath : MonoBehaviour
{
    //Definitions ----------------------------------------------------------------------------

    /// <summary>
    /// Determines what will happen when the object arrives at the end of the path
    /// </summary>
    public enum eEndPathEvent
    {
        STOP,
        REPEAT,
        RETURN
    }

    /// <summary>
    /// Determines if the object follows a relative or absolute path
    /// </summary>
    public enum ePathBehaviour
    {
        ABSOLUTE,
        RELATIVE
    }

    public class Follower
    {
        /// <summary>
        /// The object that this Follower represents
        /// </summary>
        public GameObject followerObject;
        /// <summary>
        /// Determines how far in a path it is, if needed (0-1)
        /// </summary>
        public float pathProgress;
        /// <summary>
        /// Deterimines following behaviour for this follower
        /// </summary>
        public ePathBehaviour behaviour;
        /// <summary>
        /// Determines what happens when this Follower reaches the end of the path
        /// </summary>
        public eEndPathEvent endEvent;
        /// <summary>
        /// The speed the follower goes through the path (World Units per second)
        /// </summary>
        public float speed;
        /// <summary>
        /// Creates a Follower with the object passed in with default settings: (0 progress, ABSOLUTE path, STOP endEvent, 1.0 speed)
        /// </summary>
        /// <param name="newObject">The GameObject that this Follower will represent</param>
        public Follower(GameObject newObject)
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
        public Follower(GameObject newObject, float newSpeed)
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
        public Follower(GameObject newObject, float newPathProgress, ePathBehaviour newBehaviour, eEndPathEvent newEndEvent, float newSpeed)
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


    // Methods -------------------------------------------------------------------------

    /// <summary>
    /// Adds a Follower object to the list of Followers in the path
    /// </summary>
    /// <param name="newFollower">The new follower to add</param>
    public void AddFollower(Follower newFollower)
    {
        followers.Add(newFollower);

        //TODO put the new object's position where the starting position is
        //newFollower.followerObject.transform.position
    }

    /// <summary>
    /// Removes a Follower if a match is found within the list.
    /// </summary>
    /// <param name="removeFollower">The Follower you are trying to remove</param>
    public void RemoveFollower(Follower removeFollower)
    {
        followers.Remove(removeFollower);
    }
    /// <summary>
    /// Attempts to find the Follower object that represents the object passed in, and returns it
    /// </summary>
    /// <param name="findObject">The follower you'd wisht to find</param>
    /// <returns>Returns the follower that matches the object</returns>
    public Follower FindFollower(GameObject findObject)
    {
        Follower findFollower = followers.Find(
            delegate (Follower f)
            {
                return f.followerObject.GetInstanceID() == findObject.GetInstanceID();
            });


        return findFollower;
    }

    /// <summary>
    /// Removes a follower if match is found within the list using GameObject
    /// </summary>
    /// <param name="removeObject">The object you are trying to remove</param>
    public void RemoveFollower(GameObject removeObject)
    {
        Follower removeFollower = followers.Find( 
            delegate(Follower f)
            {
                return f.followerObject.GetInstanceID() == removeObject.GetInstanceID();
            });


        followers.Remove(removeFollower);
    }

    /// <summary>
    /// Abstract function that will return the world position based on progress and follow behaviour (relative or absolute)
    /// </summary>
    abstract public void GetWorldPositionViaPathProgress(float pathProgress, ePathBehaviour behaviour);

    /// <summary>
    /// Abstract function that will iterate through all of the objects the path has control of, and updates their position
    /// </summary>
    abstract public void Follow();
    /// <summary>
    /// Abstract function that will move object if it is absolute
    /// </summary>
    abstract public void MoveAbsolute(Follower follower);
    /// <summary>
    /// Abstract function that will move object if it is relative
    /// </summary>
    abstract public void MoveRelative(Follower follower);
    /// <summary>
    /// Function that handles what happens when object hits end of path
    /// </summary>
    abstract public void EndEvent();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
