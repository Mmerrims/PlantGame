/*****************************************************************************
// File Name :         Enemy Script.cs
// Author :            Yael Martoral
// Creation Date :     April 5th, 2025
//
// Brief Description : It controls the enemy's behavior and makes them patrol 
                       shoot at the player
*****************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private List<Transform> points;

    [Header("Settings")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float smoothingSpeed;
    [SerializeField] private float stoppingDistance;
    [SerializeField] private float stoppingTime;
    [SerializeField] private float shootingMovementSpeed;
    [SerializeField] private float shootMoveRadius;

    [Tooltip("Private Variables")]
    private Vector2 movementDirection;
    private int pointIndex;
    private float timeStamp;
    private bool timeStampOnce;

    private EnemyShooting shootingScript = null;

    [SerializeField] private AudioSource EnemyWalk;

    private AudioSource audioSource;

    private void Awake()
    {
        shootingScript = GetComponent<EnemyShooting>();
        audioSource = GetComponent<AudioSource>();
    }

    //It calls the inicial movement direction, stores it and then starts the starting variables
    private void Start()
    {
        movementDirection = GetMovementDirection(points[0].position);

        pointIndex = 0;
        timeStamp = 0.0f;
        timeStampOnce = true;
    }

    //It calculates the distance between the two distances and determines what to do then, it also uses the NextMovementDirectionHandler
    private void FixedUpdate()
    {
        if (shootingScript != null && shootingScript.IsTargetInRange)
        {
            shootMove();
        }
        else
        {
            Move();
        }


    }

    private void shootMove()
    {
        float distance = Vector2.Distance(transform.position, shootingScript.Player.transform.position);
        if (distance < shootMoveRadius)
        {
            float diff = transform.position.x - shootingScript.Player.transform.position.x;
            transform.Translate(Vector3.right * diff * shootingMovementSpeed * (1 - Mathf.Exp(-smoothingSpeed * Time.fixedDeltaTime)));
        }
        


    }

    private void Move()
    {
        transform.Translate(movementDirection.normalized * movementSpeed * (1 - Mathf.Exp(-smoothingSpeed * Time.fixedDeltaTime)));
        

        NextMovementDirectionHandler();
    }


    //If the moving enemy is lesser than the stopping distance, it makes the platform stop and move to the other direction
    private void NextMovementDirectionHandler()
    {
        if (GetMovementDirection(points[pointIndex].position).magnitude < stoppingDistance)
        {
            movementDirection = Vector2.zero;

            if (timeStampOnce)
            {
                timeStamp = Time.time;
                timeStampOnce = false;
                OnMoveStart();
            }

            if((Time.time - timeStamp) > stoppingTime)
            {
                if (pointIndex == points.Count - 1)
                    pointIndex = 0;
                else
                    ++pointIndex;

                movementDirection = GetMovementDirection(points[pointIndex].position);
                timeStamp = 0.0f;
                timeStampOnce = true;
                OnMoveStop();
            }
        }
    }

    private void OnMoveStop()
    {
        EnemyWalk.Stop();
    }

    private void OnMoveStart()
    {
        EnemyWalk.Play();
    }

    //It makes the two points visable
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        foreach (Transform point in points)
            Gizmos.DrawSphere(point.position, 0.2f);
    }

    //It finds the next movement point and changes its direction
    private Vector2 GetMovementDirection(Vector2 pointPos)
    {
        return pointPos - (Vector2)transform.position;
    }
}
