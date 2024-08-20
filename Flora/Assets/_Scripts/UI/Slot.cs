using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler
{
    [Header("External Scripts")]
    public SlotManager slotManager;

    [Header("Game Objects")]
    public GameObject seedType;
    public GameObject placedTile;

    [Header("Slot Information")]
    public Text textBox;
    public Vector2 plantIconPosition;
    public int amount;
    public bool highlighted;
    public bool overSlot;
    public bool dragging;

    #region Slot Mouse Events
    /// <summary>
    /// This was only included for dragging features prior to the layout change
    /// It might be removed later just in case we want to change menu interaction
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        dragging = true;
    }

    /// <summary>
    /// When the user hovers the slot overSlot is set as true
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        overSlot = true;
    }

    /// <summary>
    /// Once the user stops hovering the slot overslot will be set to false
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        overSlot = false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        dragging = false;
        highlighted = true;
        slotManager.currentSlot = GetComponent<Slot>();

    }
    #endregion
    #region Start and Update
    // Start is called before the first frame update
    void Start()
    {
        //The player will never be hovering the slot right when the game starts
        overSlot = false;

        //Slots will be childs of the slot manager so whenever the slot is made it will get the manager component from the parent
        slotManager = gameObject.transform.parent.GetComponent<SlotManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Takes the amount of remaining items in a slot and puts it in a textbox
        textBox.text = amount.ToString();

        //Finds the image component on the game object
        Image image = GetComponent<Image>();

        //if the slot the player selected is the same as the current gameobject 
        //then it updates the slot to be displayed as seleted
        if (slotManager.currentSlot == gameObject.GetComponent<Slot>())
        {
            //the slot is highlighted it will become red to show selected
            if (highlighted)
            {
                image.color = new Color(255, 0, 0);
                highlighted = false;
            }
        }
        else
        {
            //Otherwise the image is turned to red
            image.color = new Color(255, 255, 255);
        }
        
    }
    #endregion
    #region Seed Functions
    public void PlantSeed()
    {
        amount -= 1;
        PlatformCreator seedScript = seedType.GetComponent<PlatformCreator>();
        seedScript.placedTile = placedTile;
        Instantiate(seedType,new Vector3(placedTile.transform.position.x, placedTile.transform.position.y,0),Quaternion.identity);
    }
    #endregion
}
