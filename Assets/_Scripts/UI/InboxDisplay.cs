using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InboxDisplay : MonoBehaviour
{
    public GameObject emailPrefab, emailContainer, canvas;
    bool canReplyToPreviousDayEmails;

    public void OnEnable()
    {
        GameManager.instance.InitComplete += LoadInbox;
    }

    public void OnDisable()
    {
        GameManager.instance.InitComplete -= LoadInbox;
    }

    void LoadInbox()
    {
        canReplyToPreviousDayEmails = GameManager.instance.canReplyToPreviousDayEmails;
        List<string> todaysEmails = new();
        todaysEmails.AddRange(EmailMatrix.GetEmailsByDay(GameManager.instance.GetDay()));

        List<Email> emails = EmailGrabber.instance.GetInbox();
        foreach(Email email in emails)
        {
            if (email == null) continue;

            GameObject newEmailGO = Instantiate(emailPrefab);
            newEmailGO.transform.SetParent(emailContainer.transform);
            float x = transform.localScale.x / canvas.transform.localScale.x;
            newEmailGO.transform.localScale = new Vector3(x,x,x);

            string id = email.GetFieldData(EmailFields.ID);
            EmailPrefab newEmailClass = newEmailGO.GetComponent<EmailPrefab>();
            newEmailClass.Init(
                id: id,
                subject: email.GetFieldData(EmailFields.Subject),
                author: email.GetFieldData(EmailFields.Author),
                reply: CheckForShowReply(id)
                ); 
        }

        bool CheckForShowReply(string id)
        {
            if (canReplyToPreviousDayEmails)
                if (EmailMatrix.EmailIDHasPlayerReply(id) && !EmailMatrix.PlayerHasRepliedToEmailID(id))
                    return true;

            if (EmailMatrix.EmailIDHasPlayerReply(id))
                if (!EmailMatrix.PlayerHasRepliedToEmailID(id) && EmailIsNotFromToday(id))
                    return true;
            return false;
        }

        bool EmailIsNotFromToday(string id) => !string.IsNullOrEmpty(todaysEmails.Find(x => x == id));
    }


}
