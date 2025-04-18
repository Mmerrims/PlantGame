using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private bool _bigBullet;

    private void Start()
    {
        if (_bigBullet)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "BlockRemove" || collision.gameObject.tag == "Oil" || collision.gameObject.tag == "GrassGround" || collision.gameObject.tag == "GrassWall" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
