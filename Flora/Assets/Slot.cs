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
    public GameObject plantIcon;
    public Vector2 plantIconPosition;
    public bool overSlot;
    public bool dragging;
    public SlotManager slotManager;
    public GameObject placedTile;
    public int amount;

    

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
        plantIcon.transform.position = plantIconPosition;
        slotManager.currentSlot = GetComponent<Slot>();

    }

    // Start is called before the first frame update
    void Start()
    {
        overSlot = false;
        plantIconPosition = new Vector2(plantIcon.transform.position.x,plantIcon.transform.position.y);
        slotManager = gameObject.transform.parent.GetComponent<SlotManager>();
    }

    // Update is called once per frame
    void Update()
    {
        textBox.text = amount.ToString();
        Image image = GetComponent<Image>();
        if (slotManager.currentSlot == gameObject.GetComponent<Slot>())
        {
            image.color = new Color(255, 185, 185);
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
