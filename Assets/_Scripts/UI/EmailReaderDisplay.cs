using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class EmailReaderDisplay : MonoBehaviour
{
    public static EmailReaderDisplay instance;
    public event Announcement UnselectingEmail;
    public GameObject emailReaderPrefabGO;
    public EmailReaderPrefab emailReaderPrefabClass;
    EmailAppender emailAppender = new();
    public string currentEmailID;
    public string playerReplyID;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        emailReaderPrefabClass = emailReaderPrefabGO.GetComponent<EmailReaderPrefab>();
    }

    public void LoadReaderContents(string id, bool hasReply)
    {
        currentEmailID = id;
        Email email = GameManager.emailDataManager.GetEmailByID(currentEmailID);

        bool trackingPlayerReplyID = hasReply ? true : false;
        playerReplyID = trackingPlayerReplyID ? EmailMatrix.GetPlayerReplyByEmailID(id) : "";
        bool showReplyButton = ShowReplyButton(trackingPlayerReplyID);


        string bodyText = email.GetFieldData(EmailFields.Body);
        if (emailAppender.EmailNeedsFormattingForToday(id)) bodyText = emailAppender.AppendEmailForEmailReader(id, bodyText);

        emailReaderPrefabClass.PopulateReader(
            subject: email.GetFieldData(EmailFields.Subject),
            author: email.GetFieldData(EmailFields.Author),
            date: email.GetFieldData(EmailFields.Date),
            body: bodyText,
            reply: showReplyButton);

        email.MarkAsRead();
    }

    public void ShowEmailSentUI()
    {
        EmailSentDisplay.instance.ShowPopup();
        UnselectingEmail?.Invoke();
        UnloadReaderContents();
    }

    public void UnloadReaderContents()
    {
        currentEmailID = "";
        playerReplyID = "";
        emailReaderPrefabClass.ClearReader();
    }

    bool ShowReplyButton(bool defaultState)
    {
        if (GameManager.instance.GetDay() == 7)
        {
            if (EmailMatrix.EmailIsFromToday(7, currentEmailID))
            {
                if (!EmailMatrix.EmailIDHasPlayerReply(currentEmailID)) return true;
                if (GameManager.instance.FinishedMainStory()) return false;
                return false;
            }
        }
        return defaultState;
    }
}
