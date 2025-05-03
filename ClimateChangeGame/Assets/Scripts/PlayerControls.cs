/*****************************************************************************
// File Name :         Player Controls.cs
// Author :            Amber C. Cardamone
// Creation Date :     April 29th, 2025
//
// Brief Description : Controls the player's movement, where they spawn, controls restart/quit, and checks if grounded or not.
*****************************************************************************/

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private float _horizontal;
    [SerializeField] private float _speed;
    [SerializeField] private float _baseSpeed;
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _baseJumpPower;
    [SerializeField] private float _foundJumpVelocity;
    [SerializeField] private bool _grounded;
    [SerializeField] private bool _canCancelJump;
    [SerializeField] private bool _cancelledJump;
    [SerializeField] private PlayerAnimations _pAnims;
    [SerializeField] private CheckpointManager _checkpointManager;
    private Rigidbody2D _rigidbody;

    /// <summary>
    /// Checks the spawn position, and sets up rigidbody detection and normalized movement values
    /// </summary>
    private void Awake()
    {
        _foundJumpVelocity = 10;
        _checkpointManager = FindObjectOfType<CheckpointManager>();
        transform.position = _checkpointManager.LastCheckPointPos;
        _baseSpeed = _speed;
        _baseJumpPower = _jumpPower;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Gives the player the ability to move, checks their direction and flips accordingly, and locks player upwards momentum,
    /// causing them to not gain additional height off slopes. Also allows for dynamic jump height.
    /// </summary>
    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_horizontal * _speed, _rigidbody.velocity.y);

        if (_horizontal == 1)
        {
            gameObject.transform.localScale = new Vector2(1, 1);
        } 
        else if (_horizontal == -1)
        {
            gameObject.transform.localScale = new Vector2(-1, 1);
        }

        if (_grounded)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            _foundJumpVelocity = 10;
        }
        else if (!_grounded)
        {
            if (_rigidbody.velocity.y > _foundJumpVelocity)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _foundJumpVelocity);
            }

            if (_rigidbody.velocity.y < _foundJumpVelocity)
            {
                _foundJumpVelocity = _rigidbody.velocity.y;
            }
        }

        if (_rigidbody.velocity.y < 0)
        {
            _rigidbody.gravityScale = 4;
            _pAnims.StartFall();
        }
        else
        {
            _rigidbody.gravityScale = 1.5f;
        }

        if (_canCancelJump && _cancelledJump)
        {
            if (_rigidbody.velocity.y > -1)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
                _cancelledJump = false;
                _canCancelJump = false;
            }
        }
    }

    /// <summary>
    /// Grabs the player's input actions, allowing the player to move.
    /// </summary>
    /// <param name="context"></param>
    public void Move(InputAction.CallbackContext context)
    {
        _horizontal = context.ReadValue<Vector2>().x;
    }

    /// <summary>
    /// Checks if the player has pressed/stopped pressing the input button, if they press it and are launched, they 
    /// will launch upwards, otherwise their vertical velocity will be stopped.
    /// </summary>
    /// <param name="context"></param>
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (_grounded)
            {
                _canCancelJump = false;
                _grounded = false;
                _pAnims.StartJumpAnim();
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpPower + _rigidbody.velocity.y);
                Invoke("CanCancelJump", 0.2f);
            }
        } 
        else if (context.canceled && _grounded == false && _rigidbody.velocity.y > 0)
        {
            _cancelledJump = true;
        }
    }

    public void CanCancelJump()
    {
        _canCancelJump = true;
    }

    /// <summary>
    /// Checks if the player is currently grounded, if they are grounded on oil with the oil layer, they will be slowed.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 14 || collision.gameObject.layer == 15)
        {
            _pAnims.SetGrounded(true);
            _grounded = true;
        }

        if (collision.gameObject.layer == 15 || collision.gameObject.layer == 11)
        {
            _speed = 2;
            _jumpPower = 3;
        }
    }

    /// <summary>
    /// If the player leaves the ground, they will gain a short time to continue jumping (coyotetime), and they will regain their speed if they leave oil.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 14 || collision.gameObject.layer == 15)
        {
            Invoke("CoyoteTime", .08f);
        }

        if (collision.gameObject.layer == 15 || collision.gameObject.layer == 11)
        {
            _speed = _baseSpeed;
            _jumpPower = _baseJumpPower;
        }
    }

    /// <summary>
    /// Once coyotetime is done, the player stops being counted as being on ground.
    /// </summary>
    public void CoyoteTime()
    {
        _pAnims.SetGrounded(false);
        _grounded = false;
    }

    /// <summary>
    /// Restarts the scene
    /// </summary>
    /// <param name="context"></param>
    public void Restart(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Quits the Game.
    /// </summary>
    /// <param name="context"></param>
    public void Quit(InputAction.CallbackContext context)
    {
        Application.Quit();
        print("Quit");
    }

    /// <summary>
    /// Stops the player from moving, used in the end sequence
    /// </summary>
    public void disableMovement()
    {
        _speed = 0;
        _jumpPower = 0;
    }
}
