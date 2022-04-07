using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using SomeLibrary;

namespace OddBall1
{
    class Program
    {

        static void Main(string[] args)
        {

        }

        private static void Version1()
        {
            string lines = "\"790844900493 \",\"20039-429      \",\"PRETO/ROYAL                   \",\"ENERGY" +
                           "                             \",\"21   \",\"1     \",\"BG0030B        " +
                           "\",\"631688   \",\"TENIS ENERGY                            \",\"   \",\"          \",\"14865724000102\",\"DOK                                               \"";

            var result = lines.RemoveAllWhiteSpace();

            Regex reg = new("\"([^\"]*?)\"");

            List<string> list = new();
            StringBuilder builder = new();

            var matches = reg.Matches(result);

            foreach (Match match in matches)
            {
                var theData = match.Groups[1].Value;
                list.Add(theData);
            }

            builder.AppendLine(string.Join(",", list));


            File.WriteAllText("TextFile.csv", builder.ToString().TruncateCommas());
        }

        private static void Version2()
        {
            List<string> lines = new()
            {
                "COD,CODINT,LAYOUT,CODBARRAS,REF,COR,BR,USA,EUR,LINHA,FICHA,FOTO,SEPARADOR",
                "\"1\",\"1\",\"F\",\"\",\"\",\"\",\"\",\"XX\",\"XX\",\"\",\"546903\",\"d:\\etiqueta\\Dok\\imagens\\.jpg\",\"0\"",
                "\"2\",\"2\",\"\",\"790826114473\",\"91528-1356\",\"ROYAL\",\"20/21\",\"XX\",\"XX\",\"20031FUL\",\"546903\",\"d:\\etiqueta\\Dok\\imagens\\915281356.jpg\",\"0\"\r\n"
            };

            Regex reg = new("\"([^\"]*?)\"");

            List<string> list = new();
            StringBuilder builder = new();


            for (int index = 0; index < lines.Count; index++)
            {
                if (lines[index].Contains("\""))
                {
                    var matches = reg.Matches(lines[index]);

                    foreach (Match match in matches)
                    {
                        var theData = match.Groups[1].Value;
                        list.Add(theData);
                    }

                    builder.AppendLine(string.Join(",", list));
                    list.Clear();
                }
                else
                {
                    builder.AppendLine(lines[index]);
                }
            }

            File.WriteAllText("TextFile.csv", builder.ToString().TruncateCommas());
        }
    }
}
