using TMPro;
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
    [SerializeField] private bool grass;
    [SerializeField] private bool oil;
    [SerializeField] private bool grounded;
    [SerializeField] private GameObject _healthTextObject;
    [SerializeField] private TMP_Text _healthText;

    private void Start()
    {
        _health = _maxHealth;
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _healthTextObject = GameObject.Find("HealthText");
        if (_healthTextObject != null)
        {
            _healthText = _healthTextObject.GetComponent<TMP_Text>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            grass = false;
            oil = false;
            grounded = true;
        } 
        else if (collision.gameObject.layer == 11)
        {
            grass = false;
            oil = true;
            grounded = true;
        }
        else if (collision.gameObject.layer == 12)
        {
            grass = true;
            oil = false;
            grounded = true;
        }

        if (collision.gameObject.layer == 13)
        {
            _health = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (collision.gameObject.layer == 7)
        {
            _health -= 1;
        }

        if (collision.gameObject.layer == 8)
        {
            _health -= 1;
        }
    }

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.layer == 10 || collision.gameObject.layer == 11 || collision.gameObject.layer == 12)
    //    {
    //        grounded = false;
    //    }
    //}

    private void Update()
    {
        _healthText.text = ("" + _health);

        //if (grounded)
        //{
            if (grass)
            {
                _health += _grassHealing * Time.deltaTime;
            }
            if (oil)
            {
                _health -= _oilDamage * Time.deltaTime;
            }
            if (!oil && !grass)
            {
                _health += _groundHealing * Time.deltaTime;
            }

            if (_health > _maxHealth)
            {
                _health = _maxHealth;
            }
            if (_health <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
       // }
    }
}
