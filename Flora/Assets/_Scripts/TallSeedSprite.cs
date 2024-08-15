using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TallSeedSprite : MonoBehaviour
{
    public PlatformCreator creator;
    public SpriteRenderer lowerSprite;
    public SpriteRenderer upperSprite;
    public SpriteRenderer seedSprite;
    public List<Sprite> sprList;
   

    // Update is called once per frame
    void Update()
    {
        spriteChanger();
    }

    public void spriteChanger()
    {
        switch (creator.growTime)
        {
            case 2:
                seedSprite.sprite = sprList[0];
                break;
            case 1:
                seedSprite.sprite = sprList[5];
                lowerSprite.sprite = sprList[1];
                break;
            case 0:
                upperSprite.sprite = sprList[2];
                break;
            case -1:
                upperSprite.sprite = sprList[3];
                lowerSprite.sprite = sprList[4];
                break;
        }
    }
}
