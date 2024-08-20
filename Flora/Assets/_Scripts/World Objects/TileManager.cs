using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [Header("Tile and Slot Objects")]
    public GameObject currentTile;
    public List<GameObject> tiles;
    public GameObject slotManagerObject;

    [Header("External Scripts")]
    public SlotManager slotManager;
    public Casting playerCasting;

    [Header("Tile Map Information")]
    public Tilemap tileMap;
    public List<Vector3> tilePositions;
    public GameObject tile;

    [Header("Placable Tiles")]
    public TileBase placableTile;

    #region Start and Update
    private void Start()
    {
        //Finds the slot manager and player and gets the script
        slotManagerObject = GameObject.FindGameObjectWithTag("SlotManager");
        slotManager = slotManagerObject.GetComponent<SlotManager>();
        playerCasting = GameObject.FindGameObjectWithTag("Player").GetComponent<Casting>();
    }
    private void Awake()
    {
        //Places all the tiles programmatically
        PlaceTiles();
    }

    private void Update()
    {
        //Gets which tile the player clicked on and the position
        //This is purely just for Debugging
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPos = tileMap.WorldToCell(mousePos);
            TileBase clickedTile = tileMap.GetTile(gridPos);
            //Debug.Log(clickedTile + " " + gridPos);
        }
    }
    #endregion
    #region Tile Placement and Management
    /// <summary>
    /// Takes in the the current object and puts all its children in a list
    /// </summary>
    /// <param name="parent"></param>
    /// <returns></returns>
    List<GameObject> GetChildren(Transform parent)
    {
        List<GameObject> children = new List<GameObject>();

        foreach (Transform child in parent)
        {
            children.Add(child.gameObject);
        }
        return children;
    }

    /// <summary>
    /// Places the placable tile objects according to what type of
    /// tiles are made in the map. The current thing that it does
    /// is find all the grass blocks in a tilemap and places a placable tile there
    /// </summary>
    private void PlaceTiles()
    {
        //Finds the Lower left bound position and size of the tile map so that I
        //can iterate through it and check each tile individually
        BoundsInt bounds = tileMap.cellBounds;

        //Finds the upper and lower bounds of the tilemap and stores them
        int lowerXBound = bounds.position.x;
        int upperXBound = bounds.position.x + bounds.size.x;
        int lowerYBound = bounds.position.y;
        int upperYBound = bounds.position.y + bounds.size.y;
        Debug.Log(bounds);

        //Goes through each of the tiles in the x position starting at the lower bound and iterates to the upper
        for (int i = lowerXBound; i <= upperXBound; i++)
        {
            //Goes through each of the tiles in the y position starting at the lower bound and iterates to the upper
            for (int j = lowerYBound; j <= upperYBound; j++)
            {
                //Stores the current grid position being checked finds the tile at that position
                Vector3Int gridPos = new Vector3Int(i, j, 0);
                TileBase currentTile = tileMap.GetTile(gridPos);

                //If the tile found at the position is a placable tile then it will add a placable tile game object there
                if (currentTile == placableTile)
                {
                    //Stores the positions that are possible
                    tilePositions.Add(new Vector3(i + 0.5f, j + 0.5f, 0));

                    //Places a new placable tile object at the current grid position
                    GameObject newTile = Instantiate(tile, new Vector3(i + 0.5f, j + 0.5f, 0), Quaternion.identity);
                    Tile newTileScript = newTile.GetComponent<Tile>();
                    newTileScript.tileManager = gameObject.GetComponent<TileManager>();
                    newTile.transform.parent = gameObject.transform;
                }

            }
        }
    }
    #endregion
}
