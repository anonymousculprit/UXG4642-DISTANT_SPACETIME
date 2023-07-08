using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class VolumeManager : MonoBehaviour
{
    public static VolumeManager instance;

    public GameObject sfxGO, ambienceGO;

    List<AudioSource> sfxList = new();
    List<AudioSource> ambienceList = new();

    private void Awake() => instance = this;

    public void Start()
    {
        PopulateAudioLists();
        UpdateAllVolume();
    }

    private void OnEnable()
    {
        Options.UpdateAllAudio += UpdateAllVolume;
        Options.UpdateAmbience += UpdateAmbienceVolume;
        Options.UpdateSFX += UpdateSFXVolume;
    }

    private void OnDisable()
    {
        Options.UpdateAllAudio -= UpdateAllVolume;
        Options.UpdateAmbience -= UpdateAmbienceVolume;
        Options.UpdateSFX -= UpdateSFXVolume;
    }

    public void UpdateSFXVolume()
    {
        if (sfxList.Count > 0)
            foreach (AudioSource sfx in sfxList)
                sfx.volume = Options.GetSFXVolume();
    }

    public void UpdateAmbienceVolume()
    {
        if (ambienceList.Count > 0)
            foreach (AudioSource ambience in ambienceList)
                ambience.volume = Options.GetAmbienceVolume();
    }

    void PopulateAudioLists()
    {
        AudioSource[] sfx = sfxGO.GetComponents<AudioSource>();
        if (sfx != null && sfx.Length > 0) sfxList.AddRange(sfx);

        AudioSource[] ambience = ambienceGO.GetComponents<AudioSource>();
        if (ambience != null && ambience.Length > 0) ambienceList.AddRange(ambience);
    }

    public void UpdateAllVolume() { UpdateSFXVolume(); UpdateAmbienceVolume(); }
}
