using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public List<GameObject> Platforms;
    public List<GameObject> Seeds;
    // Start is called before the first frame update
    void Start()
    {
        GatherAllPlatforms();
        GatherAllSeeds();
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

        }
    }
}
