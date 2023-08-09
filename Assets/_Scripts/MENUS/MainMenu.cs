using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start() => SceneLoader.instance.FadeFromBlack();

    public void StartGame()
    {
        ResetGame();
        SceneLoader.instance.TransitionToContractScene();
        SceneLoader.instance.FadeToBlack();
    }

    public void QuitGame() => Application.Quit();
    public void ShowOptions() => MenuInterfacer.instance.MainMenu_TurnMenuOn();

    void ResetGame()
    {
        GameManager.ClearInstance();
        EmailMatrix.ClearInstance();
    }
}
