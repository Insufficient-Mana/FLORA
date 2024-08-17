using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casting : MonoBehaviour
{
    [SerializeField] private TileManager tileManager;
    [SerializeField] private SlotManager slotManager;
    [SerializeField] private Animator playerAnimator;
    public GameObject clickedTile;
    public bool channel;
    public AudioSource channeling;
    public AudioSource cast;
    // Start is called before the first frame update
    void Start()
    {
        tileManager = GameObject.FindGameObjectWithTag("TileManager").GetComponent<TileManager>();
        slotManager = GameObject.FindGameObjectWithTag("SlotManager").GetComponent<SlotManager>();
        playerAnimator = GetComponent<Animator>();
        channel = false;
    }

    private void Update()
    {
        //channeling state check
        if(slotManager.currentSlot != null)
        {
            playerAnimator.SetBool("isChanneling", true);
            
        }
        else
        {
            playerAnimator.SetBool("isChanneling", false);
        }
    }

    public void Channel()
    {
        channeling.Play();
    }

    public void BeginCasting()
    {
        playerAnimator.Play("cast");
        cast.Play();
    }
    public void InvokeCast()
    {
        Invoke(nameof(Cast), .4f);
    }
    private void Cast()
    {
        slotManager.currentSlot.PlantSeed();
        Tile currentTile = clickedTile.GetComponent<Tile>();
        currentTile.occupied = true;
        if (tileManager.currentTile != null)
        {
            if (currentTile == tileManager.currentTile.GetComponent<Tile>())
            {
                currentTile.sprite.color = currentTile.red;
            }
        }
        clickedTile = null;
    }
}
