using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EmailGrabber : MonoBehaviour
{
    public static EmailGrabber instance;
    public bool keepPreviousDayEmails = false;
    public string divider = "=====================";

    List<Email> inbox = new();

    private void Awake()
    {
        instance = this;
    }

    public void Init(int day)
    {
        if (keepPreviousDayEmails && day > 1)
        {
            for (int i = 1; i < day; i++)
                PopulateInboxByDay(i);
        }
        PopulateInboxByDay(day);
    }

    public void PopulateInboxByDay(int day)
    {
        string[] emails = EmailMatrix.GetEmailsByDay(day);
        foreach (string emailID in emails)
            inbox.Add(GameManager.instance.emailDataManager.GetEmailByID(emailID));
        SortEmailsInInbox();
    }

    public void SortEmailsInInbox()
    {
        List<Email> emailRemovalList = new();

        if (inbox.Count > 0)
            foreach (Email email in inbox)
            {
                if (email == null) continue;
                string emailID = email.GetFieldData(EmailFields.ID);

                if (EmailMatrix.EmailIDHasPlayerReply(emailID))
                {
                    if (EmailMatrix.PlayerHasRepliedToEmailID(emailID))
                    {
                        Email playerReply = inbox.Find(x => x.GetFieldData(EmailFields.ID) == EmailMatrix.GetPlayerReplyByEmailID(emailID));
                        Email npcReply = inbox.Find(x => x.GetFieldData(EmailFields.ID) == EmailMatrix.GetNPCReplyByEmailID(emailID));

                        StringBuilder sb = new();
                        sb.Append(npcReply.GetFieldData(EmailFields.Body));
                        sb.Append("\n" + divider + "\n");
                        sb.Append(playerReply.GetFieldData(EmailFields.Body));
                        sb.Append("\n" + divider + "\n");
                        sb.Append(email.GetFieldData(EmailFields.Body));

                        emailRemovalList.Add(playerReply);
                        emailRemovalList.Add(email);
                        continue;
                    }

                    Debug.Log("email.ID: " + email.GetFieldData(EmailFields.ID));

                    Email pReply = inbox.Find(x => x.GetFieldData(EmailFields.ID) == EmailMatrix.GetPlayerReplyByEmailID(emailID));
                    Email nReply = inbox.Find(x => x.GetFieldData(EmailFields.ID) == EmailMatrix.GetNPCReplyByEmailID(emailID));
                    if (pReply != null) emailRemovalList.Add(pReply);
                    if (nReply != null) emailRemovalList.Add(nReply);
                }
            }

        if (emailRemovalList.Count > 0)
            foreach(Email email in emailRemovalList)
                inbox.Remove(email);
    }

    public int GetUnreadEmailsCount()
    {
        List<Email> unreadEmails = inbox.FindAll(x => x != null && !x.HasBeenRead());
        if (unreadEmails == null || unreadEmails.Count == 0) return 0;
        return unreadEmails.Count;
    }
    public List<Email> GetInbox() => inbox;
}
