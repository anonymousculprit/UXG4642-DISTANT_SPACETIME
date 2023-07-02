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
        if (instance = null)
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
        {
            inbox.Add(GameManager.instance.emailDataManager.GetEmailByID(emailID));
        }
        SortEmailsInInbox();
    }

    public void SortEmailsInInbox()
    {
        List<Email> emailRemovalList = new();

        foreach (Email email in inbox)
        {
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

                emailRemovalList.Add(inbox.Find(x => x.GetFieldData(EmailFields.ID) == EmailMatrix.GetPlayerReplyByEmailID(emailID)));
                emailRemovalList.Add(inbox.Find(x => x.GetFieldData(EmailFields.ID) == EmailMatrix.GetNPCReplyByEmailID(emailID)));
            }
        }

        foreach(Email email in emailRemovalList)
            inbox.Remove(email);
    }

    public int GetUnreadEmailsCount() => inbox.FindAll(x => !x.HasBeenRead()).Count;
    public List<Email> GetInbox() => inbox;
}
