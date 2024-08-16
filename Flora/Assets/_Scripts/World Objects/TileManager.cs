using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject currentTile;
    public List<GameObject> tiles;
    public GameObject slotManagerObject;
    public SlotManager slotManager;
    public Casting playerCasting;

    private void Start()
    {
        tiles = GetChildren(gameObject.transform);
        slotManagerObject = GameObject.FindGameObjectWithTag("SlotManager");
        slotManager = slotManagerObject.GetComponent<SlotManager>();
        playerCasting = GameObject.FindGameObjectWithTag("Player").GetComponent<Casting>();
    }

    List<GameObject> GetChildren(Transform parent)
    {
        List<GameObject> children = new List<GameObject>();

        foreach (Transform child in parent)
        {
            children.Add(child.gameObject);
        }
        return children;
    }
}
