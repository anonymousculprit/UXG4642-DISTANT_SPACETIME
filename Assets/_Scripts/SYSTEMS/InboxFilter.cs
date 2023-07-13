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

    private void Awake() => instance = this;

    public void Init(int day)
    {
        keepPreviousDayEmails = GameManager.instance.keepPreviousDayEmails;
        listAllEmails = GameManager.instance.DEBUG_listAllEmails;

        if (keepPreviousDayEmails && day > 1)
            for (int i = 1; i < day; i++)
                PopulateInboxByDay(i);

        PopulateInboxByDay(day);
        SortEmailsInInbox();
    }

    public void PopulateInboxByDay(int day)
    {
        string[] emails = EmailMatrix.GetEmailsByDay(day);
        if (emails == null) return;
        foreach (string emailID in emails)
            inbox.Add(GameManager.emailDataManager.GetEmailByID(emailID));
    }

    public void SortEmailsInInbox()
    {
        List<Email> emailRemovalList = new();

        if (inbox.Count == 0) return;

        foreach (Email email in inbox)
        {
            if (email == null) continue;

            string emailID = email.GetFieldData(EmailFields.ID);

            if (!EmailMatrix.EmailIDHasMetRequirements(emailID))
            {
                Debug.Log("emailID " + emailID + " has not met requirements!");
                emailRemovalList.Add(email);
                if (EmailMatrix.EmailIDHasPlayerReply(emailID)) emailRemovalList.Add(GetPlayerReply(emailID));
                if (EmailMatrix.EmailIDHasNPCReply(emailID) && EmailMatrix.EmailIsFromToday(GameManager.instance.GetDay(), EmailMatrix.GetNPCReplyByEmailID(emailID)))
                    emailRemovalList.Add(GetNPCReply(emailID));
                continue;
            }

            if (EmailMatrix.EmailIDHasPlayerReply(emailID))
            {
                if (EmailMatrix.PlayerHasRepliedToEmailID(emailID)) // operating under assumption that replies are in inbox
                {
                    bool hasNPCReply = EmailMatrix.EmailIDHasNPCReply(emailID);
                    Email emailToEdit = hasNPCReply ? GetNPCReply(emailID) : GetPlayerReply(emailID);

                    if (!emailToEdit.HasBeenEdited())
                    {
                        Email additionalAppend = hasNPCReply ? GetPlayerReply(emailID) : null;
                        emailAppender.AssembleEmail(ref emailToEdit, email, additionalAppend);
                        if (!hasNPCReply) emailToEdit.MarkAsRead();
                    }

                    emailRemovalList.Add(email);
                    if (hasNPCReply) emailRemovalList.Add(GetPlayerReply(emailID));
                }
                else
                {
                    emailRemovalList.Add(GetPlayerReply(emailID));
                    if (EmailMatrix.EmailIDHasNPCReply(emailID)) emailRemovalList.Add(GetNPCReply(emailID));
                    //FlagEmailForRemovalByID(EmailMatrix.GetPlayerReplyByEmailID(emailID));
                    //if (EmailMatrix.EmailIDHasNPCReply(emailID)) FlagEmailForRemovalByID(EmailMatrix.GetNPCReplyByEmailID(emailID));
                }
            }
        }

        if (listAllEmails) { inbox.Reverse(); return; }
        RemoveAllFlaggedEmails();
        inbox.Reverse();
        DEBUG_CheckAllEmailsInsideInbox();
        void RemoveAllFlaggedEmails()
        {
            if (emailRemovalList.Count > 0)
                foreach (Email email in emailRemovalList)
                    inbox.Remove(email);
        }
        //void FlagEmailForRemovalByID(string id)
        //{
        //    Email flaggedEmail = inbox.Find(x => x.Get(EmailFields.ID) == id);
        //    if (flaggedEmail != null) emailRemovalList.Add(flaggedEmail);
        //}
        Email GetNPCReply(string id) => inbox.Find(x => x.GetFieldData(EmailFields.ID) == EmailMatrix.GetNPCReplyByEmailID(id));
        Email GetPlayerReply(string id) => inbox.Find(x => x.GetFieldData(EmailFields.ID) == EmailMatrix.GetPlayerReplyByEmailID(id));
    }

    public void DEBUG_CheckAllEmailsInsideInbox()
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
