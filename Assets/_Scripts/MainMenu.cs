using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.ClearInstance();
        SceneLoader.instance.TransitionToGameScene();
        SceneLoader.instance.FadeToBlack();
    }

    public void QuitGame() => Application.Quit();

    public void ShowOptions()
    {
        // unimplemented.
    }
}
