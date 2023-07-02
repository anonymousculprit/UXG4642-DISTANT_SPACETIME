using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EmailPrefab : MonoBehaviour
{
    public static EmailPrefab selectedEmail;

    public TextMeshProUGUI subject;
    public TextMeshProUGUI author;
    public GameObject replyIndicator;
    public Image emailPanelImg, replyIndicatorImg;
    public ColorSet unread, selected, read;

    public string emailID { get; private set; }

    public void Init(string id, string subject, string author, bool reply = false)
    {
        emailID = id;
        this.subject.text = subject;
        this.author.text = author;
        replyIndicator.SetActive(reply);

        ChangeColor(GameManager.instance.emailDataManager.GetEmailByID(emailID).HasBeenRead() ? read : unread);
    }

    public void OnClick()
    {
        selectedEmail.SetDeselected();
        selectedEmail = this;
        EmailDisplay.instance.LoadReaderContents(emailID);
        SetSelected();
    }

    public void SetSelected()
    {
        EmailDisplay.instance.UnselectingEmail += SetDeselected;
        ChangeColor(selected);

        if (EmailMatrix.PlayerHasRepliedToEmailID(emailID))
            replyIndicatorImg.color = read.replyColor;
    }

    public void SetDeselected()
    {
        EmailDisplay.instance.UnselectingEmail -= SetDeselected;
        ChangeColor(read);

        if (!EmailMatrix.PlayerHasRepliedToEmailID(emailID))
            replyIndicatorImg.color = unread.replyColor;
    }

    public void ChangeColor(ColorSet set)
    {
        subject.color = set.subjectColor;
        author.color = set.authorColor;
        emailPanelImg.color = set.panelColor;
        replyIndicatorImg.color = set.replyColor;
    }
}

[Serializable]
public struct ColorSet
{
    public Color panelColor, replyColor, subjectColor, authorColor;
}