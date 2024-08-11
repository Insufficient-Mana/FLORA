using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultFlowerSprite : MonoBehaviour
{
    public PlatformDecay creator;
    public SpriteRenderer sprite;
    public SpriteRenderer UpperStem;
    public SpriteRenderer LowerStem;
    public List<Sprite> sprList;

    // Update is called once per frame
    void Update()
    {
        spriteChanger();
    }

    public void spriteChanger()
    {
        switch (creator.platformLifespan)
        { 
            case 3:
                sprite.sprite = sprList[0];
                UpperStem.sprite = sprList[2];
                LowerStem.sprite = sprList[5];
                break;
            case 2:
                sprite.sprite = sprList[0];
                UpperStem.sprite = sprList[3];
                LowerStem.sprite = sprList[5];
                break;
            case 1:
                sprite.sprite = sprList[1];
                UpperStem.sprite = sprList[4];
                LowerStem.sprite = sprList[6];
                break;
        }
    }
}
