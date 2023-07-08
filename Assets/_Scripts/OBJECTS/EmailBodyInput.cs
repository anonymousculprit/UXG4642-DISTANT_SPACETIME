using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class EmailBodyInput : MonoBehaviour
{
    public TextMeshProUGUI templateText;
    public TMP_InputField playerText;
    public GameObject submitButton;

    public void OnValueChanged()
    {
        string s = "";
        for (int i = 0; i < playerText.text.Length; i++)
        {
            s += playerText.text[i];
            if (playerText.text[i] != templateText.text[i])
            {
                Debug.Log("error at: " + playerText.text[i].ToString() + "| i: " + i);;
                Debug.Log("s: " + s);
            }
        }

        if (playerText.text == templateText.text)
            submitButton.SetActive(true);
        else
            submitButton.SetActive(false);
    }

    public void TypingNoise()
    {
        if (Input.anyKeyDown && !(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)))
            SFXManager.instance.PlayTypingNoise();
    }

    private void Update() => TypingNoise();
}
