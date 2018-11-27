using System;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using iTextSharp.text;
using System.Collections.Generic;

namespace PDFReader
{
    class Program
    {
        static string path = @"D:\Temp\SN.pdf";

        static void Main(string[] args)
        {

  
            int PageNumber = 1;

            var listOfLines = FindRectangles(path, PageNumber);

            //var regTextFilter = new RegionTextRenderFilter();
            //, new RegionTextRenderFilter(rect2), new RegionTextRenderFilter(rect3) };

            using (PdfReader reader = new PdfReader(path))
            {
                StringBuilder sb = new StringBuilder();


                string text = "";
                string text1 = "";
                Rectangle rect;
                Line leftLine;
                float BeginX = 0;

                foreach (Line line in listOfLines)
                {
                    if (line.BeginX == line.EndX)
                        continue;

                    rect = new Rectangle(line.BeginX, line.BeginY, line.EndX - 1, line.EndY + 10);

                    text = GetStringFromFile(reader, rect, PageNumber);

                    leftLine = listOfLines.GetLeftLine(line);
                    BeginX = 0;
                    if (leftLine != null)
                        if (leftLine.BeginY == line.BeginY)
                            BeginX = leftLine.EndX + 1;

                    rect = new Rectangle(BeginX, line.BeginY, line.BeginX - 1, line.EndY + 10);

                    text1 = GetStringFromFile(reader, rect, PageNumber);
                    sb.AppendLine($"{text1}: {text}");

                };
                


                Console.WriteLine(sb.ToString());
            }

            
            Console.ReadKey();
            }

        private static string GetStringFromFile(PdfReader reader, Rectangle rect, int pageNumber)
        {
            ITextExtractionStrategy strategy;

            RenderFilter[] filter = { new RegionTextRenderFilter(rect) };

            strategy = new FilteredTextRenderListener(new LocationTextExtractionStrategy(), filter);
            string result = PdfTextExtractor.GetTextFromPage(reader, pageNumber, strategy).Replace((char)10, ' ');
            return result;
        }

        private static List<Line> FindRectangles(string sourceFile, int pageNumber)
        {
            //Source file to read from
            
            var listOfLines = new List<Line>();
            

            //Bind a reader to our PDF
            using (PdfReader reader = new PdfReader(sourceFile))
            {

                //Create our buffer for previous token values. For Java users, List<string> is a generic list, probably most similar to an ArrayList
                List<string> buf = new List<string>();
                
                //Get the raw bytes for the page
                byte[] pageBytes = reader.GetPageContent(pageNumber);
                //Get the raw tokens from the bytes

                PRTokeniser tokeniser = new PRTokeniser(new RandomAccessFileOrArray(pageBytes));

                //Create some variables to set later
                PRTokeniser.TokType tokenType;
                string tokenValue;

                int countOfLines = 0;
                var AllowDecimalPoint = System.Globalization.NumberStyles.AllowDecimalPoint;

                //Loop through each token
                while (tokeniser.NextToken())
                {
                    //Get the types and value
                    tokenType = tokeniser.TokenType;
                    tokenValue = tokeniser.StringValue;
                    //If the type is a numeric type
                    if (tokenType == PRTokeniser.TokType.NUMBER)
                    {
                        //Store it in our buffer for later user
                        buf.Add(tokenValue);
                        //Otherwise we only care about raw commands which are categorized as "OTHER"
                    }
                    else if (tokenType == PRTokeniser.TokType.OTHER)
                    {
                        //Look for a rectangle token
                        //if (tokenValue == "re")
                        if (tokenValue == "l")
                        {
                            //Sanity check, make sure we have enough items in the buffer
                            if (buf.Count < 2) throw new Exception("Not enough elements in buffer for a rectangle");
                            countOfLines += 1;
                            //Read and convert the values
                            float x2 = float.Parse(buf[buf.Count - 2], AllowDecimalPoint);
                            float y2 = float.Parse(buf[buf.Count - 1], AllowDecimalPoint);
                            float x1 = float.Parse(buf[buf.Count - 4], AllowDecimalPoint);
                            float y1 = float.Parse(buf[buf.Count - 3], AllowDecimalPoint);
                            //Console.WriteLine($"{countOfLines} : ({x1}, {y1}) - ({x2}, {y2})");

                            listOfLines.Add(new Line() { BeginX = x1, BeginY = y1, EndX = x2, EndY = y2 });
                            //..do something with them here
                        }
                    }
                }
            }

            listOfLines.Sort();

            
            //foreach (Line line in listOfLines)
            //{
            //    countOfLines += 1;
            //    Console.WriteLine($"{countOfLines}: {line}");

            //}

            return listOfLines;
        }
        /*ITextExtractionStrategy its = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();

using (PdfReader reader = new PdfReader(path))
{
   StringBuilder text = new StringBuilder();

   for (int i = 1; i <= reader.NumberOfPages; i++)
   {
       string thePage = PdfTextExtractor.GetTextFromPage(reader, i, its);
       string[] theLines = thePage.Split('\n');
       foreach (var theLine in theLines)
       {
           text.AppendLine(theLine);
       }
   }
   Console.WriteLine(text.ToString());
   Console.ReadKey();
}*/
    }
    
}
