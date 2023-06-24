using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUG_Emails : MonoBehaviour
{
    public string[] emailFields = new string[] { "ID", "Subject", "Author", "Date", "Body" };
    public string emailPath = "";
    EmailDataManager emailDataManager = new();

    private void Awake()
    {
        emailDataManager.Init();
    }

    public void Start()
    {
        foreach (Email email in emailDataManager.GetAllEmails())
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
