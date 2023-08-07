using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public delegate void Announcement();

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public event Announcement InitComplete;
    public static void ClearInstance() { instance = null; emailDataManager = new(); emailMatrixManager = new(); }
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
    int dayValidation = 0;
    bool mainStoryClearInPlaythrough = false;

    private void Start()
    {
        if (customDayInput) day = customDay;
        SceneLoadResponsibilities(SceneManager.GetActiveScene(), LoadSceneMode.Single);
        SceneManager.sceneLoaded += SceneLoadResponsibilities;
    }

    void SceneLoadResponsibilities(Scene scene, LoadSceneMode mode)
    {
        if (CurrentSceneIsMainMenu()) { KillSelf(); return; }
        InboxFilter.instance.Init(day);
        dayValidation = day;
        InitComplete?.Invoke();
        SceneLoader.instance.Loaded += CursorActive;
        CheckForMainStoryClear();
    }

    bool CurrentSceneIsMainMenu() => SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0);
    void KillSelf()
    {
        SceneManager.sceneLoaded -= SceneLoadResponsibilities; 
        ClearInstance(); 
        Destroy(gameObject); 
        Destroy(this);
    }

    void CheckForMainStoryClear() { if (day == 8) mainStoryClearInPlaythrough = true; }

    public void JumpToDay(int newDay) { day = newDay - 1; dayValidation = day; EndDay(); }
    public void IncrementDay() => day = dayValidation + 1;
    public int GetDay() => day;
    public bool FinishedMainStory() => mainStoryClearInPlaythrough;
    public void EndDay()
    {
        if (day == 7) CustomEmailsManager.instance.RegisterDayEmailsToMatrix();
        IncrementDay();
        SceneLoader.instance.TransitionToGameScene();
        SceneLoader.instance.FadeToBlack();
        SFXManager.instance.PlayEndDaySFX();
        CursorLock();
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

    public void CursorLock() { Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false; }
    public void CursorActive() { Cursor.lockState = CursorLockMode.None; Cursor.visible = true; SceneLoader.instance.Loaded -= CursorActive; }
}


