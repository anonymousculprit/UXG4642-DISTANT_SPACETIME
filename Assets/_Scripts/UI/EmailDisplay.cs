using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class EmailDisplay : MonoBehaviour
{
    public static EmailDisplay instance;
    public event Announcement UnselectingEmail;
    public GameObject emailReaderPrefabGO;
    public EmailReaderPrefab emailReaderPrefabClass;
    public string currentEmailID;
    public string playerReplyID;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        emailReaderPrefabClass = emailReaderPrefabGO.GetComponent<EmailReaderPrefab>();
    }

    public void LoadReaderContents(string id)
    {
        bool addId = EmailMatrix.EmailIDHasPlayerReply(id);
        currentEmailID = id;
        playerReplyID = addId ? EmailMatrix.GetPlayerReplyByEmailID(id) : "";

        Email email = GameManager.instance.emailDataManager.GetEmailByID(id);
        emailReaderPrefabClass.PopulateReader(
            subject: email.GetFieldData(EmailFields.Subject),
            author: email.GetFieldData(EmailFields.Author),
            date: email.GetFieldData(EmailFields.Date),
            body: email.GetFieldData(EmailFields.Body),
            reply: addId);

        email.MarkAsRead();
    }

    public void ShowEmailSentUI()
    {
        // TODO: Show Email Sent UI
        UnselectingEmail?.Invoke();
        UnloadReaderContents();
    }

    public void UnloadReaderContents()
    {
        currentEmailID = "";
        playerReplyID = "";
        emailReaderPrefabClass.ClearReader();
    }
    
}
