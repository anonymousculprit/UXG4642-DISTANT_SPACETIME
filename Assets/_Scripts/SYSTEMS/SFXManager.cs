using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    public AudioSource clicking, typing;
    public float rangeValueForTypingNoises = 0.25f;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) PlayClickingNoise();
    }

    public void PlayClickingNoise()
    {
        clicking.Play();
    }

    public void PlayTypingNoise()
    {
        typing.pitch = 1 + Random.Range(-rangeValueForTypingNoises, rangeValueForTypingNoises);
        typing.Play();
    }

}
