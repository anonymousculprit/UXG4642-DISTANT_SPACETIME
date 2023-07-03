using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnreadEmailDisplay : MonoBehaviour
{
    public TextMeshProUGUI text;
    //public GameObject blip;
    int counter = 0;

    public void OnEnable()
    {
        GameManager.instance.InitComplete += GetCounter;
    }

    public void OnDisable()
    {
        GameManager.instance.InitComplete -= GetCounter;
    }

    private void Start()
    {
        if (text == null)
            text = gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void GetCounter() => SetCounter(EmailGrabber.instance.GetUnreadEmailsCount());

    public void SetCounter(int i)
    {
        counter = i;

        UpdateCounter();
        if (counter == 0) UpdateCounterEmpty();
        
        //if (counter == 0)
        //    TurnOffBlip();
        //else
        //    TurnOnBlip();
    }

    public void DecreaseCounter()
    {
        counter--;
        //if (counter == 0) TurnOffBlip();
        //else UpdateCounter();
        UpdateCounter();
        if (counter == 0) UpdateCounterEmpty();
    }

    public bool AllMessagesRead() => counter == 0;
    public void UpdateCounter() => text.text = "Inbox (" + counter.ToString() + ")";
    public void UpdateCounterEmpty() => text.text = "Inbox";
    //public void TurnOnBlip() => blip.SetActive(true);
    //public void TurnOffBlip() => blip.SetActive(false);
}
