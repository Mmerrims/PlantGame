/*****************************************************************************
// File Name :         Enemy Shooting.cs
// Author :            Yael Martoral
// Creation Date :     April 5th, 2025
//
// Brief Description : It calculates the distance between the enemy and the
                       player and shoots a bullet at their direction
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    [SerializeField] private float Health = 5f;


    private float timer;
    [SerializeField] private float shootDistance = 6;
    public bool IsTargetInRange { get; private set; } = false;
    public GameObject Player { get; private set; } = null;
    // It finds any objects with the "Player" tag
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // It sets a range around the enemy that when the player enters it, it shoots bullets within a certain time limit of eachother
    void Update()
    {

        float distance = Vector2.Distance(transform.position, Player.transform.position);
        IsTargetInRange = distance < shootDistance;
        //Debug.Log(distance);

        if(IsTargetInRange)
        {
            timer += Time.deltaTime;

            if (timer > 2)
            {
                timer = 0;
                shoot();
            }
        }

        if (Health <= 0)
        {
            Destroy(gameObject);
        }

    }

    // Once an objects collides with the enenmy, it reduces it's health
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Health -= 1;
        }
    }

    //It finds the distance of the player and shoots at their position
    void shoot()
    {
        GameObject bulletObj = Instantiate(bullet, bulletPos.position, Quaternion.identity);
        EnemyBulletScript bulletScript = bulletObj.GetComponent<EnemyBulletScript>();
        bulletScript.shoot(Player);
    }
}
