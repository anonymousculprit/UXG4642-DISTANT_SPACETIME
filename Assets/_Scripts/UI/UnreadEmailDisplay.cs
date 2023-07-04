using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnreadEmailDisplay : MonoBehaviour
{
    public TextMeshProUGUI text;
    int counter = 0;

    public void OnEnable()
    {
        GameManager.instance.InitComplete += Init;
    }

    public void OnDisable()
    {
        GameManager.instance.InitComplete -= Init;
    }

    public void Init() => SetCounter(InboxFilter.instance.GetUnreadEmailsCount());

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
