using System;
using System.Collections.Generic;
using System.Text;

namespace bCurses.Models
{
    class TestModel : BaseModel
    {
        private string _text;
        public string Text { get { return _text; } set { _text = value; NotifyPropertyChanged(); } }

        public TestModel(string text)
        {
            Text = text;
        }
    }
}
