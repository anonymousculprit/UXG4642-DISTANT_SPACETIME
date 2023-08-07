using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    public event Announcement Loaded;
    int sceneToTransition;
    bool waitForSFX = false;

    Animator transition;

    private void Awake() => instance = this;

    private void OnEnable()
    {
        transition = gameObject.GetComponent<Animator>();
        transition.speed = 0;
    }

    public void WaitForSFX() => waitForSFX = true;
    public void TransitionToMainMenu() => SetSceneToTransitionTo(0);
    public void TransitionToGameScene() => SetSceneToTransitionTo(1);
    public void SetSceneToTransitionTo(int i) => sceneToTransition = i;
    public void FadeFromBlack() { transition.SetTrigger("FadeFromBlack"); transition.speed = 1; }
    public void FadeToBlack() { transition.SetTrigger("FadeToBlack"); transition.speed = 1; }

    public void ANIM_Loaded() { Loaded?.Invoke(); Debug.Log("loaded."); }
    public void ANIM_ResetTriggers()
    {
        transition.ResetTrigger("FadeFromBlack");
        transition.ResetTrigger("FadeToBlack");
    }
    public void ANIM_TransitionToNextScene() 
    {
        if (!waitForSFX) SceneManager.LoadScene(sceneToTransition);
        else transition.speed = 0;
    }
    public void SFX_TransitionToNextScene() { SceneManager.LoadScene(sceneToTransition); }
}