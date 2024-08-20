using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [Header("Day Information")]
    public int maxDays;
    public int currentDay;

    [Header("Game Information")]
    public Vector3 spawnPoint;
    public GameObject flowerManager;
    public GameObject player;
    
    [Header("Altar Sprites")]
    public List<Sprite> altars;
    public SpriteRenderer altarSprite;

    [Header("Orb Information")]
    public GameObject orbSprite1;
    public GameObject orbSprite2;
    public GameObject orbSprite3;
    public GameObject crystal;
    public List<Sprite> TwoDayOrbs;
    public List<Sprite> ThreeDayOrbs;
    public List<Sprite> FourDayOrbs;

    [Header("Altar Information")]
    public bool canTeleport;
    public int currentLevel;

    [Header("Animation/Sounds")]
    public Animator transitionAnimator;
    public AudioSource win;

    #region Start
    private void Start()
    {
        //Gets the player and flower manager
        flowerManager = GameObject.FindGameObjectWithTag("FlowerManager");
        player = GameObject.FindGameObjectWithTag("Player");

        //Gets the player's initial spawn point and allows the player to teleport
        spawnPoint = new Vector3(player.transform.position.x,player.transform.position.y,0);
        canTeleport = true;

        //Sets all the orbs to false automatically
        orbSprite1.SetActive(false);
        orbSprite2.SetActive(false);
        orbSprite3.SetActive(false);
        crystal.SetActive(false);

        //Changes the sprites accordingly
        SpriteChanger();

        //Gets the day night transition to perform a play from this script
        transitionAnimator = GameObject.FindGameObjectWithTag("DayNightTransition").GetComponent<Animator>();
    }
    #endregion
    #region Programatically Change Sprites
    /// <summary>
    /// Chenges the altar and the orb sprites according to how many days long the level is
    /// </summary>
    public void SpriteChanger()
    {
        //There is a seperate array for each of the orb amounts so they need to each be placed programmatically
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

    /// <summary>
    /// Sets the orbs to active depending on the day
    /// </summary>
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
    #endregion
    #region Trigger Events
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Checks if the collision is with the player and if the player can teleport
        if(collision.gameObject.CompareTag("Player") && canTeleport)
        {
            //Adds the day amount and unlocks the next level if the player beats the level
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
                //Plays the win sound and starts the transition
                win.Play();
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
            //The player can't teleport again until it exits the trigger
            canTeleport = true;
        }
    }
    #endregion
}
