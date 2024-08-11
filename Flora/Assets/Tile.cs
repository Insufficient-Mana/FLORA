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

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tileManager.currentTile = gameObject;
        if(occupied)
        {
            sprite.color = new Color(255, 0, 0, 0.2f);
        }
        else if(!occupied)
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
        if (!occupied && overTile)
        {
            if (tileManager.slotManager.currentSlot != null)
            {
                tileManager.slotManager.currentSlot.placedTile = gameObject;
                if (tileManager.slotManager.currentSlot.amount > 0)
                {
                    tileManager.slotManager.currentSlot.PlantSeed();
                    occupied = true;
                    sprite.color = new Color(255, 0, 0, 0.2f);
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
}
