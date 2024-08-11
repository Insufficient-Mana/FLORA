using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler
{
    public GameObject manager;
    public TileManager tileManager;
    public SpriteRenderer sprite;

    public bool occupied;
    public bool valid;
    public bool overTile;
    public bool obstructed;
    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tileManager.currentTile = gameObject;
        if (tileManager.slotManager.currentSlot != null)
        {
            CheckObstructed();
        }
        if(occupied)
        {
            sprite.color = new Color(255, 0, 0, 0.2f);
        }
        else if(!occupied && obstructed)
        {
            sprite.color = new Color(255, 0, 0, 0.2f);
        }
        else if(!occupied && !obstructed)
        {
            sprite.color = new Color(0, 255, 0, 0.2f);
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
        if (!occupied && overTile && !obstructed)
        {
            if (tileManager.slotManager.currentSlot != null)
            {
                tileManager.slotManager.currentSlot.placedTile = gameObject;
                if (tileManager.slotManager.currentSlot.amount > 0)
                {
                    StartCoroutine(Cast());
                    
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        manager = gameObject.transform.parent.gameObject;
        tileManager = manager.gameObject.AddComponent<TileManager>();
        occupied = false;
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public IEnumerator Cast()
    {
        tileManager.slotManager.jump.canJump = false;
        tileManager.slotManager.movement.canMove = false;
        yield return new WaitForSeconds(.5f);
        tileManager.slotManager.currentSlot.PlantSeed();
        occupied = true;
        sprite.color = new Color(255, 0, 0, 0.2f);
        tileManager.slotManager.jump.canJump = true;
        tileManager.slotManager.movement.canMove = true;
    }
}
