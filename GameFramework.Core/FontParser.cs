using System;
using System.Collections.Generic;
using System.IO;

namespace GameFramework.Core
{
    public class FontParser
    {
        private static int _headerSize = 4;

        private static int GetValue(string s)
        {
            string value = s.Substring(s.IndexOf('=') + 1);
            return int.Parse(value);
        }

        public static Dictionary<char, CharacterData> ParseFile(string filePath)
        {
            var charDictionary = new Dictionary<char, CharacterData>();
            string[] lines = File.ReadAllLines(filePath);
            for (int i = _headerSize; i < lines.Length; i++)
            {
                string firstLine = lines[i];
                string[] typesAndValues = firstLine.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                var charData = new CharacterData()
                                   {
                                       Id = GetValue(typesAndValues[1]),
                                       X = GetValue(typesAndValues[2]),
                                       Y = GetValue(typesAndValues[3]),
                                       Width = GetValue(typesAndValues[4]),
                                       Height = GetValue(typesAndValues[5]),
                                       XOffset = GetValue(typesAndValues[6]),
                                       YOffset = GetValue(typesAndValues[7]),
                                       XAdvance = GetValue(typesAndValues[8])
                                   };

                charDictionary.Add((char) charData.Id, charData);
            }

            return charDictionary;
        }
    }
}