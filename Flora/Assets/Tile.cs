using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler
{
    public GameObject manager;
    public TileManager tileManager;

    public bool occupied;
    public bool valid;

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!occupied)
        {

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tileManager.currentTile = gameObject;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tileManager.currentTile = null;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        manager = gameObject.transform.parent.gameObject;
        tileManager = manager.gameObject.AddComponent<TileManager>();
        occupied = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
