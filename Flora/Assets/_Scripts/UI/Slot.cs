using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler
{
    public GameObject seedType;
    public Text textBox;
    public Vector2 plantIconPosition;
    public bool overSlot;
    public bool dragging;
    public SlotManager slotManager;
    public GameObject placedTile;
    public int amount;
    public bool highlighted;

    

    public void OnPointerDown(PointerEventData eventData)
    {
        dragging = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        overSlot = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        overSlot = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        dragging = false;
        highlighted = true;
        slotManager.currentSlot = GetComponent<Slot>();

    }

    // Start is called before the first frame update
    void Start()
    {
        overSlot = false;
        slotManager = gameObject.transform.parent.GetComponent<SlotManager>();
    }

    // Update is called once per frame
    void Update()
    {
        textBox.text = amount.ToString();
        Image image = GetComponent<Image>();
        if (slotManager.currentSlot == gameObject.GetComponent<Slot>())
        {
            if (highlighted)
            {
                image.color = new Color(255, 0, 0);
                highlighted = false;
            }
        }
        else
        {
            image.color = new Color(255, 255, 255);
        }
        
    }
     
    public void PlantSeed()
    {
        amount -= 1;
        PlatformCreator seedScript = seedType.GetComponent<PlatformCreator>();
        seedScript.placedTile = placedTile;
        Instantiate(seedType,new Vector3(placedTile.transform.position.x, placedTile.transform.position.y,0),Quaternion.identity);
    }
}
