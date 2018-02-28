using System.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using bCurses.Models;

namespace bCurses.Helpers
{
    [Obsolete("Just kept for reference.")]
    public abstract class BaseContent : BaseModel
    {
        /// <summary>
        /// Actual height of the content, computed using <see cref="Height"/> and <see cref="MaxHeight"/>.
        /// </summary>
        public int ActualHeight
        {
            get => Math.Min(Height, MaxHeight);
        }

        /// <summary>
        /// Actual width of the content, computed using <see cref="Width"/> and <see cref="MaxWidth"/>.
        /// </summary>
        public int ActualWidth
        {
            get => Math.Min(Width, MaxWidth);
        }

        private int _height;
        /// <summary>
        /// Desired height of the content.
        /// </summary>
        public int Height
        {
            get { return _height; }
            set
            {
                _height = value; 
                NotifyPropertyChanged(); 
                NotifyPropertyChanged(nameof(ActualHeight));
                NotifyPropertyChanged(nameof(ActualWidth));
            }
        }

        private int _width;
        /// <summary>
        /// Desired width of the content.
        /// </summary>
        public int Width
        {
            get { return _width; }
            set
            {
                _width = value; 
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(ActualHeight));
                NotifyPropertyChanged(nameof(ActualWidth));
            }
        }

        private int _maxHeight = Int32.MaxValue;
        /// <summary>
        /// Maximum height of the content.
        /// </summary>
        public int MaxHeight
        {
            get => _maxHeight;
            set
            {
                _maxHeight = value; 
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(Height));
                NotifyPropertyChanged(nameof(Width));
                NotifyPropertyChanged(nameof(ActualHeight));
                NotifyPropertyChanged(nameof(ActualWidth));
            }
            
        }

        private int _maxWidth = Int32.MaxValue;
        /// <summary>
        /// Maximum width of the content;
        /// </summary>
        public int MaxWidth
        {
            get => _maxWidth;
            set
            {
                _maxWidth = value; 
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(Height));
                NotifyPropertyChanged(nameof(Width));
                NotifyPropertyChanged(nameof(ActualHeight));
                NotifyPropertyChanged(nameof(ActualWidth));
            }
        }

        private BaseControl _parent;
        /// <summary>
        /// Reference to the parent containing this element.
        /// </summary>
        public BaseControl Parent
        {
            get => _parent;
            set { _parent = value; NotifyPropertyChanged(); }
        }

        public abstract List<string> Render(int width);
        
        /// <summary>
        /// Computes the necessary number of lines for the given string and the given width.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        protected static int ComputeHeightForWidth(string s, int width)
        {
            return (int) Math.Ceiling(s.Length / (float) width);
        }
    }

    /// <summary>
    /// Implementation of <see cref="BaseContent"/> which displays simple text.
    /// New lines in the text content are honored.
    /// </summary>
    [Obsolete("Just kept for reference.")]
    public class TextContent : BaseContent
    {
        private const int DefaultWidth = 20;
        private string _content;
        public string Content { get => _content; set { _content = value; NotifyPropertyChanged(); } }

        protected TextContent()
        {
            //
        }

        /// <summary>
        /// Creates a new instance using the default settings.
        /// </summary>
        /// <param name="content"></param>
        public TextContent(string content)
        {
            Content = content;
            Width = DefaultWidth;
            Height = ComputeHeightForWidth(content, Width);
        }
        
        /// <summary>
        /// Create a new instance of this content, restricting the maximum width.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="maxWidth"></param>
        /// <returns></returns>
        public static TextContent WithMaxWidth(string content, int maxWidth)
        {
            var c = new TextContent
            {
                Content = content,
                Width = maxWidth,
                MaxWidth = maxWidth,
                Height = ComputeHeightForWidth(content, maxWidth)
            };
            return c;
        }

        /// <summary>
        /// Creates a string representation of this content.
        /// Is already split into lines and ready to be printed.
        /// </summary>
        /// <param name="width"></param>
        /// <returns></returns>
        public override List<string> Render(int width)
        {
            var lines = Enumerable.Range(0, (int)Math.Ceiling(Content.Length / (float)width))
                        .Select(index => Content.Skip(index * width).Take(width))
                        .Select(x => String.Concat(x));
            return lines.ToList();    
        }
    }
}