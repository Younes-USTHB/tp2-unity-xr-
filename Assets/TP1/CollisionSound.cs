using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class CollisionSound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
    private void OnCollisionEnter(Collision other)
    {
        
        //if (this.GetComponent<Rigidbody>().linearVelocity.magnitude > 0.7f)
            _audioSource.PlayOneShot(_audioClip);
    }
}
