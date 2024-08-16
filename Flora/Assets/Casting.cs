using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casting : MonoBehaviour
{
    [SerializeField] private TileManager tileManager;
    [SerializeField] private SlotManager slotManager;
    [SerializeField] private Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        tileManager = GameObject.FindGameObjectWithTag("TileManager").GetComponent<TileManager>();
        slotManager = GameObject.FindGameObjectWithTag("SlotManager").GetComponent<SlotManager>();
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        //channeling state check
        if (slotManager.currentSlot != null)
        {
            playerAnimator.SetBool("isChanneling", true);
        }
        else
        {
            playerAnimator.SetBool("isChanneling", false);
        }
    }

    public void BeginCasting()
    {
        playerAnimator.Play("cast");
    }
    public void InvokeCast()
    {
        Invoke(nameof(Cast), .4f);
    }
    public void Cast()
    {
        slotManager.currentSlot.PlantSeed();
        Tile currentTile = tileManager.currentTile.GetComponent<Tile>();
        currentTile.occupied = true;
        currentTile.sprite.color = currentTile.red;
    }
}
