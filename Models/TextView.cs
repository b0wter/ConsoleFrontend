using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleFrontend.Models
{
    public class TextView : BaseControl
    {
        private string _text;
        /// <summary>
        /// Text content of this control.
        /// </summary>
        public string Text
        {
            get => _text;
            set { _text = value; NotifyPropertyChanged(); }
        }

        public override int ContentWidth  => 0;
        public override int ContentHeight => 0;

        public override BaseControl Content
        {
            set => throw new InvalidOperationException("Cannot add content to a TextView.");
            get => null;
        }

        public TextView(string text)
        {
            Text = text;
        }
        
        public override List<string> Render()
        {
            var rows = (int) Math.Ceiling(Text.Length / (float)Parent.ContentWidth);
            var lines = Enumerable.Range(0, rows)
                        .Select(index => Text.Skip(index * Parent.ContentWidth).Take(Parent.ContentWidth))
                        .Select(x => string.Concat(x))
                        .SelectMany(x => x.Split(Environment.NewLine, StringSplitOptions.None))
                        .ToList();
            
            if (lines.Count > Parent.ContentHeight)
            {
                lines = lines.Take(Parent.ContentHeight).ToList();
                string lastLine = lines.Last();
                while (lastLine.Length > Parent.ContentWidth - 5)
                    lastLine = lastLine.Substring(0, lastLine.Length - 1);
                lastLine += " [>>]";
                lines[lines.Count - 1] = lastLine;
            }
            
            return lines.ToList();
        }
    }
}