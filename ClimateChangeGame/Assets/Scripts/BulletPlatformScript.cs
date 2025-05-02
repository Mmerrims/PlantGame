using UnityEngine;

public class BulletPlatformScript : MonoBehaviour
{
    [SerializeField] private GameObject _spawnedObject;
    [SerializeField] private GameObject _parentBullet;
    private bool stop;

    private void Awake()
    {
        stop = false;
    }

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
