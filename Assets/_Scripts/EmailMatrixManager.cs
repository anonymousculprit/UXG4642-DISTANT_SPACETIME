using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EmailMatrixManager
{
    public static Dictionary<int, string[]> DayToEmailRegistry = new();

    public static void RegisterDayEmailMatrix(int day, string[] emailID) => DayToEmailRegistry.Add(day, emailID);
}