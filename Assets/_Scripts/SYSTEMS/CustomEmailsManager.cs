using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class CustomEmailsManager : MonoBehaviour
{
    public static CustomEmailsManager instance;

    public TMP_InputField playerTextInput;

    EmailAppender emailAppender = new();
    List<string> customEmailIDs = new();
    bool active = false;
    int increment = 1;

    private void Awake() => instance = this;
    private void Start() => active = GameManager.instance.GetDay() == 7; 

    public void SaveEmail()
    {
        if (!active) return;
        string currentEmailID = EmailReaderDisplay.instance.currentEmailID;
        string bodyText = RegisterToEmailAppender(currentEmailID);
        string customID = MakeEmail(currentEmailID, bodyText);
        AddEmailToMatrix(currentEmailID, customID);
        customEmailIDs.Add(customID);
        // TODO: Save Emails as TXT Files to StreamingAssets or PersistentDataPath somewhere.
    }

    string RegisterToEmailAppender(string currentEmailID)
    {
        string playerText = playerTextInput.text;
        emailAppender.AppendEmailForCustomEmailsManager(currentEmailID, playerText);
        return playerText;
    }

    string MakeEmail(string currentEmailID, string result)
    {
        Email initialEmail = GameManager.emailDataManager.GetEmailByID(currentEmailID);
        string todaysDate = DateTime.Today.Day.ToString() + "D"+ DateTime.Today.Month.ToString() + "M" + DateTime.Today.Year.ToString() +"Y";
        string customID = "CUSTOMREPLY_" + todaysDate + "_" + increment;

        string[] newEmailData = new string[]
        {
            customID,
            "RE: " + initialEmail.Get(EmailFields.Subject),
            "Dylon Smith",
            initialEmail.Get(EmailFields.Date),
            result
        };

        Email newEmail = new Email(EmailFields.DefaultEmailFields, newEmailData);

        GameManager.emailDataManager.AddEmail(newEmail);
        increment++;
        return customID;
    }

    void AddEmailToMatrix(string currentEmailID, string customID)
    {
        EmailResponseReply newMatrixEntry = new EmailResponseReply(currentEmailID, customID, null);
        newMatrixEntry.PlayerHasReplied();
        EmailMatrix.RegisterResponseToEmailRegistry(newMatrixEntry);
    }

    public void RegisterDayEmailsToMatrix() => EmailMatrix.RegisterDayEmailMatrix(8, customEmailIDs.ToArray());
}
