using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultFlower : MonoBehaviour
{

    public LayerMask Ignore;
    public Tile placedTile;
    // Start is called before the first frame update
    void Start()
    {
        CheckTileBelow();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckTileBelow()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position - new Vector3(0, 1, 0), Vector2.down, 2,~Ignore);

        if(hit.collider != null)
        {
            if(hit.collider.gameObject.CompareTag("Tile"))
            { 
                placedTile = hit.collider.gameObject.GetComponent<Tile>();
                placedTile.occupied = true;
            }
        }
    }
}
