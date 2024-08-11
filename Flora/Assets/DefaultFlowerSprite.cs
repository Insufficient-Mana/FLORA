using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultFlowerSprite : MonoBehaviour
{
    public PlatformDecay creator;
    public SpriteRenderer sprite;
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
                break;
            case 2:
                sprite.sprite = sprList[1];
                break;
            case 1:
                sprite.sprite = sprList[2];
                break;
        }
    }
}
