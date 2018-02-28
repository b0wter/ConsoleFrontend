using System;
using System.Collections.Generic;
using System.Text;
using bCurses.Helpers;
using System.Linq;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Reflection;

namespace bCurses.Models
{
    /// <summary>
    /// Windows act as permanent screen areas that contain grouped information.
    /// 
    /// ╔══════════════════════╗
    /// ║ $NAME                ║
    /// ╟──────────────────────╢
    /// ║ This is a window!    ║
    /// ║ It looks fancy :)    ║
    /// ║                      ║
    /// ║                      ║
    /// ╚══════════════════════╝
    /// 
    /// </summary>
    public abstract class Window : BaseControl
    {
        public string UpperLeftBorder   { get; set; } = "╔";
        public string UpperRightBorder  { get; set; } = "╗";
        public string LowerLeftBorder   { get; set; } = "╚";
        public string LowerRightBorder  { get; set; } = "╝";
        public string HorizontalLine    { get; set; } = "═";
        public string TitleSeparator    { get; set; } = "─";
        public string VerticalLine      { get; set; } = "║";
        public string LeftIntersection  { get; set; } = "╟";
        public string RightIntersection { get; set; } = "╢";
        public string Cross             { get; set; } = "╬";

        public override int ContentWidth => ActualWidth - 4;
        public override int ContentHeight => ActualHeight - 4;

        public override List<string> Render()
        {
            var builder = new List<string>();

            // Header
            //


            int minLength = 1 + 1 + Name.Length + 1 + 1;
            var targetWidth = Math.Max(minLength, ActualWidth);

            // Header
            //
            int headerSpace = Math.Max(0, ActualWidth - minLength);
            builder.Add($"{UpperLeftBorder}{new String(HorizontalLine[0], targetWidth - 2)}{UpperRightBorder}");
            builder.Add($"{VerticalLine} {Name.PadRight(targetWidth - 4)} {VerticalLine}");
            builder.Add($"{LeftIntersection}{new String(TitleSeparator[0], targetWidth - 2)}{RightIntersection}");

            // Content
            //
            var content = Content.Render();
            while (content.Count() < ActualHeight - 4)
                content.Add("");
            foreach(var line in content)
            {
                string l = VerticalLine + " " + line.PadRight(targetWidth - 4) + " " + VerticalLine;
                builder.Add(l);
            }
            
            // Footer
            //
            builder.Add(LowerLeftBorder + (new string(HorizontalLine[0], targetWidth - 2)) + LowerRightBorder);

            return builder;
        }
    }

    public class BindingWindow : Window
    {
        public ObservableCollection<Binding> Bindings { get; private set; } = new ObservableCollection<Binding>();

        public BindingWindow(params Binding[] bindings)
        {
            var contentRoot = new Grid();
            this.Name = Assembly.GetExecutingAssembly().GetName().Name;

            for(int i = 0; i < bindings.Length; ++i)
            {
                contentRoot.GridRowDefinitions.Add(new GridRowDefinition());
                contentRoot.SetContentAt(0, i, new TextView(bindings[i].Target, bindings[i].PropertyName));
            }

            this.Content = contentRoot;
        }
    }

    public class TextWindow : Window
    {
        public TextWindow(string content)
        {
            this.Content = new TextView(content);
            this.Name = Assembly.GetExecutingAssembly().GetName().Name;
        }
    }
}
