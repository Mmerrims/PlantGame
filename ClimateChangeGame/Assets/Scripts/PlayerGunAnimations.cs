using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGunAnimations : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private Transform _transform;
    

    public void StartShoot()
    {
        _anim.Play("WurmGunShootStartNormal");
    }

    public void EndShoot()
    {
        _anim.Play("WurmGunShootEndNormal");
    }

    private void FixedUpdate()
    {
        gameObject.transform.rotation = _transform.rotation;
    }
}
