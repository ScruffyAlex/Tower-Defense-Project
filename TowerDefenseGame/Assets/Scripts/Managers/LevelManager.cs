using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is my comment :)

public class LevelManager : MonoBehaviour
{
    /// <summary>
    /// This is a Game object that will represent the tile we use to build the map.
    /// SerializeField makes it so this property shows up on Unity despite it being private
    /// </summary>
    [SerializeField] 
    private GameObject tile1;
    [SerializeField]
    private GameObject tile2;
    [SerializeField]
    private GameObject tile3;

    /// <summary>
    /// Property that calculates the size of the tiles
    /// </summary>
    public float TileSize {
        get { return 1; }
    }

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    List<Vector2> points = new List<Vector2>();
    void Start()
    {
        points.Add(new Vector2(-10.5f,-0.5f));
        points.Add(new Vector2(-4.5f,-0.5f));
        points.Add(new Vector2(-4.5f,-3.5f));
        points.Add(new Vector2(-1.5f,-3.5f));
        points.Add(new Vector2(-1.5f, 3.5f));
        points.Add(new Vector2( 2.5f, 3.5f));
        points.Add(new Vector2( 2.5f, 0.5f));
        points.Add(new Vector2( 9.5f, 0.5f));

        CreateLevel();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        
    }


    int[,] tileIndices =
    {
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
        {0,0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0 },
        {0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,0 },
        {0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,0 },
        {0,0,0,0,0,0,0,0,0,1,0,0,0,1,1,1,1,1,1,1,2,0 },
        {1,1,1,1,1,1,1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0 },
        {0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0 },
        {0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0 },
        {0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0 },
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }
    };

    /// <summary>
    /// Creates the level using the tile field that was saved (very basic 5x5 at the moment using the white pixel sprite)
    /// </summary>
    private void CreateLevel()
    {
        //Create a new gameobject that will be the path
        GameObject path = new GameObject();
        RigidPath rigidPath = path.AddComponent<RigidPath>();
        rigidPath.Create(points);
        GameObject.Find("EnemyManager").GetComponent<EnemyManager>().paths.Add(rigidPath);



        //Grabs the main Camera
        Camera camera = Camera.main;

        //Basic explanation, returns a Vector3 of the point where the bottom left corner of the screen is in world space
        Vector3 worldStart = camera.ScreenToWorldPoint(new Vector3(0,0,0));
        worldStart.x = Mathf.Floor(worldStart.x);

        //Get the width and height of the screen in world units
        float screenHeight = (camera.orthographicSize * 2);
        float screenWidth = screenHeight * camera.aspect;

        //Determine how many tiles needed to fill screen in each dimension
        int yLength = (int) Mathf.Ceil(screenHeight / TileSize);
        int xLength = (int) Mathf.Ceil(screenWidth / TileSize);

        Debug.Log("yLength: " + yLength);
        Debug.Log("xLength: " + xLength);

        //Begin creating tiles a column at a time
        for (int y = 0; y < yLength; y++) // y position
        {
            for (int x = 0; x < xLength; x++) // x position
            {
                
                PlaceTile(x,y, worldStart, tileIndices[9-y,x]);
            }
        }
    }

    /// <summary>
    /// Places and snaps a tile based on coordinates and worldStart
    /// </summary>
    /// <param name="x">The row the tile snaps to</param>
    /// <param name="y">The column the tile snaps to</param>
    /// <param name="worldStart">The bottom left corner of the snapping grid</param>
    private void PlaceTile(int x, int y, Vector3 worldStart, int tileType)
    {
        //Create new object of the white pixel pre-fab(basically a template) then pass the reference of the object into newTile
        GameObject newTile = new GameObject();
        switch (tileType)
        {
            case 0:
                newTile = Instantiate(tile1);
                break;
            case 1:
                newTile = Instantiate(tile2);
                break;
            case 2:
                newTile = Instantiate(tile3);
                break;
        }
        

        //Assign a new position for the tile
        newTile.transform.position = new Vector3(worldStart.x + (TileSize * x), worldStart.y + (TileSize * y), 0);
    }
}
