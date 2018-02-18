using System;
using System.Collections.Generic;
using System.Text;
using ConsoleFrontend.Helpers;
using System.Linq;

namespace ConsoleFrontend.Models
{
    public class Window 
    {
        public string UpperLeftBorder   { get; set; } = "╔";
        public string UpperRightBorder  { get; set; } = "╗";
        public string LowerLeftBorder   { get; set; } = "╚";
        public string LowerRightBorder  { get; set; } = "╝";
        public string HorizontalLine    { get; set; } = "═";
        public string VerticalLine      { get; set; } = "║";
        public string LeftIntersection  { get; set; } = "╟";
        public string RightIntersection { get; set; } = "╢";
        public string Cross             { get; set; } = "╬";
    }
}
