using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnreadEmailDisplay : MonoBehaviour
{
    public static UnreadEmailDisplay instance;
    public TextMeshProUGUI text;
    int counter = 0;

    private void Awake() => instance = this;

    public void OnEnable()
    {
        GameManager.instance.InitComplete += Init;
    }

    public void OnDisable()
    {
        GameManager.instance.InitComplete -= Init;
        SceneLoader.instance.Loaded -= NewEmailSFX;
    }

    public void Init()
    {
        SetCounter(InboxFilter.instance.GetUnreadEmailsCount());
        SceneLoader.instance.Loaded += NewEmailSFX;
    }
    public void NewEmailSFX() { if (counter > 0) SFXManager.instance.Play("logon_newemails"); }

    public void SetCounter(int i)
    {
        counter = i;

        UpdateCounter();
        if (counter == 0) UpdateCounterEmpty();
    }

    public void DecreaseCounter()
    {
        counter--;
        UpdateCounter();
        if (counter == 0) UpdateCounterEmpty();
    }

    public bool AllMessagesRead() => counter == 0;
    public void UpdateCounter() => text.text = "INBOX (" + counter.ToString() + ")";
    public void UpdateCounterEmpty() => text.text = "INBOX";
}
