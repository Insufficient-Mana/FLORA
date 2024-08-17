using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    public GameObject currentTile;
    public List<GameObject> tiles;
    public GameObject slotManagerObject;
    public SlotManager slotManager;
    public Casting playerCasting;

    public Tilemap tileMap;
    public TileBase placableTile;
    public List<Vector3> tilePositions;
    public GameObject tile;
    private void Start()
    {
        tiles = GetChildren(gameObject.transform);
        slotManagerObject = GameObject.FindGameObjectWithTag("SlotManager");
        slotManager = slotManagerObject.GetComponent<SlotManager>();
        playerCasting = GameObject.FindGameObjectWithTag("Player").GetComponent<Casting>();

        BoundsInt bounds = tileMap.cellBounds;
        int lowerXBound = bounds.position.x;
        int upperXBound = bounds.position.x + bounds.size.x;
        int lowerYBound = bounds.position.y;
        int upperYBound = bounds.position.y + bounds.size.y;
        Debug.Log(bounds);
        for (int i = lowerXBound; i <= upperXBound; i++)
        {
            for(int j = lowerYBound; j <= upperYBound;j++)
            {
                Vector3Int gridPos = new Vector3Int(i, j, 0);
                TileBase currentTile = tileMap.GetTile(gridPos);
                if(currentTile == placableTile)
                {
                    tilePositions.Add(new Vector3(i+0.5f,j+0.5f,0));
                    GameObject newTile = Instantiate(tile, new Vector3(i + 0.5f, j + 0.5f, 0), Quaternion.identity);
                    Tile newTileScript = newTile.GetComponent<Tile>();
                    newTileScript.tileManager = gameObject.GetComponent<TileManager>();
                    newTile.transform.parent = gameObject.transform;
                }

            }
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPos = tileMap.WorldToCell(mousePos);
            TileBase clickedTile = tileMap.GetTile(gridPos);
            Debug.Log(clickedTile + " " + gridPos);
        }
    }
    List<GameObject> GetChildren(Transform parent)
    {
        List<GameObject> children = new List<GameObject>();

        foreach (Transform child in parent)
        {
            children.Add(child.gameObject);
        }
        return children;
    }
}
