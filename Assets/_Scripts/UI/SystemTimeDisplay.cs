using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SystemTimeDisplay : MonoBehaviour
{
    public TextMeshProUGUI text;

    private void Start()
    {
        if (text == null)
            text = gameObject.GetComponent<TextMeshProUGUI>();

        StartCoroutine(UpdateTime());
    }

    IEnumerator UpdateTime()
    {
        UpdateTimeDisplay();

        // calculate time sync with irl seconds systemtime
        int second = System.DateTime.Now.Second;
        if (second != 0) yield return new WaitForSeconds(60 - second);

        // once seconds are synced, update time accordingly
        while (true)
        {
            UpdateTimeDisplay();
            yield return new WaitForSeconds(60);
        }
    }

    void UpdateTimeDisplay()
    {
        int hour = System.DateTime.Now.Hour;
        int minute = System.DateTime.Now.Minute;

        text.text = hour + ":" + (minute < 10 ? "0" + minute : minute);
    }

    private void OnDisable() => StopAllCoroutines();
}
