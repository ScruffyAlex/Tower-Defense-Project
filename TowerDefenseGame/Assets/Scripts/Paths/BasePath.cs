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
        RESTART,
        REVERSE
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
        /// The position the object was at when they were added to the path, used for relative pathfinding
        /// </summary>
        public Vector2 relaPos;
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
            relaPos = newObject.transform.position;
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
            relaPos = newObject.transform.position;
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
            relaPos = newObject.transform.position; //Saves home position, to be changed to actual relaPos once added to a Path
        }
    }

    // Properties and fields -----------------------------------------------------------

    /// <summary>
    /// A list of objects using the path
    /// </summary>
    protected List<Follower> followers = new List<Follower>();

    /// <summary>
    /// Returns a Vector2 of the beginning of the path in world space, absolute
    /// </summary>
    abstract public Vector2 FirstPoint { get; }
    /// <summary>
    /// Returns a Vector2 of the end of the path in world space, absolute
    /// </summary>
    abstract public Vector2 LastPoint { get; }

    


    // Methods -------------------------------------------------------------------------

    /// <summary>
    /// Adds a Follower object to the list of Followers in the path
    /// </summary>
    /// <param name="newFollower">The new follower to add</param>
    public void AddFollower(Follower newFollower)
    {
        //put the new object's position where the starting position is
        Vector3 newPos = newFollower.followerObject.transform.position;
        Vector2 snapPos = GetWorldPositionViaPathProgress(newFollower.pathProgress, newFollower.behaviour);
        newPos.x = snapPos.x;
        newPos.y = snapPos.y;

        //If relative, shift position based on relaPos
        if (newFollower.behaviour == ePathBehaviour.RELATIVE) newPos += new Vector3(newFollower.relaPos.x, newFollower.relaPos.y, newPos.z);

        newFollower.followerObject.transform.position = newPos;

        //Add follower to the list
        followers.Add(newFollower);
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
    abstract public Vector2 GetWorldPositionViaPathProgress(float pathProgress, ePathBehaviour behaviour);
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
    /// Function that handles what happens when follower hits end of path
    /// </summary>
    abstract protected void EndEvent(Follower follower);
    /// <summary>
    /// Handles the stop Event for the follower
    /// </summary>
    /// <param name="follower">The follower that triggered the event</param>
    abstract protected void StopEndEvent(Follower follower);
    /// <summary>
    /// Handles the Restart event for the follower
    /// </summary>
    /// <param name="follower">The follower that triggered the event</param>
    abstract protected void RestartEndEvent(Follower follower);
    /// <summary>
    /// Handles the Reverse event for the follower
    /// </summary>
    /// <param name="follower">The follower that triggered the event</param>
    abstract protected void ReverseEndEvent(Follower follower);
    /// <summary>
    /// Snap the follower passed in to the beginning of the path
    /// </summary>
    /// <param name="follower">The follower you'd like to snap</param>
    protected virtual void SnapToBeginning(Follower follower)
    {
        //Make a new Vector3 position and preserve the z value
        Vector3 newPos = follower.followerObject.transform.position;
        follower.pathProgress = 0.0f;

        if (follower.behaviour == ePathBehaviour.ABSOLUTE) {

            newPos.x = FirstPoint.x;
            newPos.y = FirstPoint.y;
        } else
        {
            newPos.x = follower.relaPos.x;
            newPos.y = follower.relaPos.y;
        }


        //Set the new position for the follower
        follower.followerObject.transform.position = newPos;
    }
    /// <summary>
    /// Snap the follower passed in to the end of the path
    /// </summary>
    /// <param name="follower">The follower you'd like to snap</param>
    protected virtual void SnapToEnd(Follower follower)
    {

        Vector3 newPos = follower.followerObject.transform.position;
        follower.pathProgress = 1.0f;

        if (follower.behaviour == ePathBehaviour.ABSOLUTE)
        {
            newPos.x = LastPoint.x;
            newPos.y = LastPoint.y;
        }
        else
        {
            Vector2 modPos = GetWorldPositionViaPathProgress(1.0f, ePathBehaviour.RELATIVE);

            newPos.x = follower.relaPos.x + modPos.x;
            newPos.y = follower.relaPos.y + modPos.y;
        }


        //Set the new position for the follower
        follower.followerObject.transform.position = newPos;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
