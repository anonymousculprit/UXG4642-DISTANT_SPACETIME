using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SignatureInput : MonoBehaviour
{
    public static SignatureInput instance;

    public event Announcement ShowSubmitButton;
    public TextMeshProUGUI templateText, highlightText;
    public TMP_InputField playerText;
    public GameObject submitButton;
    public Color highlightColor;

    private void Awake() => instance = this;

    public void SelectInputField() => playerText.Select();

    public void OnValueChanged()
    {
        CheckToAddMark();

        if (playerText.text == templateText.text)
        {
            ShowSubmitButton?.Invoke();
            playerText.interactable = false;
            EventSystem.current.SetSelectedGameObject(null);
        }
        else
            submitButton.SetActive(false);
    }

    void TypingNoise()
    {
        if (Input.anyKeyDown && !(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)))
            SFXManager.instance.PlayTypingNoise();
    }

    void CheckToAddMark()
    {
        highlightText.text = playerText.text;
        bool highlightActive = false;
        for (int i = 0; i < playerText.text.Length; i++)
        {
            if (playerText.text[i] != templateText.text[i])
            {
                string s = "<mark=#" + ColorUtility.ToHtmlStringRGBA(highlightColor) + ">";
                highlightText.text = highlightText.text.Insert(i, s);
                highlightActive = true;
                break;
            }
        }

        if (highlightActive)
            highlightText.text += "</mark>";
    }

    private void Update() => TypingNoise();
    public void SelectText() => playerText.Select();
}
