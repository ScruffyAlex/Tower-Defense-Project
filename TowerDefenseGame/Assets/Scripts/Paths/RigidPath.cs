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
        public Vector2 beginPoint;

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
                return (endPoint - beginPoint).magnitude;
            }
        }

        /// <summary>
        /// Determines angle between the two points in radians
        /// </summary>
        public float Angle
        {
            get
            {
                float delta_x = endPoint.x - beginPoint.x;
                float delta_y = endPoint.y - beginPoint.y;
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
            beginPoint = fPoint;
            endPoint = ePoint;
        } 

    }


    // Properties and fields -----------------------------------------
    [SerializeField]
    public GameObject testFollower;

    [SerializeField]
    public GameObject nodesParent;

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
    public override Vector2 FirstPoint
    {
        get
        {
            return lines[0].beginPoint;
        }
    }
    public override Vector2 LastPoint
    {
        get
        {
            return lines[lines.Count - 1].endPoint;
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
            //Check if enemy has not been destroyed
            if (followers[i].followerObject != null)
            {
                //Only attempt to move if speed is not 0
                if (followers[i].speed != 0)
                {
                    //Move follower dependent on their behavior
                    switch (followers[i].behaviour)
                    {
                        case ePathBehaviour.ABSOLUTE:
                            MoveAbsolute(followers[i]);
                            break;
                        case ePathBehaviour.RELATIVE:
                            MoveRelative(followers[i]);
                            break;
                    }


                    //Run EndEvent if the recent move has passed the path
                    if (followers[i].pathProgress > 1.0f || followers[i].pathProgress < 0.0f)
                    {
                        EndEvent(followers[i]);
                    }
                }
            } else
            {
                followers.RemoveAt(i);
            }
        }
    }
    /// <summary>
    /// Repeated code used for both Move functions
    /// </summary>
    private Vector3 MoveGeneral(Follower follower)
    {
        Vector2 lineInfo = findLine(follower);
        int lineIndex = (int)lineInfo.x;
        float remainingLength = lineInfo.y;

        float frameSpeed = follower.speed * Time.deltaTime;

        if (Mathf.Sign(frameSpeed) == -1.0f) remainingLength = remainingLength - lines[lineIndex].Length;


        //Debug.Log("Transition to next line:" + (frameSpeed >= remainingLength));

        Vector3 newPosition = follower.followerObject.transform.position;

        if (Mathf.Abs(frameSpeed) >= Mathf.Abs(remainingLength))
        {
            frameSpeed -= remainingLength;
            short direction = (short) Mathf.Sign(frameSpeed);
            lineIndex += direction;

            //Snaps to beginning of new line
            float firstAngle = lines[lineIndex-direction].Angle;
            newPosition.x = newPosition.x + Mathf.Cos(firstAngle) * remainingLength;
            newPosition.y = newPosition.y + Mathf.Sin(firstAngle) * remainingLength;
        }

        follower.pathProgress = follower.pathProgress + (frameSpeed / PathLength);

        if (lineIndex < 0 || lineIndex >= lines.Count) return newPosition;

        float angle = lines[lineIndex].Angle;

        newPosition.x = newPosition.x + Mathf.Cos(angle) * frameSpeed;
        newPosition.y = newPosition.y + Mathf.Sin(angle) * frameSpeed;


        return newPosition;
    }

    public override void MoveAbsolute(Follower follower)
    {
        //Seperated from relative just in case unique code is needed later

        Vector3 newPos = MoveGeneral(follower);

        follower.followerObject.transform.position = newPos;
    }

    public override void MoveRelative(Follower follower)
    {
        //Seperated from absolute just in case unique code is needed later

        Vector3 newPos = MoveGeneral(follower);

        follower.followerObject.transform.position = newPos;

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
        return findLine(follower.pathProgress);
    } 

    public Vector2 findLine(float pathProgress)
    {
        float lengthTraveled = pathProgress * PathLength;

        for (int i = 0; i < lines.Count; i++)
        {
            lengthTraveled -= lines[i].Length;

            if (Mathf.Sign(lengthTraveled) == -1)
            {
                float lineRemaining = Mathf.Abs(lengthTraveled);
                return new Vector2(i, lineRemaining);
            }
        }

        return new Vector2(0, 0);
    }



    // Start is called before the first frame update
    void Start()
    {
        if (nodesParent != null)
        {
            Create(nodesParent);
        }
    }

    /// <summary>
    /// Creates a path based on empty objects taken from a parent
    /// </summary>
    /// <param name="nodeParent">The parent of the empty game objects</param>
    public void Create(GameObject nodeParent)
    {
        if (nodeParent != null)
        {
            if (nodeParent.transform.childCount < 2) return;

            for (int i = 1; i < nodeParent.transform.childCount; i++)
            {
                Vector2 point1 = nodeParent.transform.GetChild(i - 1).gameObject.transform.position;
                Vector2 point2 = nodeParent.transform.GetChild(i).gameObject.transform.position;

                lines.Add(new Line(point1, point2));
            }

            //TEST CODE
            //GameObject newFollower = Instantiate(testFollower, testFollower.transform);
            //AddFollower(new Follower(newFollower, 0.0f, ePathBehaviour.RELATIVE, eEndPathEvent.RESTART, 2.5f));
        }

    }

    /// <summary>
    /// Creates a path based on a list of Vector2s
    /// </summary>
    /// <param name="points">The list of Vector2s</param>
    public void Create(List<Vector2> points)
    {
        if (points.Count >= 3)
        {

            for (int i = 1; i < points.Count; i++)
            {
                Vector2 point1 = points[i - 1];
                Vector2 point2 = points[i];

                lines.Add(new Line(point1, point2));
            }

            //TEST CODE
            //GameObject newFollower = Instantiate(testFollower, testFollower.transform);
            //AddFollower(new Follower(newFollower, 0.0f, ePathBehaviour.RELATIVE, eEndPathEvent.RESTART, 2.5f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        Follow();

        //Debug.Log(followers[0].speed);
        //Debug.Log("PathProgress: " + followers[0].pathProgress);
    }

    void OnDrawGizmos()
    {
        Debug.Log("Drawing lines!");

        for (int i = 0; i < lines.Count; i++)
        {
            Gizmos.DrawLine(lines[i].beginPoint, lines[i].endPoint);

        }
    }

    public override Vector2 GetWorldPositionViaPathProgress(float pathProgress, ePathBehaviour behaviour)
    {
        Vector2 newCoord = (behaviour == ePathBehaviour.ABSOLUTE) ? (new Vector2(0, 0)) : (-FirstPoint);

        Vector2 lineInfo = findLine(pathProgress);
        int lineIndex = (int) lineInfo.x;
        float remainingLength = lines[lineIndex].Length - lineInfo.y;

        newCoord += lines[lineIndex].beginPoint;

        float angle = lines[lineIndex].Angle;

        newCoord.x = newCoord.x + Mathf.Cos(angle) * remainingLength;
        newCoord.y = newCoord.y + Mathf.Sin(angle) * remainingLength;

        return newCoord;

    }

    protected override void EndEvent(Follower follower)
    {
        switch (follower.endEvent)
        {
            case eEndPathEvent.STOP:
                StopEndEvent(follower);
                break;
            case eEndPathEvent.RESTART:
                RestartEndEvent(follower);
                break;
            case eEndPathEvent.REVERSE:
                ReverseEndEvent(follower);
                break;
        }
    }
    protected override void StopEndEvent(Follower follower)
    {
        //Sets absolute position to one end of the path, depending on what direction they were going
        if (follower.pathProgress > 1.0f)
        {
            SnapToEnd(follower);
        } else
        {
            SnapToBeginning(follower);
        }

        //Set the speed to 0
        follower.speed = 0;

        
    }
    protected override void RestartEndEvent(Follower follower)
    {
        //Sets absolute position to one end of the path, depending on what direction they were going
        if (follower.pathProgress > 1.0f)
        {
            SnapToBeginning(follower);
        }
        else
        {
            SnapToEnd(follower);
        }

    }
    protected override void ReverseEndEvent(Follower follower)
    {
        //Make a new Vector3 position and preserve the z value
        Vector3 newPos = follower.followerObject.transform.position;

        //Sets absolute position to one end of the path, depending on what direction they were going
        if (follower.pathProgress > 1.0f)
        {
            SnapToEnd(follower);
        }
        else
        {
            SnapToBeginning(follower);
        }

        //Flip the speed of the follower
        follower.speed *= -1;
    }

}
