using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGunAnimations : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    

    public void StartShoot()
    {
        _anim.Play("WurmGunShootStartNormal");
    }

    public void EndShoot()
    {
        _anim.Play("WurmGunShootEndNormal");
    }

}
