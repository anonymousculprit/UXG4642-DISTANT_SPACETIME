using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class InboxFilter : MonoBehaviour
{
    public static InboxFilter instance;
    public string divider = "=====================";
    bool keepPreviousDayEmails, listAllEmails;

    List<Email> inbox = new();
    EmailAppender emailAppender = new();

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
                        Email npcReply;
                        Email playerReply = inbox.Find(x => x.GetFieldData(EmailFields.ID) == EmailMatrix.GetPlayerReplyByEmailID(emailID));
                        if (EmailMatrix.EmailIDHasNPCReply(emailID))
                        {
                            npcReply = inbox.Find(x => x.GetFieldData(EmailFields.ID) == EmailMatrix.GetNPCReplyByEmailID(emailID));
                            if (!npcReply.HasBeenEdited())
                            {
                                string newBody = emailAppender.AppendEmailBtoA(playerReply.Get(EmailFields.Body), email.Get(EmailFields.Body));
                                newBody = emailAppender.AppendEmailBtoA(npcReply.Get(EmailFields.Body), newBody);

                                npcReply.ReplaceBodyText(newBody);
                            }

                            emailRemovalList.Add(playerReply);
                            emailRemovalList.Add(email);
                        }
                        else
                        {
                            if (!playerReply.HasBeenEdited())
                            {
                                string newBody = emailAppender.AppendEmailBtoA(playerReply.Get(EmailFields.Body), email.Get(EmailFields.Body));
                                playerReply.ReplaceBodyText(newBody);
                                playerReply.MarkAsRead();
                            }

                            emailRemovalList.Add(email);
                        }
                        continue;
                    }
                    else
                    {
                        string pReplyID = EmailMatrix.GetPlayerReplyByEmailID(emailID);
                        Email pReply = inbox.Find(x => x.GetFieldData(EmailFields.ID) == pReplyID);
                        if (pReply != null) emailRemovalList.Add(pReply);

                        if (EmailMatrix.EmailIDHasNPCReply(emailID)) 
                        {
                            string nReplyID = EmailMatrix.GetNPCReplyByEmailID(emailID);
                            Email nReply = inbox.Find(x => x.GetFieldData(EmailFields.ID) == nReplyID);
                            if (nReply != null) emailRemovalList.Add(nReply);
                        }
                    }
                }
            }

        if (listAllEmails) return;
        if (emailRemovalList.Count > 0)
            foreach (Email email in emailRemovalList)
                inbox.Remove(email);

        inbox.Reverse();
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
