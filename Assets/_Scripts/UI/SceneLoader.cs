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

    Animator transition;

    private void Awake() => instance = this;
    private void OnEnable() => transition = gameObject.GetComponent<Animator>();


    public void TransitionToMainMenu() => SetSceneToTransitionTo(0);
    public void TransitionToGameScene() => SetSceneToTransitionTo(1);
    public void SetSceneToTransitionTo(int i) => sceneToTransition = i;
    public void FadeFromBlack() => transition.SetTrigger("FadeFromBlack");
    public void FadeToBlack() => transition.SetTrigger("FadeToBlack");

    public void ANIM_Loaded() => Loaded?.Invoke();
    public void ANIM_TransitionToNextScene() => SceneManager.LoadScene(sceneToTransition);
    public void ANIM_ResetTriggers()
    {
        transition.ResetTrigger("FadeFromBlack");
        transition.ResetTrigger("FadeToBlack");
    }
}