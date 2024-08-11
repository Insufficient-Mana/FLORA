using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigFlower : MonoBehaviour
{
    public PlatformDecay decayScript;
    public int platformHeight;
    public int maxHeight;
    private void Start()
    {
        decayScript = GetComponent<PlatformDecay>();
    }

    public void calculateAndAddHeight()
    {
        PlatformCreator seedScript = decayScript.associatedSeed.GetComponent<PlatformCreator>();
        platformHeight = 1;
        gameObject.transform.position = decayScript.gameObject.transform.position + new Vector3(0, platformHeight, 0);

    }


}
