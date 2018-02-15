using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleFrontend.Helpers;

namespace ConsoleFrontend.Models
{
    ///<summary>
    /// Dialoge act as popups to display a small amount of information in the following form:
    ///
    /// ┌─┤ ConsoleFrontend ├─┐
    /// │ This is a simple    │
    /// │ test.               │
    /// └─────────────────────┘
    ///
    /// </summary>
    public class Dialog : BaseControl
    {
        public string UpperLeftBorder = "┌";
        public string UpperRightBorder = "┐";
        public string LowerLeftBorder = "└";
        public string LowerRightBorder = "┘";
        public string HorizontalLine = "─";
        public string VerticalLine = "│";
        public string LeftIntersection = "├";
        public string RightIntersection = "┤";
        public string Cross = "┼";

        public override int TotalWidth => Content.ActualWidth + 2 * BorderWidth;
        public override int TotalHeight => Content.ActualHeight + 2 * BorderHeight;
        
        public override List<string> Render(int? overrideWidth = null, int? overrideHeight = null)
        {
            var targetWidth = overrideWidth ?? TotalWidth;
            var targetHeight = overrideHeight ?? TotalHeight;
            
            var builder = new List<string>();
            
            // Header
            //              ┌   ─   ┤   _   $NAME           _   ├   ─   ┐
            int minLength = 1 + 1 + 1 + 1 + Name.Length + 1 + 1 + 1 + 1;
            int headerSpaces = Math.Max(0, targetWidth - minLength);
            string header = UpperLeftBorder + HorizontalLine + RightIntersection + " " + Name + " " + LeftIntersection + (new String(HorizontalLine.First(), headerSpaces)) + HorizontalLine + UpperRightBorder;
            builder.Add(header);
            
            // Content
            //
            var content = Content.Render(targetWidth).SelectMany(x => x.Split(Environment.NewLine, StringSplitOptions.None)).ToList();
            while (content.Count() < targetHeight - 3)
                content.Add("");
            foreach(var line in content)
            {
                string l = VerticalLine + " " + line.PadRight(Content.ActualWidth) + " " + VerticalLine;
                builder.Add(l);
            }

            // Footer
            //
            string footer = LowerLeftBorder + (new String(HorizontalLine.First(), targetWidth - 2)) + LowerRightBorder;
            builder.Add(footer);

            return builder;
        }
    }
    
    /// <summary>
    /// Dialog that contains
    /// </summary>
    public class MessageDialog : Dialog
    {
        //private string _content;
        //public string Content { get { return _content; } set { _content = value; NotifyPropertyChanged(); } }

        public MessageDialog(string content, int x, int y)
        {
            Content = new TextContent(content);
            this.Name = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            this.Content.Width = Math.Max(content.Length, 4 + Name.Length);
            this.Y = y;
            this.X = x;
        }

        public MessageDialog(string content, int width, int x, int y)
            : this(content, x, y)
        {
            this.Content.Width = width;
        }

    }
}

