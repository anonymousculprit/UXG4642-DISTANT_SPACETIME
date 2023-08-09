using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    [SerializeField]public List<SFXEntry> allSFX = new();
    public float rangeValueForTypingNoises = 0.25f;

    private void Awake()
    {
        instance = this;
        RegisterAllSFX();
    }

    public void RegisterAllSFX()
    {
        for (int i = 0; i < allSFX.Count; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = allSFX[i].clip;
            source.playOnAwake = false;
            allSFX[i].RegisterSource(source);
        }
    }

    public AudioSource GetSFX(string clipName) => allSFX.Find(x => x.clipName == clipName).source;
    public AudioClip GetSFXClip(string clipName) => allSFX.Find(x => x.clipName == clipName).clip;
    public void Play(string clipName) => allSFX.Find(x => x.clipName == clipName).Play();
    public void Play(string clipName, float pitchValue)
    {
        AudioSource sfx = GetSFX(clipName);
        sfx.pitch = pitchValue;
        Play(clipName);
    }

    private void Update() { if (Input.GetMouseButtonDown(0)) Play("mouseclick"); }
    public void PlayTypingNoise() => Play("keyboardtype", 1 + UnityEngine.Random.Range(-rangeValueForTypingNoises, rangeValueForTypingNoises));
    public void PlayEndDaySFX() => StartCoroutine(WaitingForSFXComplete("logoff_endday"));
    public void PlayStartDaySFX() => StartCoroutine(WaitingForSFXComplete("logon_newday"));

    IEnumerator WaitingForSFXComplete(string clipName)
    {
        SceneLoader.instance.WaitForSFX();
        AudioClip clip = GetSFXClip(clipName);
        Play(clipName);
        yield return new WaitForSeconds(clip.length);
        SceneLoader.instance.SFX_TransitionToNextScene();
    }
}

[Serializable]
public class SFXEntry
{
    public AudioClip clip;
    public string clipName;
    [HideInInspector] public AudioSource source;

    public void RegisterSource(AudioSource source) => this.source = source;
    public void Play() => source.PlayOneShot(clip);
}
