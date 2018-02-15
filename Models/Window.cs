using System;
using System.Collections.Generic;
using System.Text;
using ConsoleFrontend.Helpers;
using System.Linq;

namespace ConsoleFrontend.Models
{
    public class Window : BaseControl
    {
        public string UpperLeftBorder = "╔";
        public string UpperRightBorder = "╗";
        public string LowerLeftBorder = "╚";
        public string LowerRightBorder = "╝";
        public string HorizontalLine = "═";
        public string VerticalLine = "║";
        public string LeftIntersection = "╟";
        public string RightIntersection = "╢";
        public string Cross = "╬";

        public override int TotalWidth => ContentWidth + 2 * BorderWidth;
        public override int TotalHeight => ContentHeight + 2 * BorderHeight;

        protected IControlContent Content {get; set;}

        public override string[] Render(int? overrideWidth, int? overrideHeight)
        {
            var builder = new List<string>();
            
            // Header
            //              ┌   ─   ┤   _   $NAME           _   ├   ─   ┐
            int minLength = 1 + 1 + 1 + 1 + Name.Length + 1 + 1 + 1 + 1;
            int headerSpaces = Math.Max(0, TotalWidth - minLength);
            string header = UpperLeftBorder + HorizontalLine + RightIntersection + " " + Name + " " + LeftIntersection + (new String(HorizontalLine.First(), headerSpaces)) + HorizontalLine + UpperRightBorder;
            builder.Add(header);
            
            // Content
            //
            var content = Content.Render(ContentWidth).SelectMany(x => x.Split(Environment.NewLine, StringSplitOptions.None));
            foreach(var line in content)
            {
                string l = VerticalLine + " " + line.PadRight(ContentWidth) + " " + VerticalLine;
                builder.Add(l);
            }

            // Footer
            //
            string footer = LowerLeftBorder + (new String(HorizontalLine.First(), TotalWidth - 2)) + LowerRightBorder;
            builder.Add(footer);

            return builder.ToArray();
        }
    }
}
