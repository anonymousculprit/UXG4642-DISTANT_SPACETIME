using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmailSentDisplay : MonoBehaviour
{
    public static EmailSentDisplay instance;

    private void Awake()
    {
        instance = this;
    }

    Animator anim;

    private void Start() => anim = gameObject.GetComponent<Animator>();

    public void ShowPopup() { anim.SetTrigger("Play"); Debug.Log("Playing Popup Now"); }
    public void ANIM_ResetTrigger() => anim.ResetTrigger("Play");
}
