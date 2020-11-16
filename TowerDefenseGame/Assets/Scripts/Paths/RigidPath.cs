using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidPath : BasePath
{
    //Definitions ---------------------------------------------------

    /// <summary>
    /// Basic Line data structure to be used inside RigidPath
    /// </summary>
    struct Line
    {
        /// <summary>
        /// (x,y) of the beginning of the line in world units.
        /// </summary>
        Vector2 firstPoint;

        /// <summary>
        /// (x,y) of the end of the line in world units.
        /// </summary>
        Vector2 endPoint;


        /// <summary>
        /// Determines the length of the line in world units
        /// </summary>
        public float Length {
            get
            {
                return (endPoint - firstPoint).magnitude;
            }
        }

        /// <summary>
        /// Creates a new Line using points passed in in world units.
        /// </summary>
        /// <param name="fPoint">New first point</param>
        /// <param name="ePoint">New end point</param>
        Line(Vector2 fPoint, Vector2 ePoint)
        {
            firstPoint = fPoint;
            endPoint = ePoint;
        } 

    }


    // Properties and fields -----------------------------------------

    /// <summary>
    /// A List that holds all of the lines in order
    /// </summary>
    List<Line> lines = new List<Line>();

    /// <summary>
    /// Property that returns the amount of lines
    /// </summary>
    public int LineAmount { 
        get
        {
            return lines.Count;
        }
    }

    /// <summary>
    /// Property that returns the total length of the path
    /// </summary>
    public float PathLength { 
        get
        {
            float totalLength = 0;
            foreach(Line l in lines) {
                totalLength += l.Length;
            }

            return totalLength;
        }
    }



    /// <summary>
    /// Will use the List of lines to create and handle the path
    /// </summary>
    /// <param name="gameObject">The GameObject that needs to follow</param>
    public override void Follow()
    {
        for (int i = 0; i < followers.Count; i++)
        {
            switch (followers[i].behaviour)
            {
                case ePathBehaviour.ABSOLUTE:
                    MoveAbsolute(followers[i]);
                    break;
                case ePathBehaviour.RELATIVE:
                    MoveRelative(followers[i]);
                    break;
            }
        }
    }

    public override void MoveAbsolute(Follower follower)
    {
        //TODO flesh out Absolute first and test

        throw new System.NotImplementedException();
    }

    public override void MoveRelative(Follower follower)
    {
        throw new System.NotImplementedException();
    }



    /// <summary>
    /// Finds the index of the line (vec.x) the gameObject is on and returns the remaining length of the line (vec.y)
    /// </summary>
    /// <param name="gameObject">The object you want to find the line of</param>
    /// <returns>Returns the index of the line found</returns>
    public Vector2 findLine(GameObject gameObject)
    {
        Follower follower = FindFollower(gameObject);
        return findLine(follower);
    }

    /// <summary>
    /// Finds the index of the line (vec.x) the follower is on and returns the remaining length of the line (vec.y)
    /// </summary>
    /// <param name="follower">The follower you want to find the line of</param>
    /// <returns>Returns the index of the line found (vec.x) and remaining length of line (vec.y)</returns>
    public Vector2 findLine(Follower follower)
    {
        float lengthTraveled = follower.pathProgress * PathLength;

        for (int i = 0; i < lines.Count; i++)
        {
            lengthTraveled -= lines[i].Length;

            if (Mathf.Sign(lengthTraveled) == -1)
            {
                float lineRemaining = Mathf.Abs(lengthTraveled);
                return new Vector2(i,lineRemaining);
            }
        }

        return new Vector2(0,0);
    } 



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void GetWorldPositionViaPathProgress(float pathProgress, ePathBehaviour behaviour)
    {
        throw new System.NotImplementedException();
    }

    public override void EndEvent()
    {
        throw new System.NotImplementedException();
    }

   
}
