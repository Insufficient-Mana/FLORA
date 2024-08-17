using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorns : MonoBehaviour
{

    public GameObject portal;
    public Portal portalScript;
    public AudioSource die;
    private void Start()
    {
        portal = GameObject.FindGameObjectWithTag("Portal");
        portalScript = portal.gameObject.GetComponent<Portal>();

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("die");
            collision.gameObject.transform.position = portalScript.spawnPoint;
            die.Play();
        }
    }
}
