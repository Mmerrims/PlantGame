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

    public void Goop()
    {
        Debug.Log("Goooooooo");
        audioSource.PlayOneShot(goopHitSFX);
    }

    public void Turret()
    {
        audioSource.PlayOneShot(turretDeathSFX);
    }

    public void GameStart()
    {
        audioSource.PlayOneShot(startGameSFX);
    }

}
