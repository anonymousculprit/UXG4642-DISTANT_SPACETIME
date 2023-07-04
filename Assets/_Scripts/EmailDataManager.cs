using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmailDataManager 
{
    TXTDataRetriever emailReader = new();
    List<Email> emails = new();

    public void Init(string dataPath = null, string[] emailFields = null, string dataFolder = null)
    {
        string[] fields = emailFields != null ? emailFields : EmailFields.DefaultEmailFields;
        string path = dataPath != null ? dataPath : Application.dataPath + "/Data";
        if (dataFolder != null) path = Application.dataPath + dataFolder;
        emails.AddRange(emailReader.GetEmails(path, fields));
    }

    public List<Email> GetAllEmails() => emails;
    public Email GetEmailByID(string id) => emails.Find(x => x.Get(EmailFields.ID) == id);
    public List<Email> GetEmailsByField(string field, string value) => emails.FindAll(x => x.Get(field) == value);
}
