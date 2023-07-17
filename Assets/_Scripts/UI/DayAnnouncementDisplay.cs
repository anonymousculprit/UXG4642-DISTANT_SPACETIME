using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayAnnouncementDisplay : MonoBehaviour
{
    public Animator animator;
    public TextMeshProUGUI dayAnnouncementText;

    private void OnEnable()
    {
        GameManager.instance.InitComplete += StartDay;
    }

    private void OnDisable()
    {
        GameManager.instance.InitComplete -= StartDay;
    }

    public void StartDay()
    {
        SFXManager.instance.Play("logon_newday");
        dayAnnouncementText.text = "DAY " + GameManager.instance.GetDay();
        animator.SetTrigger("Play");
    }

    public void ANIM_ResetTrigger() => animator.ResetTrigger("Play");
    public void ANIM_CallStartDay() => SceneLoader.instance.FadeFromBlack();
}
