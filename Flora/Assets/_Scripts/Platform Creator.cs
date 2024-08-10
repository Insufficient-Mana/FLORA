using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCreator : MonoBehaviour
{
    public GameObject flowerType;
    public float spawnUnits;
    public float growTime;
    public bool stemGrown;

    private void Start()
    {
        stemGrown = false;
    }

    public void CreatePlatform()
    {
        growTime -= 1;
        if(growTime == 0)
        {
            PlatformDecay decayScript = flowerType.GetComponent<PlatformDecay>();
            decayScript.associatedSeed = gameObject;
            Instantiate(flowerType,gameObject.transform.position + new Vector3(0,spawnUnits,0), Quaternion.identity);
            stemGrown = true;
        }
        else
        {
            GrowStem();
        }
    }

    public void GrowStem()
    {

    }
}
