using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Email
{
    public Email(string[] fields, string[] emailData) => CreateEmail(fields, emailData);

    Dictionary<string, string> data = new Dictionary<string, string>();
    bool markAsRead = false;
    bool hasBeenEdited = false;

    public void CreateEmail(string[] fields, string[] emailData)
    {
        for (int i = 0; i < fields.Length; i++)
        {
            if (fields[i] == null) fields[i] = "";
            if (emailData[i] == null) emailData[i] = "";
            if (fields[i] == EmailFields.Body) emailData[i] = EmailTextCleaner.CleanText(emailData[i]);
            data.Add(fields[i], emailData[i]);
        }
    }

    public string GetFieldData(string field)
    {
        string output;
        data.TryGetValue(field, out output);
        if (string.IsNullOrEmpty(output)) output = "ERROR";
        return output;
    }

    public string Get(string field) => GetFieldData(field);
    public void MarkAsRead() => markAsRead = true;
    public bool HasBeenRead() => markAsRead;
    public bool HasBeenEdited() => hasBeenEdited;
    public void MarkAsEdited() => hasBeenEdited = true;
    public void ReplaceBodyText(string newBodyText)
    {
        data.Remove(EmailFields.Body);
        data.Add(EmailFields.Body, newBodyText);
        MarkAsEdited();
    }
}
