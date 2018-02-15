using System;
using System.Collections.Generic;
using System.Text;

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

        private int _contentWidth;
        public int ContentWidth { get { return _contentWidth; } set { _contentWidth = value; NotifyPropertyChanged(); NotifyPropertyChanged(nameof(TotalWidth)); } }

        public abstract int TotalWidth { get; }
        
        public abstract int TotalHeight { get; }

        private int _contentHeight;
        public int ContentHeight { get { return _contentHeight; } set { _contentHeight = value; NotifyPropertyChanged(); NotifyPropertyChanged(nameof(TotalHeight)); } }

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

        private VerticalAnchor _verticalAnchor = VerticalAnchor.Top;
        public VerticalAnchor VerticalAnchor { get { return _verticalAnchor;} set{ _verticalAnchor = value; NotifyPropertyChanged(); } }

        private bool _stretchVertical;
        public bool StretchVertical {get{return _stretchVertical;} set{_stretchVertical = value; NotifyPropertyChanged();}}

        public abstract string[] Render(int? overrideWidth = null, int? overrideHeight = null);
    }
}
