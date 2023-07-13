using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public struct EmailResponseReply
{
    public string npcEmail { get; private set; }
    public string playerReply { get; private set; }
    public string npcReply { get; private set; }
    public string[] requirements { get; private set; }
    public bool replied { get; private set; }

    public EmailResponseReply(string email, string response, string reply, string[] requirements = null)
    {
        this.npcEmail = email;
        this.playerReply = response;
        this.npcReply = reply;
        this.requirements = requirements;
        replied = false;
        //Debug.Log("email: " + email + " | requirements: " + (requirements == null ? "true" : "false"));
    }

    public bool HasRequirements() => requirements != null;
    public void PlayerHasReplied() => replied = true;
}
