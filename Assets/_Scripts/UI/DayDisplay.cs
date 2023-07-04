using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayDisplay : MonoBehaviour
{
    public TextMeshProUGUI dayText;

    private void Start()
    {
        dayText.text = "DAY " + GameManager.instance.GetDay();
    }
}
