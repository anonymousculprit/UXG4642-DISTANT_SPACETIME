using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GameIntroAnimation: MonoBehaviour
{
    public Animator gameIntroHeaders, gameIntroButtons;

    private void Start()
    {
        SceneLoader.instance.Loaded += PlayGameIntro;
        gameIntroHeaders.speed = 0;
        gameIntroButtons.speed = 0;
    }

    private void OnDisable() => SceneLoader.instance.Loaded -= PlayGameIntro;

    public void PlayGameIntro() => gameIntroHeaders.speed = 1;
    public void ANIM_ShowButtons() => gameIntroButtons.speed = 1;
}
