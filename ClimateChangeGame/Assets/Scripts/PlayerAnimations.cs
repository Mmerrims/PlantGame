using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _playerHitboxRB2D;
    [SerializeField] private Animator _playerBodyAnims;
    [SerializeField] private Animator _playerGunAnims;
    [SerializeField] private PlayerMovement _pM;

    public void SetGrounded(bool x)
    {
        _playerBodyAnims.SetBool("isGrounded", x);
        if (x)
        {
            _playerBodyAnims.SetBool("isDescending", false);
            _playerBodyAnims.SetBool("isAscending", false);
        }
            
    }

    public void SetMoving(bool x)
    {
        _playerBodyAnims.SetBool("isMoving", x);
    }

    /// <summary>
    /// CALL THIS WHEN A PLAYER PRESSES THE JUMP INPUT
    /// </summary>
    public void StartJumpAnim()
    {
        _playerBodyAnims.Play("WurmJumpStart");
        Invoke("StartJump", 0.25f);
        SetGrounded(false);
    }

    /// <summary>
    /// CALL THIS WHEN A PLAYER BEGINS ASCENDING
    /// </summary>
    public void StartJump()
    {
        _playerBodyAnims.SetBool("isAscending", true);
        SetGrounded(false);
    }

    /// <summary>
    /// CALL THIS WHEN A PLAYER BEGINS DESCENDING
    /// </summary>
    public void StartFall()
    {
        _playerBodyAnims.SetBool("isDescending", true);
        _playerBodyAnims.SetBool("isAscending", false);
    }

    private void FixedUpdate()
    {
        if (_playerHitboxRB2D.velocity.x != 0)
        {
            SetMoving(true);
        }
        else
        {
            SetMoving(false);
        }

        if (_playerBodyAnims.GetBool("isAscending") && _playerHitboxRB2D.velocity.y < 0)
        {
            StartFall();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
