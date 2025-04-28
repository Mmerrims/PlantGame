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
    [SerializeField] private bool _grounded;
    [SerializeField] private bool _canCancelJump;
    [SerializeField] private bool _cancelledJump;
    [SerializeField] private PlayerAnimations _pAnims;
    [SerializeField] private CheckpointManager _checkpointManager;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _checkpointManager = FindObjectOfType<CheckpointManager>();
        transform.position = _checkpointManager.LastCheckPointPos;
        _baseSpeed = _speed;
        _baseJumpPower = _jumpPower;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

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

    public void Move(InputAction.CallbackContext context)
    {
        _horizontal = context.ReadValue<Vector2>().x;
    }

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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 14 || collision.gameObject.layer == 15)
        {
            _pAnims.SetGrounded(false);
            _grounded = false;
        }

        if (collision.gameObject.layer == 15 || collision.gameObject.layer == 11)
        {
            _speed = _baseSpeed;
            _jumpPower = _baseJumpPower;
        }
    }

    public void Restart(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit(InputAction.CallbackContext context)
    {
        Application.Quit();
        print("Quit");
    }
}
