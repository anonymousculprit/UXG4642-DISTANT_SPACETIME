using System;
using System.Collections.Generic;
using UnityEngine;

public class TextAssetParserConfig
{
    public string fileType { get; private set; }
    public char delimiter { get; private set; }
    public string newLineMark { get; private set; }
    public char quotationMark { get; private set; }
    public string path { get; private set; }

    public static TextAssetParserConfig DefaultCSV => new TextAssetParserConfig("*.csv", ',', "\r\n", '\"');
    public static TextAssetParserConfig DefaultTXT => new TextAssetParserConfig("*.txt", ',', "\r\n", '\"');

    public void SetPath(string path) => this.path = path;

    public TextAssetParserConfig(string fileType, char delimiter, string newLineMark, char quotationMark, string path = null)
    {
        this.fileType = fileType;
        this.delimiter = delimiter;
        this.newLineMark = newLineMark;
        this.quotationMark = quotationMark;
        this.path = path;
    }
}
