using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _oilDamage;
    [SerializeField] private float _groundHealing;
    [SerializeField] private float _grassHealing;
    private PlayerMovement _playerMovement;
    private bool grass;
    private bool oil;

    private void Start()
    {
        _health = _maxHealth;
        _playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            grass = false;
            oil = false;
        } 
        else if (collision.gameObject.layer == 11)
        {
            grass = false;
            oil = true;
        }
        else if (collision.gameObject.layer == 12)
        {
            grass = true;
            oil = false;
        }
    }

    private void Update()
    {
        if (grass)
        {
            _health += _grassHealing * Time.deltaTime;
        }
        else if (oil)
        {
            _health -= _oilDamage * Time.deltaTime;
        }
        else
        {
            _health += _groundHealing * Time.deltaTime;
        }

        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        } 
        else if (_health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
