using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ConsoleFrontend.Models
{
    public class Grid : BaseControl
    {
        public string UpperLeftBorder   { get; set; } = "┌";
        public string UpperRightBorder  { get; set; }= "┐";
        public string LowerLeftBorder   { get; set; }= "└";
        public string LowerRightBorder  { get; set; }= "┘";
        public string HorizontalLine    { get; set; }= "─";
        public string VerticalLine      { get; set; }= "│";
        public string LeftIntersection  { get; set; }= "├";
        public string RightIntersection { get; set; }= "┤";
        public string Cross = "┼";
        
        private ObservableCollection<>
        
        private bool _hasBorder = false;
        /// <summary>
        /// If true, drawes a border (single cell) around the grid. Reduces the ContentWidth/Height by 2.
        /// </summary>
        public bool HasBorder
        {
            get => _hasBorder;
            set
            {
                _hasBorder = value;
                NotifyPropertyChanged();
            }
        }

        public override int ContentWidth  => HasBorder ? ActualWidth - 2 : ActualWidth;
        public override int ContentHeight => HasBorder ? ActualHeight - 2 : ActualHeight;
        
        public override List<string> Render()
        {
            throw new System.NotImplementedException();
        }
    }

    public class GridColDefinition : BaseModel
    {
        private int _widht;
        public int Widht
        {
            get => _widht;
            set => _widht = value;
        }
    }

    public class GridRowDefinition : BaseModel
    {
        private int _height;
        public int Height
        {
            get => _height;
            set => _height = value;
        }
    }
}