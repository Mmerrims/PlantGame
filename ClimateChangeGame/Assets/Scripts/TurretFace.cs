/*****************************************************************************
// File Name :         TurretFace.cs
// Author :            Cam Drane
// Creation Date :     April 5th, 2025
//
// Brief Description : It makes the turret's sprite face the player
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFace : MonoBehaviour
{

    [SerializeField] private Transform _playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        _playerTransform = GameObject.Find("Player (1)").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 turretToPlayerVector = _playerTransform.position - transform.position;
        Vector2 targetDirection = turretToPlayerVector.normalized;

        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, targetDirection);
        transform.rotation = targetRotation;
    }
}
