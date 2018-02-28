using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace bCurses.Helpers
{
    public class Binding
    {
        public string PropertyName { get; }
        public INotifyPropertyChanged Target { get; }
        public IBindingContentConverter Converter { get; }

        public Binding(INotifyPropertyChanged target, string propertyName)
        {
            Target = target;
            PropertyName = propertyName;
        }

        public Binding(INotifyPropertyChanged target, string propertyName, IBindingContentConverter converter)
            : this(target, propertyName)
        {
            Converter = converter;
        }
    }
}
