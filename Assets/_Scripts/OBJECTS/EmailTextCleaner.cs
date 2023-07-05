using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class EmailTextCleaner
{
    static List<CharPair> charChecker = new List<CharPair>()
    {
        new CharPair('’','\'')
    };

    public static string CleanText(string bodyText)
    {
        string s = bodyText;
        foreach (CharPair hostileCharacter in charChecker)
        {
            if (s.Contains(hostileCharacter.GetFind()))
                s = s.Replace(hostileCharacter.GetFind(), hostileCharacter.GetReplace());
        }
        return s;
    }
}

public struct CharPair
{
    char find;
    char replace;

    public CharPair(char find, char replace)
    {
        this.find = find;
        this.replace = replace;
    }

    public char GetFind() => find;
    public char GetReplace() => replace;
}
