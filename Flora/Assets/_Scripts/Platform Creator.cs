using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCreator : MonoBehaviour
{
    public GameObject flowerType;
    public float spawnUnits;
    public float growTime;

    public void CreatePlatform()
    {
        if(growTime == 0)
        {
            Instantiate(flowerType,gameObject.transform.position + new Vector3(0,spawnUnits,0), Quaternion.identity);
        }
    }
}
