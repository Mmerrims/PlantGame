/*****************************************************************************
// File Name :         Audio Manager.cs
// Author :            Yael Martoral
// Creation Date :     April 30th, 2025
//
// Brief Description : It controls the audio in the game
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip enemyWalkSFX;
    public AudioClip goopHitSFX;
    public AudioClip startGameSFX;
    public AudioClip turretDeathSFX;

    //Sound effects for the gun shooting
    public void Goop()
    {
        Debug.Log("Goooooooo");
        audioSource.PlayOneShot(goopHitSFX);
    }

    //Sound effect for the turret being destroyed
    public void Turret()
    {
        audioSource.PlayOneShot(turretDeathSFX);
    }

    //Sound effect when you start the game
    public void GameStart()
    {
        audioSource.PlayOneShot(startGameSFX);
    }

}
