using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleFrontend.Helpers;

namespace ConsoleFrontend.Models
{
    ///<summary>
    /// Dialoge fungieren als kleine Statusfenster und werden in der folgenden Form gerendet:
    ///
    /// ┌─┤ ConsoleFrontend ├─┐
    /// │ Dies ist ein:       │
    /// │ Test.               │
    /// └─────────────────────┘
    ///
    /// </summary>
    public abstract class Dialog : BaseControl
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

        public override int TotalWidth => ContentWidth + 2 * BorderWidth;
        public override int TotalHeight => ContentHeight + 2 * BorderHeight;

        protected IControlContent Content {get; set;}
    }
    
    public class MessageDialog : Dialog
    {
        //private string _content;
        //public string Content { get { return _content; } set { _content = value; NotifyPropertyChanged(); } }

        public MessageDialog(string content, int x, int y)
        {
            Content = new TextContent(content);
            this.Name = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            this.ContentWidth = Math.Max(content.Length, 4 + Name.Length);
            this.Y = y;
            this.X = x;
        }

        public MessageDialog(string content, int width, int x, int y)
            : this(content, x, y)
        {
            this.ContentWidth = width;
        }

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

