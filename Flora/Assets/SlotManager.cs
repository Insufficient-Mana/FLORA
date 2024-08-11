using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public Slot currentSlot;
    public GameObject player;
    public LRMovement movement;
    public Jump jump;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        movement = player.GetComponent<LRMovement>();
        jump = player.GetComponent<Jump>();
    }
    private void Update()
    {
        if (!jump.isOnGround || movement.moving)
        {
            currentSlot = null;
        }
    }
}
