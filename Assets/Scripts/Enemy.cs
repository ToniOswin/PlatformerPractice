using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    AudioClip deathSound;

    public virtual void Die(AudioSource audioS)
    {
        audioS.PlayOneShot(deathSound);
        Destroy(gameObject);
    } 
}
