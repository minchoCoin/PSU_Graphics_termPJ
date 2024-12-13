using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_WeaponRifle: MonoBehaviour
{
    [Header("Audio Clips")]
    [SerializeField]
    private AudioClip AC_TakeOutWeapon;
    private AudioSource audioSC;


    private void PlaySound(AudioClip NewCips)
    {
        audioSC.Stop();
        audioSC.clip = NewCips;
        audioSC.Play();
    }

    void Awake()
    {
        audioSC = GetComponent<AudioSource>();
    }


    private void OnEnable()
    {
        PlaySound(AC_TakeOutWeapon);
    }
    void Update()
    {
        
    }
}
