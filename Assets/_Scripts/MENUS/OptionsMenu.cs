using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider masterVolume, ambienceVolume, sfxVolume;
    public Button muteMaster, muteAmbience, muteSFX, cheatEnable;
    public TextMeshProUGUI masterVolText, ambienceVolText, sfxVolText;
    [Header("Sprite References")]
    public Sprite muteOff;
    public Sprite muteOn, boolFalse, boolTrue;

    private void OnEnable()
    {
        masterVolume.value = Options.GetMasterMute() ? 0 : Options.GetMasterVolume();
        ambienceVolume.value = Options.GetAmbienceMute() ? 0 : Options.GetAmbienceVolumeRaw();
        sfxVolume.value = Options.GetSFXMute() ? 0 : Options.GetSFXVolumeRaw();

        UpdateMuteButton(muteMaster, masterVolume);
        UpdateMuteButton(muteAmbience, ambienceVolume);
        UpdateMuteButton(muteSFX, sfxVolume);
    }

    public void EnableCheat()
    {
        Options.SetAutoCompleteEmail();
        cheatEnable.image.sprite = Options.GetAutoCompleteEmail() ? boolTrue : boolFalse;
    }

    public void UpdateMasterVolume() {
        if (masterVolume.value < 0.01f) MuteMasterVolume();
        else { Options.MasterUnmute(); Options.SetMasterVol(masterVolume.value); UpdateText(masterVolText, FormatVolume(masterVolume)); UpdateMuteButton(muteMaster, masterVolume); }
    }
    public void UpdateAmbienceVolume() {
        if (ambienceVolume.value < 0.01f) MuteAmbienceVolume();
        else { Options.AmbienceUnmute(); Options.SetAmbienceVol(ambienceVolume.value); UpdateText(ambienceVolText, FormatVolume(ambienceVolume)); UpdateMuteButton(muteAmbience, ambienceVolume); } 
    } 
    public void UpdateSFXVolume() {
        if (sfxVolume.value < 0.01f) MuteSFXVolume();
        else { Options.SFXUnmute(); Options.SetSFXVol(sfxVolume.value); UpdateText(sfxVolText, FormatVolume(sfxVolume)); UpdateMuteButton(muteSFX, sfxVolume); } 
    }

    void SliderToZero(Slider slider) => slider.value = 0;
    void SliderToVolume(Slider slider, float volume) => slider.value = volume;
    void UpdateText(TextMeshProUGUI text, string val) => text.text = val;
    string FormatVolume(Slider slider) => Mathf.RoundToInt(slider.value * 100).ToString();
    string FormatVolume(float value) => Mathf.RoundToInt(value * 100).ToString();
    void UpdateMuteButton(Button button, Slider slider) => button.image.sprite = slider.value > 0 ? muteOff : muteOn;

    public void MuteMasterVolume() { Options.MasterMute(); SliderToZero(masterVolume); UpdateText(masterVolText, 0.ToString()); UpdateMuteButton(muteMaster, masterVolume); }
    public void MuteAmbienceVolume() { Options.AmbienceMute(); SliderToZero(ambienceVolume); UpdateText(ambienceVolText, 0.ToString()); UpdateMuteButton(muteAmbience, ambienceVolume); }
    public void MuteSFXVolume() { Options.SFXMute(); SliderToZero(sfxVolume); UpdateText(sfxVolText, 0.ToString()); UpdateMuteButton(muteSFX, sfxVolume); }
    
    public void UpdateMasterMute()
    {
        if (Options.GetMasterMute())
        {
            float volume = Options.GetMasterVolumeRaw();
            Options.MasterUnmute(); UpdateText(masterVolText, FormatVolume(volume)); SliderToVolume(masterVolume, volume);
            UpdateMuteButton(muteMaster, masterVolume);
        }
        else
            MuteMasterVolume();
    }
    public void UpdateAmbienceMute()
    {
        if (Options.GetAmbienceMute())
        {
            float volume = Options.GetAmbienceVolumeRaw();
            Options.AmbienceUnmute(); UpdateText(ambienceVolText, FormatVolume(volume)); SliderToVolume(ambienceVolume, volume);
            UpdateMuteButton(muteAmbience, ambienceVolume);
        }
        else
            MuteAmbienceVolume();
    }
    public void UpdateSFXMute()
    {
        if (Options.GetSFXMute())
        {
            float volume = Options.GetSFXVolumeRaw();
            Options.SFXUnmute(); UpdateText(sfxVolText, FormatVolume(volume)); SliderToVolume(sfxVolume, volume);
            UpdateMuteButton(muteSFX, sfxVolume);
        }
        else
            MuteSFXVolume();
    }
}
