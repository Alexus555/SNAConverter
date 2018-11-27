using System;
using System.Collections.Generic;
using System.Text;

namespace PDFReader
{
    public class Line : IComparable
    {
        public float BeginX { get; set; }
        public float BeginY { get; set; }
        public float EndX { get; set; }
        public float EndY { get; set; }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;

            var objLine = obj as Line;

            if (objLine == null)
                return 1;

            if (objLine.BeginY == this.BeginY)
            {

                if (objLine.BeginX == this.BeginX)
                    return 0;

                if (objLine.BeginX > this.BeginX)
                    return -1;
                else
                    return 1;
            }
                

            if (objLine.BeginY < this.BeginY)
                return -1;
            else
                return 1;
        }

        public override string ToString()
        {
            return $"({BeginX}, {BeginY}) - ({EndX}, {EndY})";
        }
    }
}
