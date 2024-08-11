using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MovingFlower : MonoBehaviour
{
    public Vector2 spawnPoint;
    public Vector2 rightPoint;
    public Vector2 leftPoint;
    public bool movingRight;
    public float moveSpeed;
    private void Start()
    {
        spawnPoint = transform.position;
        rightPoint = spawnPoint + new Vector2(1, 0);
        leftPoint = spawnPoint + new Vector2(-1,0);
    }
    private void Update()
    {
        if(movingRight)
        {
            transform.position = Vector2.MoveTowards(transform.position, rightPoint, moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, leftPoint, moveSpeed*Time.deltaTime);
        }

        CheckDirectionReached();
    }

    public void CheckDirectionReached()
    {
        float leftCheck = leftPoint.x - transform.position.x;
        float rightCheck = rightPoint.x - transform.position.x;
        if (leftCheck >= 0)
        {
            movingRight = true;
        }
        else if (rightCheck <= 0)
        {
            movingRight = false;
        }

    }
}
