using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class CSVDataRetriever
{
    CSVReader csvReader = new();

    public void SetPath(string path) => csvReader.config.SetPath(path);
    public void CreateDBObjCollection(string[] dbObjs)
    {
        foreach(string s in dbObjs)
        {
            IEnumerable<string[]> db = RetrieveDataFromDB(s);
            if (db == null) { Debug.LogError(s + "DB is missing from Data!"); continue; }
            switch (s)
            {
                case "Matrix":
                    foreach (string[] data in db) DB.enhancements.Add(new DBObj_Enhancement(data, csvReader.config.delimiter));
                    break;
                case "Responses":
                    foreach (string[] data in db) DB.skills.Add(new DBObj_Skill(data, csvReader.config.delimiter));
                    break;
                default: Debug.LogError(s + "DB is not registered in CSVDataRetriever!"); break;
            }
        }
    }

    public IList<string[]> RetrieveDataFromDB(string dbObj)
    {
        var txtFiles = Directory.EnumerateFiles(csvReader.config.path, "*.csv");
        List<string[]> result = new();
        foreach (string currentFile in txtFiles)
        {
            if (!currentFile.Contains(dbObj.ToLower())) continue;

            string currFilePath = currentFile;
            var fileStream = new FileStream(currFilePath, FileMode.Open, FileAccess.Read);

            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string header = streamReader.ReadLine();
                string line;
                while ((line = streamReader.ReadLine()) != null) result.Add(csvReader.Read(line));
            }
        }
        return result;
    }
}