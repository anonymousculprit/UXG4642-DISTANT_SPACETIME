using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DistanceDisplay : MonoBehaviour
{
    public TextMeshProUGUI distanceText;
    public string[] distanceTextPerDay;
    public float chanceOfFakeNumber = 0.3f;
    string[] defaultFakeNumbers = new string[] { "<i>i</i> LY", "undefined LY", "ERROR", "Can't Find Earth!", ":(", "???" };
    

    private void Start()
    {
        int i = GameManager.instance.GetDay();

        if (BeyondStoryTimeframe(i)) UsePresetNumbers(i);
        else if (i > 100)
        {
            if (Random.Range(0, 1f) >= chanceOfFakeNumber)
            {
                System.Random rand = new System.Random();
                int j = rand.Next();
                distanceText.text = j.ToString() + "..." + "\n" + "LY";
            }
            else
            {
                distanceText.text = defaultFakeNumbers[Random.Range(0, defaultFakeNumbers.Length - 1)];
            }
        }
        else
        {
            if (Random.Range(0, 1f) >= chanceOfFakeNumber)
            {
                distanceText.text = (i - distanceTextPerDay.Length).ToString() + " LY";
            }
            else
            {
                distanceText.text = defaultFakeNumbers[Random.Range(0, defaultFakeNumbers.Length - 1)];
            }
        }
    }

    bool BeyondStoryTimeframe(int i) => i <= distanceTextPerDay.Length;
    void UsePresetNumbers(int i) { distanceText.text = distanceTextPerDay[i - 1]; }
}
