using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Windows;
using System.Windows.Control;

//This is my comment :)

public class LevelManager : MonoBehaviour
{
    public enum eState
    {
        Title,
        StartGame,
        Game,
        GameOver
    }

    
    public eState State { get; set; } = eState.Title;
    
    /// <summary>
    /// This is a Game object that will represent the tile we use to build the map.
    /// SerializeField makes it so this property shows up on Unity despite it being private
    /// </summary>
    [SerializeField] 
    private GameObject tile;

    /// <summary>
    /// Property that calculates the size of the tiles
    /// </summary>
    public float TileSize {
        get { return tile.GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        CreateLevel();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        switch (State)
        {
            case eState.Title:
                if(InputSystem.Instance.GetKeyState(InputSystem.eKey.Space))
                {
                    State = eState.StartGame;
                }
                break;
            case eState.StartGame:
                if()
                break;
            case eState.Game:

                break;
            case eState.GameOver:

                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Creates the level using the tile field that was saved (very basic 5x5 at the moment using the white pixel sprite)
    /// </summary>
    private void CreateLevel()
    {
        //Grabs the main Camera
        Camera camera = Camera.main;

        //Basic explanation, returns a Vector3 of the point where the bottom left corner of the screen is in world space
        Vector3 worldStart = camera.ScreenToWorldPoint(new Vector3(0,0,0));

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
                PlaceTile(x,y, worldStart);
            }
        }
    }

    /// <summary>
    /// Places and snaps a tile based on coordinates and worldStart
    /// </summary>
    /// <param name="x">The row the tile snaps to</param>
    /// <param name="y">The column the tile snaps to</param>
    /// <param name="worldStart">The bottom left corner of the snapping grid</param>
    private void PlaceTile(int x, int y, Vector3 worldStart)
    {
        //Create new object of the white pixel pre-fab(basically a template) then pass the reference of the object into newTile
        GameObject newTile = Instantiate(tile);

        //Assign a new position for the tile
        newTile.transform.position = new Vector3(worldStart.x + (TileSize * x), worldStart.y + (TileSize * y), 0);
    }
}
