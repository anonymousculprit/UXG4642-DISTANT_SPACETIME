using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmailDataManager 
{
    string[] defaultFields = new string[] { "ID", "Subject", "Author", "Date", "Body" };

    EmailDataRetriever emailReader = new();
    List<Email> emails;

    public void Init(string dataPath = null, string[] emailFields = null)
    {
        string[] fields = emailFields != null ? emailFields : defaultFields;
        string path = dataPath != null ? dataPath : Application.dataPath + "/Data";
        emails = emailReader.GetEmails(path, fields);
    }

    public List<Email> GetAllEmails() => emails;
    public Email GetEmailByID(string id) => emails.Find(x => x.Get("ID") == id);
    public List<Email> GetEmailsByField(string field, string value) => emails.FindAll(x => x.Get(field) == value);
    public string[] GetDefaultFields() => defaultFields;
}
