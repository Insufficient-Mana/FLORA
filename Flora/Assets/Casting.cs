using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casting : MonoBehaviour
{
    [Header("Managers and Animator")] 
    [SerializeField] private TileManager tileManager;
    [SerializeField] private SlotManager slotManager;
    [SerializeField] private Animator playerAnimator;

    [Header("Tile Tracker")]
    public GameObject clickedTile;

    [Header("Casting Information")]
    public bool channel;
    public AudioSource channeling;
    public AudioSource cast;

    #region Start and Update
    // Start is called before the first frame update
    void Start()
    {
        //Finds the tile manager and slot manager in each level
        tileManager = GameObject.FindGameObjectWithTag("TileManager").GetComponent<TileManager>();
        slotManager = GameObject.FindGameObjectWithTag("SlotManager").GetComponent<SlotManager>();

        //finds the animator on the player
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
    #endregion
    #region Casting Functions
    /// <summary>
    /// Purely plays the channeling sound effect
    /// </summary>
    public void Channel()
    {
        channeling.Play();
    }

    /// <summary>
    /// Plays the sound and animation to cast a spell
    /// </summary>
    public void BeginCasting()
    {
        playerAnimator.Play("cast");
        cast.Play();
    }

    /// <summary>
    /// Allows to invoke a cast from an outside script
    /// </summary>
    public void InvokeCast()
    {
        Invoke(nameof(Cast), .4f);
    }

    /// <summary>
    /// The cast action is performed allowing the user to place a seed and updating tiles
    /// </summary>
    private void Cast()
    {
        //The user will plant the seed of whichever slot is currenlt selected
        slotManager.currentSlot.PlantSeed();

        //clicked tile is set outside of this script in the tile script
        Tile currentTile = clickedTile.GetComponent<Tile>();

        //Whatever tile that the player clicks on is set to occupied once the casting is successful
        currentTile.occupied = true;

        //The player is hovering over the tile
        if (tileManager.currentTile != null)
        {
            //The tile that the player clicked on and the hovered tile are the same
            if (currentTile == tileManager.currentTile.GetComponent<Tile>())
            {
                //it will turn the tile to red showing that it is now succefully casted and the tile is now occupied
                currentTile.sprite.color = currentTile.red;
            }
        }

        //no matter what the clicked tile is reset once casting is over
        clickedTile = null;
    }
    #endregion
}
