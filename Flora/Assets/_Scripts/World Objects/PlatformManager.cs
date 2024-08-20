using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [Header("Platforms and Seeds")]
    public List<GameObject> Platforms;
    public List<GameObject> Seeds;

    #region Daily Action
    /// <summary>
    /// Grows seeds and decays any platforms
    /// </summary>
    public void GrowAndDecay()
    {
        //collects all flowers and seeds
        GatherAllPlatforms();
        GatherAllSeeds();

        //Performs the actions on all of the seeds and flowers
        DecayFlowers();
        GrowSeeds();

        //Resets the flowers and seeds
        Platforms = new List<GameObject>();
        Seeds = new List<GameObject>();
    }
    #endregion
    #region Gather Seeds/Flowers
    /// <summary>
    /// Finds a tag on each platform and adds them to a list of flowers
    /// </summary>
    public void GatherAllPlatforms()
    {
        GameObject[] platforms;
        platforms = GameObject.FindGameObjectsWithTag("Flower");

        foreach (GameObject platform in platforms)
        { 
            Platforms.Add(platform);
        }
    }

    /// <summary>
    /// Finds a tag on each platform and adds them to a list of seeds
    /// </summary>
    public void GatherAllSeeds()
    {
        GameObject[] seeds;
        seeds = GameObject.FindGameObjectsWithTag("Seed");

        foreach (GameObject seed in seeds)
        {
            Seeds.Add(seed);
        }
    }
    #endregion
    #region Platform And Seed Actions
    /// <summary>
    /// Goes through each of the flower platforms and decreases their life span
    /// </summary>
    public void DecayFlowers()
    {
        //Goes through each of the platforms in a list
        foreach(GameObject platform in Platforms)
        {
            //Gets the decay script in each of the platforms and decays them
            PlatformDecay decayScript = platform.GetComponent<PlatformDecay>();
            decayScript.DecreaseLifespan();
            
            //Gets the flower type in each platform
            FlowerType type = platform.GetComponent<FlowerType>();

            //Checks to see if it is a big flower and performs a uniques function to it
            if(type.type == FlowerType.FlowerTypes.Big)
            {
                BigFlower bigScript = platform.GetComponent<BigFlower>();
                bigScript.calculateAndAddHeight();
            }
            
        }
    }

    /// <summary>
    /// Goes through each of the seeds and grows them
    /// </summary>
    public void GrowSeeds()
    {
        //goes through each of the seeds in the list
        foreach(GameObject seed in Seeds)
        {
            //Reduces the grow time by getting the platform creator script
            PlatformCreator creator = seed.GetComponent<PlatformCreator>();
            creator.ReduceGrowTime();

            //If the stem is not grown yet it tries to create a platform
            if (creator.stemGrown == false)
            {
                creator.CreatePlatform();
            }
            
        }
    }
    #endregion
}
