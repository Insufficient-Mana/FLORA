using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public List<GameObject> Platforms;
    public List<GameObject> Seeds;
    // Start is called before the first frame update

    public void GrowAndDecay()
    {
        
        GatherAllPlatforms();
        GatherAllSeeds();
        DecayFlowers();
        GrowSeeds();
        Platforms = new List<GameObject>();
        Seeds = new List<GameObject>();
    }

    public void GatherAllPlatforms()
    {
        GameObject[] platforms;
        platforms = GameObject.FindGameObjectsWithTag("Flower");

        foreach (GameObject platform in platforms)
        { 
            Platforms.Add(platform);
        }
    }   
    
    public void GatherAllSeeds()
    {
        GameObject[] seeds;
        seeds = GameObject.FindGameObjectsWithTag("Seed");

        foreach (GameObject seed in seeds)
        {
            Seeds.Add(seed);
        }
    }

    public void DecayFlowers()
    {
        foreach(GameObject platform in Platforms)
        {
            PlatformDecay decayScript = platform.GetComponent<PlatformDecay>();
            decayScript.DecreaseLifespan();

            FlowerType type = platform.GetComponent<FlowerType>();
            if(type.type == FlowerType.FlowerTypes.Big)
            {
                BigFlower bigScript = platform.GetComponent<BigFlower>();
                bigScript.calculateAndAddHeight();
            }
            
        }
    }

    public void GrowSeeds()
    {
        foreach(GameObject seed in Seeds)
        {
            PlatformCreator creator = seed.GetComponent<PlatformCreator>();
            if(creator.stemGrown == false)
            {
                creator.CreatePlatform();
            }
        }
    }
}
