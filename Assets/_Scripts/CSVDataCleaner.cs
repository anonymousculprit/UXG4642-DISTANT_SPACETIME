using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CSVDataCleaner
{
    List<string> day1 = new();
    List<string> day2 = new();
    List<string> day3 = new();
    List<string> day4 = new();
    List<string> day5 = new();
    List<string> day6 = new();
    List<string> day7 = new();
    List<string> misc = new();

    public void SortDayEmailInput(string[] emailsSortedByPerson)
    {
        for (int i = 0; i < 7; i++)
        {
            string s = emailsSortedByPerson[i];
            if (!string.IsNullOrEmpty(s))
                s = s.Trim();
                switch (i)
                {
                    case 0: day1.Add(s); break;
                    case 1: day2.Add(s); break;
                    case 2: day3.Add(s); break;
                    case 3: day4.Add(s); break;
                    case 4: day5.Add(s); break;
                    case 5: day6.Add(s); break;
                    case 6: day7.Add(s); break;
                    default: misc.Add(s); break;
                }
        }
    }

    public void SubmitDayEmailInputToMatrix()
    {
        if (day1.Count > 0) EmailMatrixManager.RegisterDayEmailMatrix(1, day1.ToArray());
        if (day2.Count > 0) EmailMatrixManager.RegisterDayEmailMatrix(2, day2.ToArray());
        if (day3.Count > 0) EmailMatrixManager.RegisterDayEmailMatrix(3, day3.ToArray());
        if (day4.Count > 0) EmailMatrixManager.RegisterDayEmailMatrix(4, day4.ToArray());
        if (day5.Count > 0) EmailMatrixManager.RegisterDayEmailMatrix(5, day5.ToArray());
        if (day6.Count > 0) EmailMatrixManager.RegisterDayEmailMatrix(6, day6.ToArray());
        if (day7.Count > 0) EmailMatrixManager.RegisterDayEmailMatrix(7, day7.ToArray());
    }
}