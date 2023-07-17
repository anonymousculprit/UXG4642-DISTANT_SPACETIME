using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInterfacer : MonoBehaviour
{
    public static MenuInterfacer instance;

    public GameObject menuGO, pauseMenu, optionsMenu, dayMenu;
    public GameObject optionsMenuClose;

    private void Awake() => instance = this;

    public void ClearMenus()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
        dayMenu.SetActive(false);
    }

    void SwapToPauseMenu() { ClearMenus(); pauseMenu.SetActive(true); }
    public void SwapToOptionsMenu() { ClearMenus(); optionsMenu.SetActive(true); }
    public void SwapToDayMenu() { ClearMenus(); dayMenu.SetActive(true); }
    public void BackToMainMenu() { TurnMenuOff(); SceneLoader.instance.TransitionToMainMenu(); }
    public void QuitGame() => Application.Quit();

    public void TurnMenuOn()
    {
        menuGO.SetActive(true);
        SwapToPauseMenu();
        Time.timeScale = 0;
    }

    public void TurnMenuOff()
    {
        menuGO.SetActive(false);
        ClearMenus();
        Time.timeScale = 1;
    }

    public void MainMenu_TurnMenuOn()
    {
        menuGO.SetActive(true);
        SwapToOptionsMenu();
        optionsMenuClose.SetActive(false);
        Time.timeScale = 0;
    }

    public void BackToPauseMenu() => SwapToPauseMenu();
}
