using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTSCRIPT : MonoBehaviour
{
    public int testIntVar;
    public string testStringVar;

    public void Start()
    {
        testStringVar = testIntVar.ToString();
        testStringVar += " | Hello!";
        testStringVar += " | Equilibrium.";
    }
}
