using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using ColorCodeStandard.Common;
using Xunit.Extensions;

namespace ColorCode
{
    public class ColorizeData : DataAttribute
    {
        readonly Regex sourceFileRegex = new Regex(@"(?i)[a-z]+\.source\.([a-z0-9]+)", RegexOptions.Compiled);

        public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
        {
            List<object[]> colorizeData = new List<object[]>();

            string appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            
            string[] dirNames = Directory.GetDirectories(Path.Combine(appPath, @"..\..\Data"));

            foreach(string dirName in dirNames)
            {
                string[] sourceFileNames = Directory.GetFiles(dirName, "*.source.*");

                foreach(string sourceFileName in sourceFileNames)
                {
                    Match sourceFileMatch = sourceFileRegex.Match(sourceFileName);

                    if (sourceFileMatch.Success)
                    {
                        string fileExtension = sourceFileMatch.Groups[1].Captures[0].Value;
                        string languageId = GetLanguageId(fileExtension);
                        
                        string expectedFileName = sourceFileName.Replace(".source.", ".expected.").Replace("." + fileExtension, ".html");
                        
                        colorizeData.Add(new object[] {languageId, sourceFileName, expectedFileName});
                    }
                }
            }

            return colorizeData;
        }

        private static string GetLanguageId(string fileExtension)
        {
            switch (fileExtension)
            {
                case "asax":
                    return LanguageId.Asax;
                case "ashx":
                    return LanguageId.Ashx;
                case "cs":
                    return LanguageId.CSharp;
                case "php":
                    return LanguageId.Php;
                case "css":
                    return LanguageId.Css;
                case "vb":
                    return LanguageId.VbDotNet;
                case "sql":
                    return LanguageId.Sql;
                case "xml":
                    return LanguageId.Xml;
                case "ps1":
                    return LanguageId.PowerShell;
                default:
                    throw new ArgumentException(string.Format("Unexpected file extension: {0}.", fileExtension));
            }
        }
    }
}