using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PDFReader
{
    public static class ListOfLinesExtension
    {
        public static Line GetLeftLine(this List<Line> list, Line line)
        {
            Line result = null;

            int id = list.IndexOf(line);
            if (id > 0)
                result = list[id - 1];

            return result;
        }

        public static Line GetTopLine(this List<Line> list, Line line)
        {
            var result = list.Where(x => (x.BeginX == line.BeginX) && (x.BeginY < line.BeginY)).FirstOrDefault();

            return result;
        }
    }
}
