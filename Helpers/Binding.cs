using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ConsoleFrontend.Helpers
{
    public class Binding
    {
        public string PropertyName { get; }
        public INotifyPropertyChanged Target { get; }

        public Binding(INotifyPropertyChanged target, string propertyName)
        {
            Target = target;
            PropertyName = propertyName;
        }
    }
}
