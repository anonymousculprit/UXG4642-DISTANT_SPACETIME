using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public delegate void Announcement();

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public event Announcement InitComplete;
    public static void ClearInstance() => instance = null;
    private void Awake() => ManageInstance();
    public static EmailDataManager emailDataManager = new();
    public static EmailMatrixManager emailMatrixManager = new();

    [Header("Email Settings")]
    public string emailDataFolder;
    public string emailMatrixFolder;
    public bool keepPreviousDayEmails = true;
    public bool canReplyToPreviousDayEmails = false;
    public bool DEBUG_listAllEmails = false;

    [Header("Set Custom Day")]
    public bool customDayInput = false;
    public int customDay = 1;
    int day = 1;

    private void Start()
    {
        if (customDayInput) day = customDay;
        SceneLoadResponsibilities(SceneManager.GetActiveScene(), LoadSceneMode.Single);
        SceneManager.sceneLoaded += SceneLoadResponsibilities;
    }

    void SceneLoadResponsibilities(Scene scene, LoadSceneMode mode)
    {
        EmailGrabber.instance.Init(day);
        InitComplete?.Invoke();
    }

    public void JumpToDay(int newDay) => day = newDay;
    public void IncrementDay() => day++;
    public void StartDay() => SceneLoader.instance.FadeFromBlack();
    public int GetDay() => day;
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
            DontDestroyOnLoad(this);
            if (!string.IsNullOrEmpty(emailDataFolder)) emailDataManager.Init(dataFolder: emailDataFolder); else emailDataManager.Init();
            if (!string.IsNullOrEmpty(emailMatrixFolder)) emailMatrixManager.Init(dataFolder: emailDataFolder); else emailMatrixManager.Init();
        }
        if (instance != this) Destroy(gameObject);
    }
}


