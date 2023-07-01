using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;

public class CSVReader
{
    public TextAssetParserConfig config { get; private set; }

    public CSVReader(TextAssetParserConfig config = null) => this.config = config != null ? config : TextAssetParserConfig.DefaultCSV;

    public string[] Read(string line)
    {
        return ParseLine(line);
    }

    private string[] ParseLine(string line)
    {
        Stack<string> result = new Stack<string>();

        int i = 0;
        while (true)
        {
            string cell = ParseNextCell(line, ref i);
            if (cell == null) break;
            result.Push(cell);
        }

        var resultAsArray = result.ToArray();
        Array.Reverse(resultAsArray);
        return resultAsArray;
    }

    // returns iterator after delimiter or after end of string
    private string ParseNextCell(string line, ref int i)
    {
        if (i >= line.Length) return null;

        if (line[i] != config.quotationMark)
            return ParseNotEscapedCell(line, ref i);
        else
            return ParseEscapedCell(line, ref i);
    }

    // returns iterator after delimiter or after end of string
    private string ParseNotEscapedCell(string line, ref int i)
    {
        StringBuilder sb = new StringBuilder();
        while (true)
        {
            if (i >= line.Length) // return iterator after end of string
                break;
            if (line[i] == config.delimiter)
            {
                i++; // return iterator after delimiter
                break;
            }
            sb.Append(line[i]);
            i++;
        }
        return sb.ToString();
    }

    // returns iterator after delimiter or after end of string
    private string ParseEscapedCell(string line, ref int i)
    {
        i++; // omit first character (quotation mark)
        StringBuilder sb = new StringBuilder();
        while (true)
        {
            if (i >= line.Length)
                break;
            if (line[i] == config.quotationMark)
            {
                i++; // we're more interested in the next character
                if (i >= line.Length)
                {
                    // quotation mark was closing cell;
                    // return iterator after end of string
                    break;
                }
                if (line[i] == config.delimiter)
                {
                    // quotation mark was closing cell;
                    // return iterator after delimiter
                    i++;
                    break;
                }
                if (line[i] == config.quotationMark)
                {
                    // it was doubled (escaped) quotation mark;
                    // do nothing -- we've already skipped first quotation mark
                }
            }
            sb.Append(line[i]);
            i++;
        }

        return sb.ToString();
    }
}
