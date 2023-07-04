using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class CSVDataRetriever
{
    CSVReader csvReader = new();

    public void SetPath(string path) { csvReader.config.SetPath(path); }
    public void CreateDBObjCollection(string[] dbObjs)
    {
        CSVDataCleaner cleaner = new();
        foreach (string s in dbObjs)
        {
            IEnumerable<string[]> db = RetrieveDataFromDB(s);
            if (db == null) { Debug.LogError(s + "DB is missing from Data!"); continue; }
            switch (s)
            {
                case "Day":
                    foreach (string[] data in db) cleaner.SortAndCleanDayEmailInput(data);
                    cleaner.SubmitDayEmailInputToMatrix();
                    break;
                case "Response":
                    foreach (string[] data in db) cleaner.CleanEmailResponseInput(data);
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
            if (!currentFile.ToLower().Contains(dbObj.ToLower())) continue;

            string currFilePath = currentFile;
            var fileStream = new FileStream(currFilePath, FileMode.Open, FileAccess.Read);

            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string instructions = streamReader.ReadLine();
                string header = streamReader.ReadLine();
                string sample = streamReader.ReadLine();
                string line;
                while ((line = streamReader.ReadLine()) != null) result.Add(csvReader.Read(line));
            }
        }
        return result;
    }
}