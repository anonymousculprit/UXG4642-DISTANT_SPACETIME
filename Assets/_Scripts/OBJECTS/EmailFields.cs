using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class EmailFields
{
    public static string ID = "ID";
    public static string Subject = "Subject";
    public static string Author = "Author";
    public static string Date = "Date";
    public static string Body = "Body";
    public static string PlayerName = "PlayerName";

    public static string[] DefaultEmailFields = new string[] { ID, Subject, Author, Date, Body };
}
