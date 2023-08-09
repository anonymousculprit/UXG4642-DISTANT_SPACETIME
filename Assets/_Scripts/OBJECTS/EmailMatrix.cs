using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EmailMatrix
{
    public static Dictionary<int, string[]> DayToEmailRegistry = new();
    public static List<EmailResponseReply> ResponseToEmailRegistry = new();

    public static void ClearInstance() { DayToEmailRegistry = new(); ResponseToEmailRegistry = new(); }
    public static void RegisterDayEmailMatrix(int day, string[] emailID) => DayToEmailRegistry.Add(day, emailID);
    public static void RegisterResponseToEmailRegistry(EmailResponseReply entry) => ResponseToEmailRegistry.Add(entry);
    public static string[] GetEmailsByDay(int day)
    {
        string[] emails = new string[0];
        DayToEmailRegistry.TryGetValue(day, out emails);
        return emails;
    }

    public static bool EmailIsFromToday(int day, string emailID)
    {
        List<string> emailsSearchable = new();
        string[] emailsByDay = GetEmailsByDay(day);
        if (emailsByDay != null) emailsSearchable.AddRange(emailsByDay);
        return emailsSearchable.Contains(emailID);
    }

    public static bool EmailIsInsideInboxFilter(int day, string emailID)
    {
        for (int i = 1; i < day; i++)
            if (EmailIsFromToday(i, emailID)) return true;
        return EmailIsFromToday(day, emailID);
    }

    public static bool EmailIDIsNPCReply(string id) => !string.IsNullOrEmpty(ResponseToEmailRegistry.Find(x => x.npcReply == id).npcReply);
    public static bool EmailIDIsPlayerReply(string id) => !string.IsNullOrEmpty(ResponseToEmailRegistry.Find(x => x.playerReply == id).playerReply);

    public static bool EmailIDHasPlayerReply(string id) => !string.IsNullOrEmpty(ResponseToEmailRegistry.Find(x => x.npcEmail == id).playerReply);
    public static bool EmailIDHasNPCReply(string id) => !string.IsNullOrEmpty(ResponseToEmailRegistry.Find(x => x.npcEmail == id).npcReply);
    public static bool PlayerReplyIDHasNPCReply(string id) => !string.IsNullOrEmpty(ResponseToEmailRegistry.Find(x => x.playerReply == id).npcReply);

    public static string GetPlayerReplyByEmailID(string id) => ResponseToEmailRegistry.Find(x => x.npcEmail == id).playerReply;
    public static string GetNPCReplyByEmailID(string id) => ResponseToEmailRegistry.Find(x => x.npcEmail == id).npcReply;
    public static string GetNPCReplyByPlayerReplyID(string id) => ResponseToEmailRegistry.Find(x => x.playerReply == id).npcReply;
    public static string GetEmailIDByPlayerReplyID(string id) => ResponseToEmailRegistry.Find(x => x.playerReply == id).npcEmail;
    public static bool PlayerHasRepliedToEmailID(string id) => ResponseToEmailRegistry.Find(x => x.npcEmail == id).replied;
    public static bool PlayerHasRepliedToNPCReplyID(string id) => ResponseToEmailRegistry.Find(x => x.npcReply == id).replied;

    public static bool EmailIDHasMetRequirements(string id)
    {
        EmailResponseReply matrixEntry = ResponseToEmailRegistry.Find(x => x.npcEmail == id);

        if (!matrixEntry.HasRequirements()) return true;

        foreach (string requirementID in matrixEntry.requirements)
            if (!PlayerHasRepliedToEmailID(requirementID)) return false;
        return true;
    }

    public static void MarkReplyByPlayerReplyID(string id) // haha this was longer than i thought because structs pass by value and not by ref :')
    {
        EmailResponseReply updatingEntry = ResponseToEmailRegistry.Find(x => x.playerReply == id);
        ResponseToEmailRegistry.Remove(updatingEntry);
        updatingEntry.PlayerHasReplied();
        ResponseToEmailRegistry.Add(updatingEntry);
    }
}

