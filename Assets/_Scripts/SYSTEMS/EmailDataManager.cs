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
        CheckForIDDuplicates();
    }

    public List<Email> GetAllEmails() => emails;
    public Email GetEmailByID(string id) => emails.Find(x => x.Get(EmailFields.ID) == id);
    public List<Email> GetEmailsByField(string field, string value) => emails.FindAll(x => x.Get(field) == value);
    public void CheckForIDDuplicates()
    {
        int errorsFound = 0;
        for (int i = 0; i < emails.Count; i++)
        {
            string id = emails[i].Get(EmailFields.ID);
            for (int j = i + 1; j < emails.Count; j++)
            {
                string jd = emails[j].Get(EmailFields.ID);
                if (jd != id) continue;
                else 
                { 
                    Debug.LogError("Email ID Conflict found! | " + id + " & " + jd);
                    errorsFound++;
                }
            }
        }

        if (errorsFound == 0) Debug.Log("No Email ID Conflicts found!");
        else Debug.Log("Total Errors Found: " + errorsFound);

    }
}
