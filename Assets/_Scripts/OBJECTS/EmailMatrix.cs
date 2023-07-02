using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EmailMatrix
{
    public static Dictionary<int, string[]> DayToEmailRegistry = new();
    public static List<EmailResponseReply> ResponseToEmailRegistry = new();

    public static void RegisterDayEmailMatrix(int day, string[] emailID) => DayToEmailRegistry.Add(day, emailID);
    public static void RegisterResponseToEmailRegistry(EmailResponseReply entry) => ResponseToEmailRegistry.Add(entry);
    public static string[] GetEmailsByDay(int day)
    {
        string[] emails = new string[0];
        DayToEmailRegistry.TryGetValue(day, out emails);
        return emails;
    }

    public static bool EmailIDIsNPCReply(string id) => !string.IsNullOrEmpty(ResponseToEmailRegistry.Find(x => x.npcReply == id).npcReply);
    public static bool EmailIDIsPlayerReply(string id) => !string.IsNullOrEmpty(ResponseToEmailRegistry.Find(x => x.playerReply == id).playerReply);

    public static bool EmailIDHasPlayerReply(string id) => !string.IsNullOrEmpty(ResponseToEmailRegistry.Find(x => x.npcEmail == id).playerReply);
    public static bool EmailIDHasNPCReply(string id) => !string.IsNullOrEmpty(ResponseToEmailRegistry.Find(x => x.npcEmail == id).npcReply);
    public static bool PlayerReplyIDHasNPCReply(string id) => !string.IsNullOrEmpty(ResponseToEmailRegistry.Find(x => x.playerReply == id).npcReply);

    public static string GetPlayerReplyByEmailID(string id) => ResponseToEmailRegistry.Find(x => x.npcEmail == id).playerReply;
    public static string GetNPCReplyByEmailID(string id) => ResponseToEmailRegistry.Find(x => x.npcEmail == id).npcReply;
    public static string GetNPCReplyByPlayerReplyID(string id) => ResponseToEmailRegistry.Find(x => x.playerReply == id).npcReply;
    public static void MarkReplyByPlayerReplyID(string id) => ResponseToEmailRegistry.Find(x => x.playerReply == id).PlayerHasReplied();
    public static bool PlayerHasRepliedToEmailID(string id) => ResponseToEmailRegistry.Find(x => x.npcEmail == id).replied;
    public static bool PlayerHasRepliedToNPCReplyID(string id) => ResponseToEmailRegistry.Find(x => x.npcReply == id).replied;
}

public struct EmailResponseReply
{
    public string npcEmail { get; private set; }
    public string playerReply { get; private set; }
    public string npcReply { get; private set; }
    public bool replied { get; private set; }

    public EmailResponseReply(string email, string response, string reply)
    {
        this.npcEmail = email;
        this.playerReply = response;
        this.npcReply = reply;
        replied = false;
    }

    public void PlayerHasReplied() => replied = true;
}