/*****************************************************************************
// File Name :         Enemy Bullet Script.cs
// Author :            Yael Martoral
// Creation Date :     April 5th, 2025
//
// Brief Description : Controls the bullet and how it behaves when it launches
                       what happens when it collides with the player
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    public float force;
    private float timer;
    [SerializeField] private float despawnTime = 6;
    [SerializeField] private float yoffset = 0;
    // It gets the it's own rigid bodies in the beggining
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // It calculates the trajectory between the player and itself and goes towards the player
    public void shoot(GameObject player)
    {
        if(rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        this.player = player;

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y + yoffset).normalized * force;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    // After a bit of time when the bullet does not make contact with anything, it deletes itself
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > despawnTime)
        {
            Destroy(gameObject);
        }
    }

    // Once the bullet enters a trigger, it destroys itself
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
