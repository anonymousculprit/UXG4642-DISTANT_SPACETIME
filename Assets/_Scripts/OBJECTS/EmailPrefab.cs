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
    public GameObject replyIconGO, unreadIconGO;
    public Image emailPanelImg, iconImg, outlineImg;
    public Sprite replyIcon;
    public ColorSet unread, selected, read;

    public string emailID { get; private set; }
    bool hasReply = false;

    public void Init(string id, string subject, string author, bool reply = false)
    {
        emailID = id;
        this.subject.text = subject;
        this.author.text = author;
        hasReply = reply;

        CheckIfEmailHasBeenRead();
        ShortenEmailDisplay(subject, author);
    }

    void ShortenEmailDisplay(string subject, string author)
    {
        if (subject.Length > subjectLineMaxCharacters) this.subject.text = subject.Substring(0, subjectLineMaxCharacters) + "...";
        if (author.Length > authorLineMaxCharacters) this.author.text = subject.Substring(0, authorLineMaxCharacters) + "...";
    }

    void CheckIfEmailHasBeenRead()
    {
        bool hasBeenRead = GameManager.emailDataManager.GetEmailByID(emailID).HasBeenRead();

        if (hasBeenRead)
        {
            replyIconGO.SetActive(false);
            unreadIconGO.SetActive(false);
            ChangeColor(read);
        }
        else
        {
            replyIconGO.SetActive(false);
            unreadIconGO.SetActive(true);
            ChangeColor(unread);
        }
    }

    public void OnClick()
    {
        if (selectedEmail != null) { selectedEmail.SetDeselected(); SFXManager.instance.Play("emailreader_diff"); }
        else { SFXManager.instance.Play("emailreader_new"); }
        selectedEmail = this;
        SetSelected();
        EmailReaderDisplay.instance.LoadReaderContents(emailID, hasReply);
    }

    public void SetSelected()
    {
        EmailReaderDisplay.instance.UnselectingEmail += SetDeselectedByEmailDisplay;
        ChangeColor(selected);

        if (unreadIconGO.activeInHierarchy) { unreadIconGO.SetActive(false); UnreadEmailDisplay.instance.DecreaseCounter(); }
        replyIconGO.SetActive(true);

        if (hasReply) iconImg.sprite = replyIcon;
        else replyIconGO.SetActive(false);
    }

    public void SetDeselected()
    {
        EmailReaderDisplay.instance.UnselectingEmail -= SetDeselectedByEmailDisplay;
        ChangeColor(read);
        
        if (hasReply && replyIconGO.activeInHierarchy)
            if (!EmailMatrix.PlayerHasRepliedToEmailID(emailID))
                iconImg.color = unread.iconColor;
    }

    public void SetDeselectedByEmailDisplay()
    {
        EmailReaderDisplay.instance.UnselectingEmail -= SetDeselectedByEmailDisplay;
        ChangeColor(read);
        hasReply = false;
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