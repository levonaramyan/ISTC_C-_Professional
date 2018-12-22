using System;
using System.Net;
using HtmlAgilityPack;

// The aim of this program is to print in the console the code, available in given any Github Url

namespace Printing_Written_Code_From_Github_Url
{
    class Program
    {
        static void Main(string[] args)
        {
            string testUrl = @"https://github.com/VanHakobyan/ISTC_Coding_School/blob/master/ISTC.ThirdStage.Advance/ISTC.ThirdStage.Advance.WorkWithText.HTMLAP/Program.cs";
            Console.WriteLine(GrabTheCodeFromGithub(testUrl));
            Console.ReadKey();
        }

        // Returns the code, available in any github url
        public static string GrabTheCodeFromGithub(string url)
        {
            WebClient client = new WebClient();
            string content = client.DownloadString(url);
            HtmlDocument code = new HtmlDocument();
            code.LoadHtml(content);
            string innerCode = "";
            int line = 1;

            while (true)
            {
                try
                {
                    HtmlNode theInsideCode = code.DocumentNode.SelectSingleNode($"//*[@id=\"LC{line}\"]");
                    int validation = theInsideCode.Line;
                    innerCode += $"{theInsideCode.InnerText}\n";
                }
                catch (Exception) { break; }

                line++;
            }

            innerCode = innerCode.Replace("&quot;", "\"");

            return innerCode;
        }
    }
}
