using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCreator : MonoBehaviour
{
    [Header("Seed Information")]
    public GameObject flowerType;
    public float spawnUnits;
    public float growTime;
    public bool stemGrown;
    public GameObject placedTile;

    #region Start
    private void Start()
    {
        stemGrown = false;
    }
    #endregion
    #region Platform Creation Functions
    /// <summary>
    /// Reduces the countdown to make a platform
    /// </summary>
    public void ReduceGrowTime()
    {
        growTime -= 1;
    }

    /// <summary>
    /// Creates a new platform when the timer reaches zero
    /// </summary>
    public void CreatePlatform()
    {
        if(growTime == 0)
        {
            //Gets the decay script of the flower and sets it's associated seed as this object
            PlatformDecay decayScript = flowerType.GetComponent<PlatformDecay>();
            decayScript.associatedSeed = gameObject;

            //creates the platform at the amount of spawn units for this specific type of flower and sets the stem grown to true
            Instantiate(flowerType,gameObject.transform.position + new Vector3(0,spawnUnits,0), Quaternion.identity);
            stemGrown = true;
        }
    }
    #endregion
    #region Deletion
    public void DeleteSeed()
    {
        Tile tile = placedTile.GetComponent<Tile>();
        tile.occupied = false;
        tile.sprite.color = new Color(0, 255, 0, 0f);
        Destroy(gameObject);
    }
    #endregion
}
