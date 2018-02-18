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
        private bool _hasSeparator;
        public bool HasSeparator
        {
            get => _hasSeparator;
            set { _hasSeparator = value; NotifyPropertyChanged(); }
        }

        // Statusbar is only useable in stretched-mode
        public override int ContentWidth  => Width;
        public override int ContentHeight => Height - (HasSeparator ? 1 : 0);
        
        public override bool StretchHorizontal { get => true; set{ ; } }
        public override VerticalAnchors VerticalAnchor
        {
            get { return _verticalAnchor;} 
            set
            {
                if (value != VerticalAnchors.Bottom && value != VerticalAnchors.Top)
                    throw new ArgumentException("Status bar only supports top and bottom anchors");
                else
                    _verticalAnchor = value;
            }
        }
        public char Border { get; set; } = '─';

        public StatusBar(string content)
        {
            Content = new TextView(content);
        }

        public override List<string> Render()
        {
            var targetWidth = Math.Min(Parent.Width, Width);
            var targetHeight = Math.Min(Parent.Height, Height);

            var builder = new List<string>();
            
            var sepratator = new String('─', targetWidth);
            
            builder.Add(sepratator);
            if(VerticalAnchor == VerticalAnchors.Bottom)
                builder.AddRange(Content.Render());
            else
                builder.InsertRange(0, Content.Render());
            
            return builder;
        }
    }
}
