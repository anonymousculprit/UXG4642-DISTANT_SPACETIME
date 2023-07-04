using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public delegate void Announcement();

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public event Announcement InitComplete;
    public static void ClearInstance() => instance = null;
    private void Awake() => ManageInstance();
    public EmailDataManager emailDataManager = new();
    public EmailMatrixManager emailMatrixManager = new();

    public string emailDataFolder, emailMatrixFolder;
    int day = 1;

    private void Start()
    {
        EmailGrabber.instance.Init(day);
        InitComplete?.Invoke();
        StartDay();
    }

    public void JumpToDay(int newDay) => day = newDay;
    public void IncrementDay() => day++;
    public void StartDay() => SceneLoader.instance.FadeFromBlack();
    public void EndDay()
    {
        IncrementDay();
        SceneLoader.instance.TransitionToGameScene();
        SceneLoader.instance.FadeToBlack();
    }

    public void ManageInstance()
    {
        if (instance == null)
        {
            instance = this;

            if (!string.IsNullOrEmpty(emailDataFolder)) emailDataManager.Init(dataFolder: emailDataFolder); else emailDataManager.Init();
            if (!string.IsNullOrEmpty(emailMatrixFolder)) emailMatrixManager.Init(dataFolder: emailDataFolder); else emailMatrixManager.Init();
        }
        else
        {
            day = instance.day;
            emailDataManager = instance.emailDataManager;
            emailMatrixManager = instance.emailMatrixManager;
            instance = this;
        }
    }
}


