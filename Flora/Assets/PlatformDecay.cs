using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDecay : MonoBehaviour
{
    public int platformLifespan;
    
    public void DecreaseLifespan()
    {
        platformLifespan -= 1;
    }

    public void CheckLifespan()
    {
        if(platformLifespan <= 0) 
        { 
            Destroy(gameObject);
        }
    }
}
