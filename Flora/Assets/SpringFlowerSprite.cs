using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringFlowerSprite : MonoBehaviour
{
    public PlatformDecay creator;
    public SpriteRenderer sprite;
    public SpriteRenderer mush1;
    public SpriteRenderer mush2;
    public SpriteRenderer mush3;
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
            case 2:
                mush1.gameObject.SetActive(false);
                break;
            case 1:
                mush2.gameObject.SetActive(false);
                sprite.sprite = sprList[1];
                mush3.sprite = sprList[2];
                break;
        }
    }
}


