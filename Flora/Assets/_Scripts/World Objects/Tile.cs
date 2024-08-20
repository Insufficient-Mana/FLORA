using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerEnterHandler,IPointerDownHandler, IPointerExitHandler, IPointerUpHandler
{
    [Header("Tile Management")]
    public TileManager tileManager;
    public SpriteRenderer sprite;

    [Header("Tile States")]
    public bool occupied;
    public bool overTile;
    public bool obstructed;

    [Header("Colors")]
    public Color red = new Color(255, 0, 0, 0.2f);
    public Color green = new Color(0, 255, 0, 0.2f);

    #region Tile Events

    /// <summary>
    /// Pointer event for when you press on it 
    /// Necessary for it to be here in order for the 
    /// pointer up event to function
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {

    }

    /// <summary>
    /// When the mouse is over the tile
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        //The current tile object in the manager is set to this one
        tileManager.currentTile = gameObject;

        //When the current slot is filled then it will check if the plant is to bigg to be placed there
        if (tileManager.slotManager.currentSlot != null)
        {
            CheckObstructed();
        }
        else
        {
            obstructed = false;
        }

        //Turns the sprite to red if it is obstructed or occupied otherwise it is placable and turns green
        if(occupied || obstructed)
        {
            sprite.color = red;
        }
        else if (!occupied && !obstructed)
        {
            sprite.color = green;
        }
        overTile = true;
    }

    /// <summary>
    /// When the pointer leaves the tile it turns the current tile to null and makes it transparent
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        tileManager.currentTile = null;
        sprite.color = new Color(1, 1, 1, 0);
        overTile = false;
    }

    /// <summary>
    /// When the player clicks on the  tile it will cast a spell
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Cast");
        //When the conditions are right the script can continue to see if the player can cast a spell
        if (!occupied && overTile && !obstructed && tileManager.slotManager.currentSlot != null)
        {
            //Sets the placed tile to this object
            tileManager.slotManager.currentSlot.placedTile = gameObject;

            //if the current slots amount is greater than zero it will cast the spell
            if (tileManager.slotManager.currentSlot.amount > 0)
            {
                tileManager.playerCasting.clickedTile = tileManager.currentTile;
                tileManager.playerCasting.BeginCasting();
                tileManager.playerCasting.InvokeCast();
            }
        }
    }
    #endregion
    #region Awake
    private void Awake()
    {
        occupied = false;
        sprite = GetComponent<SpriteRenderer>();
    }
    #endregion
    #region Check Functions
    /// <summary>
    /// Checks if the currently selected flower is too tall for where it is being placed
    /// </summary>
    public void CheckObstructed()
    {
        //Gets the height of the flower and checks if it there is any ground above it by the flower's height
        PlatformCreator platformHeight = tileManager.slotManager.currentSlot.seedType.GetComponent<PlatformCreator>();
        float plantheight = platformHeight.spawnUnits;
        RaycastHit2D hit = Physics2D.Raycast(transform.position+new Vector3(0,1,0), Vector2.up, plantheight-1);

        //if the collider returns an object and the objects aren't thorns or the player then it can't be placed there
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
    #endregion

}
