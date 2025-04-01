using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private GameObject _player;
    [SerializeField] private Rigidbody2D _playerRigidbody;
    [SerializeField] private Transform _thisTransform;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "BlockRemove" || collision.gameObject.tag == "Oil" || collision.gameObject.tag == "GrassGround")
        {
            if (_player != null)
            {
                _player.transform.position = _thisTransform.transform.position;
                _playerRigidbody.velocity = new Vector2(0, 0);
            }
            Destroy(gameObject);
        }
    }
}
