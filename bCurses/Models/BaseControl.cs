using System;
using System.Collections.Generic;
using System.Text;
using bCurses.Helpers;

namespace bCurses.Models
{
    /// <summary>
    /// Possible ways for a control to be anchored in its parent (horizontally).
    /// </summary>
    public enum HorizontalAnchors
    {
        Left,
        Center,
        Right
    }

    /// <summary>
    /// Possible ways for a control to be anchored in its parent (vertically).
    /// </summary>
    public enum VerticalAnchors
    {
        Top,
        Center,
        Bottom
    }

    public abstract class BaseControl : BaseModel
    {
        private string _name;
        /// <summary>
        /// Name of this control. Might be used as a title by some controls.
        /// </summary>
        public string Name { get => _name; set { _name = value; NotifyPropertyChanged(); } }

        private int _width;
        /// <summary>
        /// Desired width of this control. If it is set to zero it will be stretched.
        /// </summary>
        public int Width
        {
            get => _width;
            set { _width = value; NotifyPropertyChanged(); }
        }

        private int _height;
        /// <summary>
        /// Desired height of this control. It it is set to zero it will be stretched.
        /// </summary>
        public int Height
        {
            get => _height;
            set { _height = value; NotifyPropertyChanged(); }
        }
        
        /// <summary>
        /// Available space for the content.
        /// </summary>
        public abstract int ContentWidth { get; }
        
        /// <summary>
        /// Available space for the content.
        /// </summary>
        public abstract int ContentHeight { get; }
        
        /// <summary>
        /// Actual height of the control, checks the layout options and the size of the parent.
        /// </summary>
        public int ActualHeight
        {
            get
            {
                if (StretchVertical)
                    return Parent.ContentHeight;
                else
                    return Height;
            }
        }

        /// <summary>
        /// Actual height of the control, checks the layout options and the size of the parent.
        /// </summary>
        public int ActualWidth
        {
            get
            {
                if (StretchHorizontal)
                    return Parent.ContentWidth;
                else
                    return Width;
            }
        }

        private int _y;
        /// <summary>
        /// Vertical position (+/- depends on the <see cref="VerticalAnchor"/>. 
        /// Is set to zero if no <see cref="Height"/> is set.
        /// </summary>
        public int Y { get => Height == 0 ? 0 : _y; set { _y = value; NotifyPropertyChanged(); } }

        private int _x;
        /// <summary>
        /// Horizontal position (+/- depends on the <see cref="HorizontalAnchor"/>).
        /// Is set to zero if no <see cref="Width"/> is set.
        /// </summary>
        public int X { get => Width == 0 ? 0 : _x; set { _x = value; NotifyPropertyChanged(); } }

        private int _zIndex;
        /// <summary>
        /// Used to stack controls over each other. Higher ZIndex means more prevalence.
        /// </summary>
        public int ZIndex { get { return _zIndex; } set { _zIndex = value; NotifyPropertyChanged(); } }

        private HorizontalAnchors _horizontalAnchor = HorizontalAnchors.Left;
        /// <summary>
        /// Point of the parent this control is attached to. E.g. left or right.
        /// </summary>
        public HorizontalAnchors HorizontalAnchor { get => _horizontalAnchor; set{ _horizontalAnchor = value; NotifyPropertyChanged(); } }

        private bool _stretchHorizontal;
        /// <summary>
        /// Wether or not the control should be stretched across the available space.
        /// </summary>
        public virtual bool StretchHorizontal { get => Width == 0 || _stretchHorizontal; set{_stretchHorizontal=value; NotifyPropertyChanged();}}

        protected VerticalAnchors _verticalAnchor = VerticalAnchors.Top;
        /// <summary>
        /// Point of the parent this control is attached to. E.g. top or bottom.
        /// </summary>
        public virtual VerticalAnchors VerticalAnchor { get => _verticalAnchor; set{ _verticalAnchor = value; NotifyPropertyChanged(); } }

        private bool _stretchVertical;
        /// <summary>
        /// Wether or not the control should be stretched across the available space.
        /// </summary>
        public bool StretchVertical { get => Height == 0 || _stretchVertical; set { _stretchVertical = value; NotifyPropertyChanged();}}

        private string _foregroundColor;
        /// <summary>
        /// Foregroundcolor for this control. Needs to be known to the renderer.
        /// </summary>
        public string ForegroundColor { get { return _foregroundColor; } set { _foregroundColor = value; NotifyPropertyChanged(); } }

        private string _backgroundColor;
        /// <summary>
        /// Backgroundcolor for this control. Needs to be known to the renderer.
        /// </summary>
        public string BackgroundColor { get { return _backgroundColor; } set { _backgroundColor = value; NotifyPropertyChanged(); } }

        private BaseControl _content;
        /// <summary>
        /// Content of this control. May be another control, or actual content like text.
        /// </summary>
        public virtual BaseControl Content
        {
            get => _content;
            set
            {
                if (_content != null)
                {
                    _content.PropertyChanged -= Content_PropertyChanged;
                    _content.Parent = null;
                }
                
                _content = value;
                _content.Parent = this;
                _content.PropertyChanged += Content_PropertyChanged;
                NotifyPropertyChanged();
            }
        }

        private void Content_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            TriggerNotifyPropertyChangedFor(sender, e);
        }

        private BaseControl _parent;
        /// <summary>
        /// Reference to the parent control of this control.
        /// </summary>
        public virtual BaseControl Parent
        {
            get => _parent;
            set { _parent = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// Creates a string representation of this control and its content.
        /// </summary>
        /// <returns></returns>
        public abstract List<string> Render();
    }
}
