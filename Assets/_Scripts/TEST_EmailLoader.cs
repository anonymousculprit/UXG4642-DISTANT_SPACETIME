using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_EmailLoader : MonoBehaviour
{
    public string[] emailFields = new string[] { "ID", "Subject", "Author", "Date", "Body" };

    EmailDataRetriever emailReader = new();
    public List<Email> emails;

    public void Awake()
    {
        emails = emailReader.GetEmails(Application.dataPath + "/Data", emailFields);
    }

    public void Start()
    {
        foreach (Email email in emails)
        {
            string headers = "";
            string body = "";
            foreach (string field in emailFields)
            {
                if (field == "Body") body = email.Get("Body");
                else
                {
                    if (headers != "") headers += " | "; 
                    headers += email.Get(field);
                }
            }
            Debug.Log(headers);
            Debug.Log(body);
        }
    }
}
