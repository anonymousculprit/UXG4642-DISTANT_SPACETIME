using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUG_Emails : MonoBehaviour
{
    public string[] emailFields = new string[] { "ID", "Subject", "Author", "Date", "Body" };
    public string emailPath = "";
    EmailDataManager emailDataManager = new();
    EmailMatrixManager emailMatrixManager = new();

    private void Awake()
    {
        emailDataManager.Init();
        emailMatrixManager.Init();
    }

    public void Start()
    {
        TestMatrixFetching();
    }

    public void TestMatrixFetching()
    {
        Debug.Log("==================================== EMAIL DAY MATRIX ====================================");

        for (int i = 1; i < 8; i++)
        {
            Debug.Log("DAY" + i);
            string[] ph = EmailMatrix.GetEmailsByDay(i);
            if (ph != null)
            foreach (string s in ph)
                Debug.Log(s);
        }

        Debug.Log("==================================== EMAIL RESPONSE MATRIX ====================================");

        foreach (EmailResponseReply set in EmailMatrix.ResponseToEmailRegistry)
        {
            Debug.Log(set.npcEmail + ", " + set.playerReply + ", " + set.npcReply);
        }
    }

    public void TestEmailFetching()
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
