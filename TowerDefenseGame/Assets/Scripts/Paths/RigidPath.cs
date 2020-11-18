﻿using System.Collections;
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
        }
    }

    public override void MoveAbsolute(Follower follower)
    {
        Vector2 lineInfo = findLine(follower);
        int lineIndex = (int) lineInfo.x;
        float remainingLength = lineInfo.y;

        float frameSpeed = follower.speed * Time.deltaTime;

        if (Mathf.Sign(frameSpeed) == -1.0f) remainingLength = remainingLength - lines[lineIndex].Length;


        //Debug.Log("Transition to next line:" + (frameSpeed >= remainingLength));

        Vector3 newPosition = follower.followerObject.transform.position;

        if (Mathf.Abs(frameSpeed) >= Mathf.Abs(remainingLength))
        {
            frameSpeed -= remainingLength;
            lineIndex += (int) Mathf.Sign(frameSpeed);

            //Snaps to beginning of new line

            if (lineIndex >= lines.Count && lineIndex <= -1)
            {
                newPosition.x = lines[lineIndex].beginPoint.x;
                newPosition.y = lines[lineIndex].beginPoint.y;
            }
        }

        follower.pathProgress = follower.pathProgress + (frameSpeed / PathLength);

        if (lineIndex < 0 || lineIndex >= lines.Count) return;

        float angle = lines[lineIndex].Angle;

        newPosition.x = newPosition.x + Mathf.Cos(angle) * frameSpeed;
        newPosition.y = newPosition.y + Mathf.Sin(angle) * frameSpeed;


        follower.followerObject.transform.position = newPosition;

        

        
    }

    public override void MoveRelative(Follower follower)
    {
        


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
        if (nodesParent != null)
        {
            if (nodesParent.transform.childCount < 2) return;

            for (int i = 1; i < nodesParent.transform.childCount; i++)
            {
                Vector2 point1 = nodesParent.transform.GetChild(i - 1).gameObject.transform.position;
                Vector2 point2 = nodesParent.transform.GetChild(i).gameObject.transform.position;

                lines.Add(new Line(point1, point2));
            }

            GameObject newFollower = Instantiate(testFollower);
            followers.Add(new Follower(newFollower, 0.0f, ePathBehaviour.ABSOLUTE, eEndPathEvent.RESTART, 2.5f));
            Gizmos.color = Color.red;
        }
        else
        {
            //Default testing values
            lines.Add(new Line(new Vector2(0, 0), new Vector2(2, 0)));
            lines.Add(new Line(new Vector2(2, 0), new Vector2(2, 2)));
            lines.Add(new Line(new Vector2(2, 2), new Vector2(4, 4)));
            lines.Add(new Line(new Vector2(4, 4), new Vector2(4, 0)));

            GameObject newFollower = Instantiate(testFollower);
            followers.Add(new Follower(newFollower, 0.0f, ePathBehaviour.ABSOLUTE, eEndPathEvent.REVERSE, 2.5f));
            Gizmos.color = Color.blue;
        }
    }

    // Update is called once per frame

    Color tempColor;
    void Update()
    {
        Follow();

        Debug.Log(followers[0].speed);
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

    public override void GetWorldPositionViaPathProgress(float pathProgress, ePathBehaviour behaviour)
    {
        throw new System.NotImplementedException();
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
