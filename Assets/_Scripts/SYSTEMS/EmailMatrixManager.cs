using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EmailMatrixManager
{
    string[] defaultFields = new string[] { "Day", "Response" };

    CSVDataRetriever matrixReader = new();

    public void Init(string dataPath = null, string[] emailFields = null, string dataFolder = null)
    {
        string[] fields = emailFields != null ? emailFields : defaultFields;
        string path = dataPath != null ? dataPath : Application.streamingAssetsPath + "/Data";
        if (dataFolder != null) path = Application.streamingAssetsPath + dataFolder;
        matrixReader.SetPath(path);
        matrixReader.CreateDBObjCollection(fields);
    }
}
