using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        // ReSharper disable MemberCanBePrivate.Global
        public string UpperLeftBorder   { get; set; } = "┌";
        public string UpperRightBorder  { get; set; }= "┐";
        public string LowerLeftBorder   { get; set; }= "└";
        public string LowerRightBorder  { get; set; }= "┘";
        public string HorizontalLine    { get; set; }= "─";
        public string VerticalLine      { get; set; }= "│";
        public string LeftIntersection  { get; set; }= "├";
        public string RightIntersection { get; set; }= "┤";
        public string Cross = "┼";
        // ReSharper restore MemberCanBePrivate.Global

        public override int ContentWidth => ActualWidth - 4;
        public override int ContentHeight => ActualHeight - 2;

        public override List<string> Render()
        {
            var builder = new List<string>();
            // Header
            //              ┌   ─   ┤   _   $NAME           _   ├   ─   ┐
            int minLength = 1 + 1 + 1 + 1 + Name.Length + 1 + 1 + 1 + 1;

            var targetWidth = Math.Max(minLength, ActualWidth);
            
            int headerSpaces = Math.Max(0, ActualWidth - minLength);
            string header = UpperLeftBorder + HorizontalLine + RightIntersection + " " + Name + " " + LeftIntersection + (new String(HorizontalLine.First(), headerSpaces)) + HorizontalLine + UpperRightBorder;
            builder.Add(header);
            
            // Content
            //
            var content = Content.Render().SelectMany(x => x.Split(Environment.NewLine, StringSplitOptions.None)).ToList();
            while (content.Count() < ActualHeight - 2)
                content.Add("");
            foreach(var line in content)
            {
                string l = VerticalLine + " " + line.PadRight(targetWidth - 4) + " " + VerticalLine;
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
    public sealed class MessageDialog : Dialog
    {
        public MessageDialog(string content)
        {
            Content = new TextView(content);    
            this.Name = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

            // To show the full header, see Render()
            this.Width = 1 + 1 + 1 + 1 + Name.Length + 1 + 1 + 1 + 1;
        }
        
        public MessageDialog(string content, int x, int y, int width, int height)
            : this(content)
        {
            Content = new TextView(content);
            //this.Content.Width = Math.Max(content.Length, 4 + Name.Length);
            this.Y = y;
            this.X = x;
            this.Width = Math.Max(this.Width, width);
            this.Height = height;
        }
    }
}

