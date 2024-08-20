using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDecay : MonoBehaviour
{
    [Header("Flower Information")]
    public int platformLifespan;
    public GameObject associatedSeed;

    #region Lifespan Functions
    /// <summary>
    /// Decreases the life span of the platform and checks if it is ready to delete
    /// </summary>
    public void DecreaseLifespan()
    {
        platformLifespan -= 1;
        CheckLifespan();
    }
    
    /// <summary>
    /// Check if the the life span of the flower is up and deletes it accordingly
    /// </summary>
    public void CheckLifespan()
    {
        //When the platform life gets to below zero it deletes it
        if(platformLifespan <= 0) 
        {
            //Disables the collider of the object this is attached to
            Collider2D collider = gameObject.GetComponent<Collider2D>();
            collider.enabled = false;

            //Flower type helps to see how the decay script will react with specific flowers
            FlowerType flowerType = GetComponent<FlowerType>();

            //Deletes the seed if the flowers is not an established default flower and it has an associated seed
            if(flowerType.type != FlowerType.FlowerTypes.Established && associatedSeed != null)
            {
                PlatformCreator seedScript = associatedSeed.GetComponent<PlatformCreator>();
                seedScript.DeleteSeed();
            }

            //Checks to see if it is a default flower
            if(flowerType.type == FlowerType.FlowerTypes.Established)
            {
                DefaultFlower defaultFlower = gameObject.GetComponent<DefaultFlower>();
                //Sets the default flower's associated tile to null
                if (defaultFlower.placedTile != null)
                {
                    defaultFlower.placedTile.occupied = false;
                }
            }

            //Destroys the flower
            Destroy(gameObject);
        }
    }
    #endregion
}
