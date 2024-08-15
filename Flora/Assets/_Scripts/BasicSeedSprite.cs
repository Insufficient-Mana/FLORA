using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class BasicSeedSprite : MonoBehaviour
{
    public PlatformCreator creator;
    public SpriteRenderer sprite;
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
            case 1:
                sprite.sprite = sprList[0];
                break;
            case 0:
                sprite.sprite = sprList[1];
                break;
            case -1:
                sprite.sprite = sprList[2];
                break;
        }
    }
}
