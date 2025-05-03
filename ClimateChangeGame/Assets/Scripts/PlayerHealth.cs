/*****************************************************************************
// File Name :         PlayerHealth.cs
// Author :            Amber C. Cardamone
// Creation Date :     April 29th, 2025
//
// Brief Description : Checks the player's health, connects with a healthbar, and restarts the level if health goes to 0.
*****************************************************************************/

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _oilDamage;
    [SerializeField] private float _groundHealing;
    [SerializeField] private float _grassHealing;
    [SerializeField] private bool grass;
    [SerializeField] private bool oil;
    [SerializeField] private bool grounded;
    public Image healthBar;

    /// <summary>
    /// Sets up the health system to be resettable.
    /// </summary>
    private void Start()
    {
        _health = _maxHealth;
    }

    /// <summary>
    /// OnCollisionEnter Checks what the player is colliding with, and gives/loses health depending on what they hit.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 13)
        {
            _health = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (collision.gameObject.layer == 7)
        {
            _health -= 3;
        }

        if (collision.gameObject.layer == 8)
        {
            _health -= 3;
        }
    }

    /// <summary>
    /// Used for continuous hitting effects, checking if the player is hitting oil or grass.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
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
    }

    /// <summary>
    /// Checks if the player leaves tiles
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 10 || collision.gameObject.layer == 11 || collision.gameObject.layer == 12 || collision.gameObject.layer == 14)
        {
            grounded = false;
        }

        if (collision.gameObject.layer == 11)
        {
            oil = false;
        }

        if (collision.gameObject.layer == 12)
        {
            grass = false;
        }
    }

    /// <summary>
    /// Checks if the player leaves tiles but a trigger this time.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10 || collision.gameObject.layer == 11 || collision.gameObject.layer == 12 || collision.gameObject.layer == 14)
        {
            grounded = false;
        }

        if (collision.gameObject.layer == 11)
        {
            oil = false;
        }

        if (collision.gameObject.layer == 12)
        {
            grass = false;
        }
    }

    /// <summary>
    /// Makes the player consistently lose/gain health, and checks if they have no health, reloads scene.
    /// </summary>
    private void Update()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = _health / _maxHealth;
        }
        

        if (grounded)
        {
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
        }
    }
}
