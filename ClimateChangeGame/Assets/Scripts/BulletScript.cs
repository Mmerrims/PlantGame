using Unity.VisualScripting;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private bool _bigBullet;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioManager audioManager;

    private void Start()
    {
       var audioManagerObject = GameObject.Find("AudioManager");
        if (audioManagerObject != null)
        {
            audioManager = audioManagerObject.GetComponent<AudioManager>();
        }
        if (_bigBullet)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "BlockRemove" || collision.gameObject.tag == "Oil" || collision.gameObject.tag == "GrassGround" || collision.gameObject.tag == "GrassWall" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player" || collision.gameObject.layer == 11 || collision.gameObject.tag == "Untagged")
        {
            Debug.Log("Please Goo");
            audioManager.Goop();
            Destroy(gameObject);
        }
    }
}
