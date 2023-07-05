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

    public void SortAndCleanDayEmailInput(string[] emailsSortedByPerson)
    {
        for (int i = 1; i < emailsSortedByPerson.Length; i++)
        {
            string s = emailsSortedByPerson[i];
            if (string.IsNullOrEmpty(s)) continue;

            // check for multiple emails on same day from same author.
            string[] idArray = new string[0];
            if (s.Contains(','))
            {
                idArray = s.Split(',');
                for (int j = 0; j < idArray.Length; j++)
                    idArray[j] = idArray[j].Trim();
            }

            // if there are multiple emails on same day from same author, copy all of them into respective folders.
            if (idArray.Length > 0)
            {
                foreach (string id in idArray)
                    switch (i)
                    {
                        case 1: day1.Add(id); break;
                        case 2: day2.Add(id); break;
                        case 3: day3.Add(id); break;
                        case 4: day4.Add(id); break;
                        case 5: day5.Add(id); break;
                        case 6: day6.Add(id); break;
                        case 7: day7.Add(id); break;
                        default: misc.Add(id); break;
                    }
            }
            // handle case for single email from author otherwise.
            else
            {
                s = s.Trim();
                switch (i)
                {
                    case 1: day1.Add(s); break;
                    case 2: day2.Add(s); break;
                    case 3: day3.Add(s); break;
                    case 4: day4.Add(s); break;
                    case 5: day5.Add(s); break;
                    case 6: day6.Add(s); break;
                    case 7: day7.Add(s); break;
                    default: misc.Add(s); break;
                }
            }
        }
    }

    public void SubmitDayEmailInputToMatrix()
    {
        if (day1.Count > 0) EmailMatrix.RegisterDayEmailMatrix(1, day1.ToArray());
        if (day2.Count > 0) EmailMatrix.RegisterDayEmailMatrix(2, day2.ToArray());
        if (day3.Count > 0) EmailMatrix.RegisterDayEmailMatrix(3, day3.ToArray());
        if (day4.Count > 0) EmailMatrix.RegisterDayEmailMatrix(4, day4.ToArray());
        if (day5.Count > 0) EmailMatrix.RegisterDayEmailMatrix(5, day5.ToArray());
        if (day6.Count > 0) EmailMatrix.RegisterDayEmailMatrix(6, day6.ToArray());
        if (day7.Count > 0) EmailMatrix.RegisterDayEmailMatrix(7, day7.ToArray());
    }

    public void CleanEmailResponseInput(string[] emailResponseReply)
    {
        for (int i = 0; i < emailResponseReply.Length; i++) emailResponseReply[i] = emailResponseReply[i].Trim();
        EmailMatrix.RegisterResponseToEmailRegistry(
            new EmailResponseReply(
                email: emailResponseReply[1],
                response: emailResponseReply[2],
                reply: emailResponseReply.Length < 4 ? "" : emailResponseReply[3]
            ));
    }
}