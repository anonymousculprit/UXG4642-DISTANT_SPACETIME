using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContractDisplay : MonoBehaviour
{
    public TextMeshProUGUI refText, liveText;
    string fullText;

    public void StartTyping()
    {
        fullText = refText.text;
    }

}
