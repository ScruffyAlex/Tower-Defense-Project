using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidPath : BasePath
{
    //Definitions ---------------------------------------------------

    /// <summary>
    /// Basic Line data structure to be used inside RigidPath
    /// </summary>
    public class Line
    {
        /// <summary>
        /// (x,y) of the beginning of the line in world units.
        /// </summary>
        public Vector2 firstPoint;

        /// <summary>
        /// (x,y) of the end of the line in world units.
        /// </summary>
        public Vector2 endPoint;


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
        /// Determines angle between the two points in radians
        /// </summary>
        public float Angle
        {
            get
            {
                float delta_x = endPoint.x - firstPoint.x;
                float delta_y = endPoint.y - firstPoint.y;
                return Mathf.Atan2(delta_y, delta_x);
            }
        }

        /// <summary>
        /// Creates a new Line using points passed in in world units.
        /// </summary>
        /// <param name="fPoint">New first point</param>
        /// <param name="ePoint">New end point</param>
        public Line(Vector2 fPoint, Vector2 ePoint)
        {
            firstPoint = fPoint;
            endPoint = ePoint;
        } 

    }


    // Properties and fields -----------------------------------------
    [SerializeField]
    public GameObject testFollower;


    /// <summary>
    /// A List that holds all of the lines in order
    /// </summary>
    private List<Line> lines = new List<Line>();

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
    /// Property that returns the total length of the path in world units
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

    // Methods ------------------------------------------------------------



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
        Vector2 lineInfo = findLine(follower);
        int lineIndex = (int) lineInfo.x;
        float remainingLength = lineInfo.y;

        float frameSpeed = Time.deltaTime / follower.speed;

        //Debug.Log("Transition to next line:" + (frameSpeed >= remainingLength));

        Vector3 newPosition = follower.followerObject.transform.position;
        if (frameSpeed >= remainingLength)
        {
            frameSpeed -= remainingLength;
            lineIndex++;

            //Snaps to beginning of new line
            newPosition.x = lines[lineIndex].firstPoint.x;
            newPosition.y = lines[lineIndex].firstPoint.y;
        }

        float angle = lines[lineIndex].Angle;

        newPosition.x = newPosition.x + Mathf.Cos(angle) * frameSpeed;
        newPosition.y = newPosition.y + Mathf.Sin(angle) * frameSpeed;


        follower.followerObject.transform.position = newPosition;

        follower.pathProgress = follower.pathProgress+ (frameSpeed / PathLength);
        Debug.Log("PathProgress: " + follower.pathProgress);
        
    }

    public override void MoveRelative(Follower follower)
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Calculates the length of lines from this path with the given indexes
    /// </summary>
    /// <param name="i1">first index</param>
    /// <param name="i2">second index, inclusive</param>
    /// <returns>Returns length of lines provided</returns>
    public float returnLengthOfLines(int i1, int i2)
    {
        float totalLength = 0;

        for (int j = i1; j <= i2; j++)
        {
            totalLength += lines[j].Length;
        }

        return totalLength;
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
        lines.Add(new Line(new Vector2(0, 0), new Vector2(2, 0)));
        lines.Add(new Line(new Vector2(2, 0), new Vector2(2, 2)));
        lines.Add(new Line(new Vector2(2, 2), new Vector2(4, 4)));
        lines.Add(new Line(new Vector2(4, 4), new Vector2(4, 0)));

        GameObject newFollower = Instantiate(testFollower);
        followers.Add(new Follower(newFollower, 0.0f, ePathBehaviour.ABSOLUTE, eEndPathEvent.STOP,  1.5f));
    }

    // Update is called once per frame

    void Update()
    {
        Follow();
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
