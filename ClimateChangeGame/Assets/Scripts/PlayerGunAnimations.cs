using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGunAnimations : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    //[SerializeField] private Transform _transform1;
    //[SerializeField] private Transform _transform2;
    

    public void StartShoot()
    {
        _anim.Play("WurmGunShootStartNormal");
    }

    public void EndShoot()
    {
        _anim.Play("WurmGunShootEndNormal");
    }

    //private void FixedUpdate()
    //{
    //    _transform1.rotation = _transform2.rotation;
    //}
}
