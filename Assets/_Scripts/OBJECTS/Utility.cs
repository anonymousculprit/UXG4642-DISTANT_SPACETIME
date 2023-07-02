using System;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static bool ArrayIsNullOrEmpty(int[] array) => array != null || array.Length > 0 ? true : false;
    public static bool ArrayIsNullOrEmpty(string[] array) => array != null || array.Length > 0 ? true : false;
    public static bool IntMoreThanZero(int i) => i > 0 ? true : false;
    public static string[] ParseForStringArray(string cell, char delimiter)
    {
        string[] result = new string[0];
        if (string.IsNullOrEmpty(cell)) return result;
        if (!cell.Contains(delimiter)) return new string[] { cell };

        result = cell.Split(delimiter);
        foreach (string s in result) s.Trim();  // cleaning data
        return result;
    }
    public static int[] ParseForIntArray(string cell, char delimiter)
    {
        List<int> result = new();
        if (string.IsNullOrEmpty(cell)) return result.ToArray();
        if (!cell.Contains(delimiter)) return new int[] { int.Parse(cell) };

        string[] temp = cell.Split(delimiter);
        foreach (string s in temp)
        {
            s.Trim();  // cleaning data
            result.Add(int.Parse(s));
        }
        return result.ToArray();
    }
}