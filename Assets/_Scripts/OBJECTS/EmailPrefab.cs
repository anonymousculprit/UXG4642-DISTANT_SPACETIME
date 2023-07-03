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

    [Header("Text Formatting")]
    public int subjectLineMaxCharacters = 30;
    public int authorLineMaxCharacters = 35;
    
    [Header("Obj References")]
    public TextMeshProUGUI subject;
    public TextMeshProUGUI author;
    public GameObject iconGO;
    public Image emailPanelImg, iconImg, outlineImg;
    public Sprite replyIcon, unreadIcon;
    public ColorSet unread, selected, read;

    public string emailID { get; private set; }
    bool hasReply = false;

    public void Init(string id, string subject, string author, bool reply = false)
    {
        emailID = id;
        this.subject.text = subject;
        this.author.text = author;
        hasReply = reply;
        iconGO.SetActive(true);
        iconImg.sprite = unreadIcon;

        if (subject.Length > subjectLineMaxCharacters)
            this.subject.text = subject.Substring(0, subjectLineMaxCharacters) + "...";
        if (author.Length > authorLineMaxCharacters)
            this.author.text = subject.Substring(0, authorLineMaxCharacters) + "...";

        ChangeColor(GameManager.instance.emailDataManager.GetEmailByID(emailID).HasBeenRead() ? read : unread);
    }

    public void OnClick()
    {
        if (selectedEmail != null)
            selectedEmail.SetDeselected();
        selectedEmail = this;
        SetSelected();
        EmailDisplay.instance.LoadReaderContents(emailID);
    }

    public void SetSelected()
    {
        EmailDisplay.instance.UnselectingEmail += SetDeselected;
        ChangeColor(selected);

        if (hasReply)
            iconImg.sprite = replyIcon;
        else
            iconGO.SetActive(false);

        if (EmailMatrix.PlayerHasRepliedToEmailID(emailID))
            iconImg.color = read.iconColor;
    }

    public void SetDeselected()
    {
        EmailDisplay.instance.UnselectingEmail -= SetDeselected;
        ChangeColor(read);

        if (iconGO.activeInHierarchy)
            if (!EmailMatrix.PlayerHasRepliedToEmailID(emailID))
                iconImg.color = unread.iconColor;
    }

    public void ChangeColor(ColorSet set)
    {
        subject.color = set.subjectColor;
        author.color = set.authorColor;
        emailPanelImg.color = set.panelColor;
        iconImg.color = set.iconColor;
        outlineImg.color = set.outlineColor;
    }
}

[Serializable]
public struct ColorSet
{
    public Color panelColor, iconColor, subjectColor, authorColor, outlineColor;
}