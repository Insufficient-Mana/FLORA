using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerEnterHandler,IPointerDownHandler, IPointerExitHandler, IPointerUpHandler
{
    public TileManager tileManager;
    public SpriteRenderer sprite;

    public bool occupied;
    public bool overTile;
    public bool obstructed;

    public Color red = new Color(255, 0, 0, 0.2f);
    public Color green = new Color(0, 255, 0, 0.2f);


    public void OnPointerEnter(PointerEventData eventData)
    {
        tileManager.currentTile = gameObject;
        if (tileManager.slotManager.currentSlot != null)
        {
            CheckObstructed();
        }
        else
        {
            obstructed = false;
        }
        if (occupied)
        {
            sprite.color = red;
        }
        else if (!occupied && obstructed)
        {
            sprite.color = red;
        }
        else if (!occupied && !obstructed)
        {
            sprite.color = green;
        }
        overTile = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tileManager.currentTile = null;
        sprite.color = new Color(1, 1, 1, 0);
        overTile = false;
 
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Cast");
        if (!occupied && overTile && !obstructed)
        {
            if (tileManager.slotManager.currentSlot != null)
            {
                tileManager.slotManager.currentSlot.placedTile = gameObject;
                
                if (tileManager.slotManager.currentSlot.amount > 0)
                {
                    tileManager.playerCasting.clickedTile = tileManager.currentTile;
                    tileManager.playerCasting.BeginCasting();
                    tileManager.playerCasting.InvokeCast();
                }
            }
        }
    }

    private void Awake()
    {
        occupied = false;
        sprite = GetComponent<SpriteRenderer>();
    }

    public void CheckObstructed()
    {
        PlatformCreator platformHeight = tileManager.slotManager.currentSlot.seedType.GetComponent<PlatformCreator>();
        float plantheight = platformHeight.spawnUnits;
        RaycastHit2D hit = Physics2D.Raycast(transform.position+new Vector3(0,1,0), Vector2.up, plantheight-1);
        if(hit.collider != null)
        {
            if(!hit.collider.gameObject.CompareTag("Thorns"))
            {
                obstructed = true;
            }
        }
        else
        {
            obstructed = false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
}
