using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class EmailAppender
{
    // to get body text, find player reply, append it to body text and return it to email display
    // or should i just save it under the id? lmao,

    string dividerGaps = "\n\n";
    string divider = "================================================";

    Dictionary<string, string> editedEmails = new();

    public bool EmailNeedsFormattingForToday(string id)
    {
        if (EmailMatrix.EmailIsFromToday(GameManager.instance.GetDay(), id))
        {
            if (GameManager.instance.GetDay() == 7 && GameManager.instance.FinishedMainStory()) return false;
            if (EmailMatrix.PlayerHasRepliedToEmailID(id)) return true;
        }
        return false;
    }

    public string AppendEmailForEmailReader(string id, string body)
    {
        string result = "";
        if (editedEmails.ContainsKey(id))
            editedEmails.TryGetValue(id, out result);
        else
            result = RegisterNewEditedEmail(id, body);

        return result;
    }

    public string AppendEmailForCustomEmailsManager(string npcEmailID, string playerReplyBody) 
    {
        string npcEmailBody = GameManager.emailDataManager.GetEmailByID(npcEmailID).Get(EmailFields.Body);
        string result = AppendEmailBtoA(playerReplyBody, npcEmailBody);
        editedEmails.Add(npcEmailID, result);
        return result;
    }

    public string RegisterNewEditedEmail(string npcEmailID, string npcEmailBody)
    {
        string playerReplyID = EmailMatrix.GetPlayerReplyByEmailID(npcEmailID);
        string playerReplyBody = GameManager.emailDataManager.GetEmailByID(playerReplyID).Get(EmailFields.Body);
        string result = AppendEmailBtoA(playerReplyBody, npcEmailBody);
        editedEmails.Add(npcEmailID, result);
        return result;
    }

    public string AppendEmailBtoA(string b, string a)
    {
        StringBuilder sb = new();
        sb.Append(b);
        sb.Append(dividerGaps + divider + dividerGaps);
        sb.Append(a);
        return sb.ToString();
    }

    public string AppendEmailBtoA(Email b, Email a) => AppendEmailBtoA(b.Get(EmailFields.Body), a.Get(EmailFields.Body));

    public void AssembleEmail(ref Email emailToEdit, Email originalEmail, Email additionalAppend = null) 
    {
        string newBody = "";
        if (additionalAppend == null)
        {
            newBody = AppendEmailBtoA(emailToEdit, originalEmail);
        }
        else
        {
            newBody = AppendEmailBtoA(additionalAppend, originalEmail);
            newBody = AppendEmailBtoA(emailToEdit.Get(EmailFields.Body), newBody);
        }

        emailToEdit.ReplaceBodyText(newBody);
    }
}