using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsInterfacer : MonoBehaviour
{
    public GameObject menu;
    public void TurnOnMenu() => menu.SetActive(true);
    public void TurnOffMenu() => menu.SetActive(false);
}
