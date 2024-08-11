using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public int maxDays;
    public int currentDay;
    public Vector3 spawnPoint;
    public GameObject flowerManager;
    public GameObject player;
    public List<Sprite> altars;
    public bool canTeleport;

    public SpriteRenderer altarSprite;
    public GameObject orbSprite1;
    public GameObject orbSprite2;
    public GameObject orbSprite3;
    public GameObject crystal;
    public List<Sprite> TwoDayOrbs;
    public List<Sprite> ThreeDayOrbs;
    public List<Sprite> FourDayOrbs;


    public int currentLevel;

    public Animator transitionAnimator;

    private void Start()
    {
        flowerManager = GameObject.FindGameObjectWithTag("FlowerManager");
        player = GameObject.FindGameObjectWithTag("Player");
        spawnPoint = new Vector3(player.transform.position.x,player.transform.position.y,0);
        canTeleport = true;
        orbSprite1.SetActive(false);
        orbSprite2.SetActive(false);
        orbSprite3.SetActive(false);
        crystal.SetActive(false);
        SpriteChanger();

        transitionAnimator = GameObject.FindGameObjectWithTag("DayNightTransition").GetComponent<Animator>();
    }

    public void SpriteChanger()
    {
        if(maxDays == 2)
        {
            altarSprite.sprite = altars[0];
            SpriteRenderer sprite = orbSprite1.GetComponent<SpriteRenderer>();
            sprite.sprite = TwoDayOrbs[0];
        }
        else if(maxDays == 3)
        {
            altarSprite.sprite = altars[1];
            SpriteRenderer sprite = orbSprite1.GetComponent<SpriteRenderer>();
            sprite.sprite = ThreeDayOrbs[0];
            SpriteRenderer sprite2 = orbSprite2.GetComponent<SpriteRenderer>();
            sprite2.sprite = ThreeDayOrbs[1];
        }
        else if(maxDays == 4)
        {
            altarSprite.sprite = altars[2];
            SpriteRenderer sprite = orbSprite1.GetComponent<SpriteRenderer>();
            sprite.sprite = FourDayOrbs[0];
            SpriteRenderer sprite2 = orbSprite2.GetComponent<SpriteRenderer>();
            sprite2.sprite = FourDayOrbs[1];
            SpriteRenderer sprite3 = orbSprite3.GetComponent<SpriteRenderer>();
            sprite3.sprite = FourDayOrbs[2];
        }
    }

    public void ChangeOrb()
    {
        if (maxDays == 2)
        {
            if(currentDay == 1)
            {
                orbSprite1.SetActive(true);
                crystal.SetActive(true);
            }
        }
        else if (maxDays == 3)
        {
            if(currentDay == 1)
            {
                orbSprite1.SetActive(true);
            }
            else if(currentDay == 2)
            {
                orbSprite1.SetActive(true);
                orbSprite2.SetActive(true);
                crystal.SetActive(true);
            }
        }
        else if (maxDays == 4)
        {
            if (currentDay == 1)
            {
                orbSprite1.SetActive(true);
            }
            else if (currentDay == 2)
            {
                orbSprite1.SetActive(true);
                orbSprite2.SetActive(true);
            }
            else if (currentDay == 3)
            {
                orbSprite1.SetActive(true);
                orbSprite2.SetActive(true);
                orbSprite3.SetActive(true);
                crystal.SetActive(true);
            }
        }
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && canTeleport)
        {
            currentDay += 1;
            if (currentDay == maxDays)
            {
                //unlock next level
                if (PlayerPrefs.GetInt("HighestLevelUnlocked") <= currentLevel)
                {
                    PlayerPrefs.SetInt("HighestLevelUnlocked", currentLevel + 1);
                }
                SceneManager.LoadScene("Menu");
            }
            else
            {
                transitionAnimator.Play("begin");
                canTeleport = false;
                player.transform.position = spawnPoint;
                PlatformManager manager = flowerManager.GetComponent<PlatformManager>();
                manager.GrowAndDecay();

                ChangeOrb();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canTeleport = true;
        }
    }
}
