using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultFlower : MonoBehaviour
{
    [Header("Flower Information")]
    public LayerMask Ignore;
    public Tile placedTile;

    #region Start
    // Start is called before the first frame update
    void Start()
    {
        CheckTileBelow();
    }
    #endregion
    #region Flower Checks
    public void CheckTileBelow()
    {
        //Does a raycast 4 blocks below to get if there is a tile below and sets the flower's tile to it
        RaycastHit2D hit = Physics2D.Raycast(transform.position - new Vector3(0, 1, 0), Vector2.down, 4,~Ignore);

        //If it hits a collider and it is a tile it will occupy the tile
        if(hit.collider != null)
        {
            Debug.Log("Hit");
            if(hit.collider.gameObject.CompareTag("Tile"))
            { 
                placedTile = hit.collider.gameObject.GetComponent<Tile>();
                placedTile.occupied = true;
            }
        }
    }
    #endregion
}
