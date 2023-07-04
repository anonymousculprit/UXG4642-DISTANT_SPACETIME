using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EmailGrabber : MonoBehaviour
{
    public static EmailGrabber instance;
    public string divider = "=====================";
    bool keepPreviousDayEmails, listAllEmails;

    List<Email> inbox = new();

    private void Awake()
    {
        instance = this;
    }

    public void Init(int day)
    {
        keepPreviousDayEmails = GameManager.instance.keepPreviousDayEmails;
        listAllEmails = GameManager.instance.DEBUG_listAllEmails;
        if (keepPreviousDayEmails && day > 1)
        {
            for (int i = 1; i < day; i++)
                PopulateInboxByDay(i);
        }
        PopulateInboxByDay(day);
        SortEmailsInInbox();
    }

    public void PopulateInboxByDay(int day)
    {
        string[] emails = EmailMatrix.GetEmailsByDay(day);
        foreach (string emailID in emails)
            inbox.Add(GameManager.emailDataManager.GetEmailByID(emailID));
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
                    else
                    {
                        string pReplyID = EmailMatrix.GetPlayerReplyByEmailID(emailID);
                        string nReplyID = EmailMatrix.GetNPCReplyByEmailID(emailID);

                        Email pReply = inbox.Find(x => x.GetFieldData(EmailFields.ID) == pReplyID);
                        Email nReply = inbox.Find(x => x.GetFieldData(EmailFields.ID) == nReplyID);
                        if (pReply != null) emailRemovalList.Add(pReply);
                        if (nReply != null) emailRemovalList.Add(nReply);
                    }
                }
            }

        if (listAllEmails) return;
        if (emailRemovalList.Count > 0)
            foreach (Email email in emailRemovalList)
                inbox.Remove(email);
    }

    public void CheckAllEmailsInsideInbox()
    {
        foreach (Email email in inbox)
        {
            if (email != null)
                Debug.Log("email: " + email.Get(EmailFields.ID));
        }
    }

    public int GetUnreadEmailsCount()
    {
        List<Email> unreadEmails = inbox.FindAll(x => x != null && !x.HasBeenRead());
        if (unreadEmails == null || unreadEmails.Count == 0) return 0;
        return unreadEmails.Count;
    }
    public List<Email> GetInbox() => inbox;
}
