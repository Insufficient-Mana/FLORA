using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorns : MonoBehaviour
{
    [Header("Portal Information")]
    public GameObject portal;
    public Portal portalScript;

    [Header("Audio")]
    public AudioSource die;

    #region Start and Collision
    private void Start()
    {
        //Gets the portal component once the script find the portal
        portal = GameObject.FindGameObjectWithTag("Portal");
        portalScript = portal.gameObject.GetComponent<Portal>();

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Resets the player to their spawn point if they collide with thorns
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("die");
            collision.gameObject.transform.position = portalScript.spawnPoint;
            die.Play();
        }
    }
    #endregion
}
