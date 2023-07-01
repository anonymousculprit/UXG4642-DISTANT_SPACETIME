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

    public static bool EmailIDHasPlayerReply(string id) => !string.IsNullOrEmpty(ResponseToEmailRegistry.Find(x => x.npcEmail == id).playerReply);
    public static bool EmailIDHasNPCReply(string id) => !string.IsNullOrEmpty(ResponseToEmailRegistry.Find(x => x.npcEmail == id).npcReply);
    public static bool PlayerReplyIDHasNPCReply(string id) => !string.IsNullOrEmpty(ResponseToEmailRegistry.Find(x => x.playerReply == id).npcReply);
    public static string GetPlayerReplyByEmailID(string id) => ResponseToEmailRegistry.Find(x => x.npcEmail == id).playerReply;
    public static string GetNPCReplyByEmailID(string id) => ResponseToEmailRegistry.Find(x => x.npcEmail == id).npcReply;
    public static string GetNPCReplyByPlayer(string id) => ResponseToEmailRegistry.Find(x => x.playerReply == id).npcReply;
}

public struct EmailResponseReply
{
    public string npcEmail { get; private set; }
    public string playerReply { get; private set; }
    public string npcReply { get; private set; }

    public EmailResponseReply(string email, string response, string reply)
    {
        this.npcEmail = email;
        this.playerReply = response;
        this.npcReply = reply;
    }
}