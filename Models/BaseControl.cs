using System;
using System.Collections.Generic;
using System.Text;
using ConsoleFrontend.Helpers;

namespace ConsoleFrontend.Models
{
    public enum HorizontalAnchor
    {
        Left,
        Center,
        Right
    }

    public enum VerticalAnchor
    {
        Top,
        Center,
        Bottom
    }

    public abstract class BaseControl : BaseModel
    {
        protected const int BorderWidth = 2;
        protected const int BorderHeight = 2;

        private string _name;
        public string Name { get { return _name; } set { _name = value; NotifyPropertyChanged(); } }

        public abstract int TotalWidth { get; }
        public abstract int TotalHeight { get; }

        private int _y;
        public int Y { get { return _y; } set { _y = value; NotifyPropertyChanged(); } }

        private int _x;
        public int X { get { return _x; } set { _x = value; NotifyPropertyChanged(); } }

        private int _zIndex;
        public int ZIndex { get { return _zIndex; } set { _zIndex = value; NotifyPropertyChanged(); } }

        private HorizontalAnchor _horizontalAnchor = HorizontalAnchor.Left;
        public HorizontalAnchor HorizontalAnchor { get { return _horizontalAnchor; } set{ _horizontalAnchor = value; NotifyPropertyChanged(); } }

        private bool _stretchHorizontal;
        public virtual bool StretchHorizontal {get{return _stretchHorizontal;} set{_stretchHorizontal=value; NotifyPropertyChanged();}}

        protected VerticalAnchor _verticalAnchor = VerticalAnchor.Top;
        public virtual VerticalAnchor VerticalAnchor { get { return _verticalAnchor;} set{ _verticalAnchor = value; NotifyPropertyChanged(); } }

        private bool _stretchVertical;
        public bool StretchVertical {get{return _stretchVertical;} set{_stretchVertical = value; NotifyPropertyChanged();}}

        private BaseContent _content;
        public BaseContent Content { get { return _content; } set { _content = value; NotifyPropertyChanged(); } }

        private BaseControl _parent;
        public BaseControl Parent
        {
            get => _parent;
            set { _parent = value; NotifyPropertyChanged(); }
        }

        public abstract List<string> Render(int? overrideWidth = null, int? overrideHeight = null);
    }
}
