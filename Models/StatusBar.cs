using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using ConsoleFrontend.Helpers;

namespace ConsoleFrontend.Models
{
    /// <summary>
    /// A status bar renders information in the following form:
    /// 
    /// ──────────────────────────────
    /// This is status text!
    ///  
    /// </summary>
    public class StatusBar : BaseControl
    {
        // Statusbar is only useable in stretched-mode
        public override bool StretchHorizontal { get => true; set{ ; } }
        public override VerticalAnchor VerticalAnchor
        {
            get { return _verticalAnchor;} 
            set
            {
                if (value != VerticalAnchor.Bottom && value != VerticalAnchor.Top)
                    throw new ArgumentException("Status bar only supports top and bottom anchors");
                else
                    _verticalAnchor = value;
            }
        }
        public char Border { get; set; } = '─';

        public override int TotalHeight => Content.ActualHeight + 1;
        public override int TotalWidth => 0;

        public StatusBar(string content)
        {
            Content = new TextContent(content);
        }

        public override List<string> Render(int? overrideWidth, int? overrideHeight)
        {
            var targetWidth = overrideWidth ?? TotalWidth;
            var targetHeight = overrideHeight ?? TotalHeight;

            var builder = new List<string>();
            
            var sepratator = new String('─', targetWidth);
            
            builder.Add(sepratator);
            if(VerticalAnchor == VerticalAnchor.Bottom)
                builder.AddRange(Content.Render(targetWidth));
            else
                builder.InsertRange(0, Content.Render(targetWidth));
            
            return builder;
        }
    }
}
