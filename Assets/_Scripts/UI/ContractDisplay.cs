using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContractDisplay : MonoBehaviour
{
    public TextMeshProUGUI refText, liveText;
    public bool DEBUG_SkipText = false;
    public float defaultTypingSpeed = 0.05f;
    public float shortPause = 2f;
    public float longPause = 3f;
    string text;

    ContractManager alert;

    public void ANIM_DisplayContract()
    {
        SaveText();
        StartCoroutine(LoadText());
        alert = GetComponent<ContractManager>();
    }

    public void SaveText()
    {
        text = refText.text;
        liveText.text = "";
    }

    IEnumerator LoadText()
    {
        if (DEBUG_SkipText)
        {
            liveText.text = text;
            yield return new WaitForSeconds(1.5f);
            alert.DisplaySignature();
            yield break;
        }

        for (int i = 0; i < text.Length; i++)
        {
            liveText.text += text[i];
            yield return new WaitForSeconds(DeterminePauseTimer(text[i]));
        }
        alert.DisplaySignature();
    }

    float DeterminePauseTimer(char c)
    {
        switch (c)
        {
            case ',': return shortPause;
            case '\n':
            case '!':
            case '.': return longPause;
            default: return defaultTypingSpeed;
        }
    }
}
