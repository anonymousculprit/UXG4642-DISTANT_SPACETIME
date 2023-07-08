using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Options
{
    public static event Announcement UpdateSFX, UpdateAmbience, UpdateAllAudio;
    static float masterVol = 1;
    static float ambienceVol = 1;
    static float sfxVol = 1;
    static bool masterMuted, ambienceMuted, sfxMuted;
    static bool autoCompleteEmail;

    public static float GetMasterVolume() => masterMuted ? 0 : masterVol;
    public static float GetAmbienceVolume() => masterMuted || ambienceMuted ? 0 : ambienceVol * masterVol;
    public static float GetSFXVolume() => masterMuted || sfxMuted ? 0 : sfxVol * masterVol;

    public static float GetMasterVolumeRaw() => masterVol;
    public static float GetAmbienceVolumeRaw() => ambienceVol;
    public static float GetSFXVolumeRaw() => sfxVol;

    public static bool GetMasterMute() => masterMuted;
    public static bool GetAmbienceMute() => ambienceMuted;
    public static bool GetSFXMute() => sfxMuted;

    public static void SetMasterVol(float vol) { masterVol = vol; UpdateAllAudio?.Invoke(); }
    public static void SetAmbienceVol(float vol) { ambienceVol = vol; UpdateAmbience?.Invoke(); }
    public static void SetSFXVol(float vol) { sfxVol = vol; UpdateSFX?.Invoke(); }

    public static void MasterMute() { masterMuted = true; UpdateAllAudio?.Invoke(); }
    public static void AmbienceMute() { ambienceMuted = true; UpdateAmbience?.Invoke(); }
    public static void SFXMute() { sfxMuted = true; UpdateSFX?.Invoke(); }

    public static void MasterUnmute() { masterMuted = false; UpdateAllAudio?.Invoke(); }
    public static void AmbienceUnmute() { ambienceMuted = false; UpdateAmbience?.Invoke(); }
    public static void SFXUnmute() { sfxMuted = false; UpdateSFX?.Invoke(); }

    public static void SetAutoCompleteEmail() => autoCompleteEmail = !autoCompleteEmail;
    public static bool GetAutoCompleteEmail() => autoCompleteEmail;
}
