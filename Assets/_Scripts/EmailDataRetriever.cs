using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class EmailDataRetriever 
{
    public List<Email> GetEmails(string path, string[] fields)
    {
        int body = 0;
        string[] filterFields = MakeFilterFields(fields, out body);

        List<Email> emails = new List<Email>();

        var txtFiles = Directory.EnumerateFiles(path, "*.txt");
        foreach (string currentFile in txtFiles)
        {
            string currFilePath = currentFile;
            var fileStream = new FileStream(currFilePath, FileMode.Open, FileAccess.Read);

            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                string[] emailInfo = new string[filterFields.Length];

                while ((line = streamReader.ReadLine()) != null)
                {
                    if (line.Length == 0) continue;
                    if (RetrievedFieldInfo(line, ref emailInfo)) continue;
                    else AddTextToBody(line, ref emailInfo);
                }

                emails.Add(new Email(fields, emailInfo));
            }
        }

        return emails;

        string[] MakeFilterFields(string[] fields, out int body)
        {
            string[] fieldsCopy = (string[])fields.Clone();
            int bodyIndex = fields.Length;
            for (int i = 0; i < fields.Length; i++)
            {
                if (!fields[i].Contains(':')) fieldsCopy[i] += ':';
                if (fields[i].Contains("Body")) bodyIndex = i;
            }

            body = bodyIndex;
            return fieldsCopy;
        }
        bool RetrievedFieldInfo(string line, ref string[] emailInfo)
        {
            for (int i = 0; i < filterFields.Length; i++)
                if (line.Contains(filterFields[i]))
                {
                    line = line.Remove(0, filterFields[i].Length + 1);
                    emailInfo[i] = line;
                    return true;
                }
            return false;
        }
        void AddTextToBody(string line, ref string[] emailInfo)
        {
            if (emailInfo[body] != null)
                emailInfo[body] += "\n";

            emailInfo[body] += line;
        }

    }
}
