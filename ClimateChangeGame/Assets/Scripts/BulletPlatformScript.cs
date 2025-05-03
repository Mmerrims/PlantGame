/*****************************************************************************
// File Name :         BulletPlatformScript.cs
// Author :            Amber C. Cardamone
// Creation Date :     April 29th, 2025
//
// Brief Description : When a large bullet collides with a ground object, it will spawn a platform
*****************************************************************************/

using UnityEngine;

public class BulletPlatformScript : MonoBehaviour
{
    [SerializeField] private GameObject _spawnedObject;
    [SerializeField] private GameObject _parentBullet;
    private bool stop;

    /// <summary>
    /// Makes it so the bullet can spawn an object.
    /// </summary>
    private void Awake()
    {
        stop = false;
    }

    /// <summary>
    /// Spawns an object after hitting a gameobject, cannot repeat multiple times do to stop bool.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Oil") || collision.gameObject.CompareTag("GrassGround") || collision.gameObject.CompareTag("GrassWall") || collision.gameObject.layer == 11)
        {
            if (!stop)
            {
                stop = true;
                GameObject SpawnPlatform = Instantiate(_spawnedObject, this.transform.position, this.transform.rotation);
                Destroy(_parentBullet);
            }
        }
    }
}
