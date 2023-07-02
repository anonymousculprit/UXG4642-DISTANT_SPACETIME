using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InboxLoader : MonoBehaviour
{
    public GameObject emailPrefab, emailContainer;

    public void OnEnable()
    {
        GameManager.instance.InitComplete += LoadInbox;
    }

    public void OnDisable()
    {
        GameManager.instance.InitComplete -= LoadInbox;
    }

    void LoadInbox()
    {
        List<Email> emails = EmailGrabber.instance.GetInbox();
        foreach(Email email in emails)
        {
            GameObject newEmailGO = Instantiate(emailPrefab);
            newEmailGO.transform.SetParent(emailContainer.transform);

            string id = email.GetFieldData(EmailFields.ID);
            EmailPrefab newEmailClass = newEmailGO.GetComponent<EmailPrefab>();
            newEmailClass.Init(
                id: id,
                subject: email.GetFieldData(EmailFields.Subject),
                author: email.GetFieldData(EmailFields.Author),
                reply: EmailMatrix.EmailIDHasPlayerReply(id)
                );
        }
    }
}
