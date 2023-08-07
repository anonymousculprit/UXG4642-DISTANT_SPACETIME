using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class EmailReaderPrefab : MonoBehaviour
{
    public GameObject prefab;
    public TextMeshProUGUI subjectLine;
    public TextMeshProUGUI authorLine, dateLine, bodyText, playerTemplateText;
    public TMP_InputField playerTypingText;
    public GameObject replyButton, submitButton, selectTypingTextButton;
    private GameObject playerTypingGO, playerTemplateGO;

    public void Start()
    {
        playerTemplateGO = playerTemplateText.gameObject;
        playerTypingGO = playerTypingText.gameObject;
        ClearReader();
    }

    public void TurnOffPrefab() => prefab.SetActive(false);
    public void TurnOnPrefab() { prefab.SetActive(true); ClearReader(); }
    public void PopulateReader(string subject, string author, string date, string body, bool reply)
    {
        ClearReader();
        subjectLine.text = subject;
        authorLine.text = "From: " + author;
        dateLine.text = "Date: " + date;
        bodyText.text = body;
        replyButton.SetActive(reply);
    }
    public void ClearReader()
    {
        subjectLine.text = "";
        authorLine.text = "";
        dateLine.text = "";
        bodyText.text = "";
        playerTemplateText.text = "";
        playerTypingText.text = "";
        playerTemplateGO.SetActive(false);
        playerTypingGO.SetActive(false);
        replyButton.SetActive(false);
        submitButton.SetActive(false);
        selectTypingTextButton.SetActive(false);
    }
    public void WriteReply()
    {
        SFXManager.instance.Play("email_write");
        ClearReader();

        if (GameManager.instance.GetDay() == 7)
        {
            Email currentEmail = GameManager.emailDataManager.GetEmailByID(EmailReaderDisplay.instance.currentEmailID);

            subjectLine.text = "RE: " + currentEmail.Get(EmailFields.Subject);
            authorLine.text = "Dylon Smith";
            dateLine.text = currentEmail.Get(EmailFields.Date);
        }
        else
        {
            Email emailData = GameManager.emailDataManager.GetEmailByID(EmailReaderDisplay.instance.playerReplyID);

            subjectLine.text = emailData.GetFieldData(EmailFields.Subject);
            authorLine.text = emailData.GetFieldData(EmailFields.Author);
            dateLine.text = emailData.GetFieldData(EmailFields.Date);
            playerTemplateText.text = emailData.GetFieldData(EmailFields.Body);
            playerTypingGO.SetActive(true);
        }
        playerTemplateGO.SetActive(true);
        playerTypingGO.SetActive(true);
        if (SelectText()) playerTypingText.Select();
        selectTypingTextButton.SetActive(true);
    }

    bool SelectText()
    {
        if (GameManager.instance.GetDay() == 7) return true;
        if (!Options.GetAutoCompleteEmail()) return true;
        return false;
    }

    public void SubmitPlayerReply()
    {
        CustomEmailsManager.instance.SaveEmail();
        EmailMatrix.MarkReplyByPlayerReplyID(EmailReaderDisplay.instance.playerReplyID);
        UpdateEmailPrefab();
        EmailReaderDisplay.instance.ShowEmailSentUI();
        ClearReader();
    }

    void UpdateEmailPrefab()
    {
        Email currentEmail = GameManager.emailDataManager.GetEmailByID(EmailReaderDisplay.instance.currentEmailID);
        EmailPrefab prefab = InboxDisplay.instance.GetEmailPrefab(currentEmail);

        if (GameManager.instance.GetDay() == 7)
        {
            prefab.UpdateEmailPrefabAfterReply("RE: " + currentEmail.Get(EmailFields.Subject), "Dylon Smith");
        }
        else
        {
            Email emailData = GameManager.emailDataManager.GetEmailByID(EmailReaderDisplay.instance.playerReplyID);
            prefab.UpdateEmailPrefabAfterReply(emailData.GetFieldData(EmailFields.Subject), emailData.GetFieldData(EmailFields.Author));
        }
    }
}
