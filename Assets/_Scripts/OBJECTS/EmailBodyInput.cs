using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class EmailBodyInput : MonoBehaviour
{
    public TextMeshProUGUI playerText, templateText;
    public GameObject submitButton;

    private void OnEnable() => StartCoroutine(CheckIfTextMatches());
    private void OnDisable() => StopAllCoroutines();

    IEnumerator CheckIfTextMatches()
    {
        while (true)
        {
            if (playerText.text == templateText.text)
                submitButton.SetActive(true);
            else
                submitButton.SetActive(false);
            yield return new WaitForSeconds(1f);
        }
    }

}
