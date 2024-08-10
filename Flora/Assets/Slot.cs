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
    public Camera mainCamera;

    

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
        PlantSeed();

    }

    // Start is called before the first frame update
    void Start()
    {
        overSlot = false;
        plantIconPosition = new Vector2(plantIcon.transform.position.x,plantIcon.transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if(dragging == true)
        {
            plantIcon.transform.position = Input.mousePosition;
        }
    }

    public void PlantSeed()
    {
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Instantiate(seedType,new Vector3(worldPos.x,worldPos.y,0),Quaternion.identity);
    }
}
