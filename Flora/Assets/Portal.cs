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
    public bool canTeleport;

    public int currentLevel;

    private void Start()
    {
        flowerManager = GameObject.FindGameObjectWithTag("FlowerManager");
        player = GameObject.FindGameObjectWithTag("Player");
        spawnPoint = new Vector3(player.transform.position.x,player.transform.position.y,0);
        canTeleport = true;
    }

    private void Update()
    {
        if(currentDay == maxDays)
        {
            //unlock next level
            if (PlayerPrefs.GetInt("HighestLevelUnlocked") <= currentLevel)
            {
                PlayerPrefs.SetInt("HighestLevelUnlocked", currentLevel + 1);
            }
            SceneManager.LoadScene("Menu");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && canTeleport)
        {
            canTeleport = false;
            player.transform.position = spawnPoint;
            PlatformManager manager = flowerManager.GetComponent<PlatformManager>();
            manager.GrowAndDecay();
            currentDay += 1;
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
