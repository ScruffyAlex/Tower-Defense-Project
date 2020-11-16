using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidPath : BasePath
{
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

    /// <summary>
    /// A List that holds all of the lines in order
    /// </summary>
    List<Line> lines = new List<Line>();

    /// <summary>
    /// Property that returns the amount of lines
    /// </summary>
    public int lineAmount { 
        get
        {
            return lines.Count;
        }
    }



    /// <summary>
    /// Will use the List of lines to create and handle the path
    /// </summary>
    /// <param name="gameObject">The GameObject that needs to follow</param>
    public override void Follow(ref GameObject gameObject)
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Finds the line the gameObject is on
    /// </summary>
    /// <returns></returns>
    public int findLine(ref GameObject gameObject)
    {
        

        return 0;
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
