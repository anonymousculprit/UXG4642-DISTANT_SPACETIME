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
    public TextMeshProUGUI authorLine, dateLine, bodyText, playerTemplateText, playerTypingText;
    public GameObject replyButton, submitButton;
    private GameObject playerTypingGO, playerTemplateGO;

    public void Start()
    {
        playerTemplateGO = playerTemplateText.gameObject;
        playerTypingGO = playerTypingText.gameObject;
    }

    public void TurnOffPrefab() => prefab.SetActive(false);
    public void TurnOnPrefab() { prefab.SetActive(true); ClearReader(); }
    public void PopulateReader(string subject, string author, string date, string body, bool reply)
    {
        subjectLine.text = subject;
        authorLine.text = author;
        dateLine.text = date;
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
    }
    public void WriteReply()
    {
        ClearReader();

        Email emailData = GameManager.instance.emailDataManager.GetEmailByID(EmailDisplay.instance.playerReplyID);

        subjectLine.text = emailData.GetFieldData(EmailFields.Subject);
        authorLine.text = emailData.GetFieldData(EmailFields.Author);
        dateLine.text = emailData.GetFieldData(EmailFields.Date);
        playerTemplateText.text = emailData.GetFieldData(EmailFields.Body);
        submitButton.SetActive(true);
        playerTemplateGO.SetActive(true);
        playerTypingGO.SetActive(true);
    }
    public void SubmitPlayerReply()
    {
        // TODO: submit text/save in system?
        EmailMatrix.MarkReplyByPlayerReplyID(EmailDisplay.instance.playerReplyID);
        EmailDisplay.instance.ShowEmailSentUI();
    }
    public void UpdateTimeNow() => dateLine.text = System.DateTime.Now.ToString("MM/dd/yyyy");
}
