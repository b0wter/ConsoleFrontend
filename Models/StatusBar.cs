using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using ConsoleFrontend.Helpers;

namespace ConsoleFrontend.Models
{
    public class StatusBar : BaseControl
    {
        // Statusbar is only useable in stretched-mode.
        public override bool StretchHorizontal { get { return true; } set{ ; } }

        public char Border { get; set; } = '─';

        private IControlContent _content;
        public IControlContent Content { get { return _content; } set { _content = value; NotifyPropertyChanged(); } }

        public override int TotalHeight = 2;

        public StatusBar(IControlContent content)
        {
            Content = content;
        }
        
        public override string[] Render(int? overrideWidth, int? overrideHeight)
        {
            
        }
    }
}
