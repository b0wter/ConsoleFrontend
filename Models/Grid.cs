using ConsoleFrontend.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace ConsoleFrontend.Models
{
    public class Grid : BaseControl
    {
        /// <summary>
        /// Holds the actual cells this grid contains.
        /// </summary>
        public ObservableCollection<GridCell> GridCells { get; } = new ObservableCollection<GridCell>();
        /// <summary>
        /// Contains the GridColDefinitions of this grid. Each element represents a single column of this grid.
        /// </summary>
        public ObservableCollection<GridColDefinition> GridColDefinitions { get; } = new ObservableCollection<GridColDefinition>();
        /// <summary>
        /// Contains the GridRowDefinitions of this grid. Each element represents a single row of this grid.
        /// </summary>
        public ObservableCollection<GridRowDefinition> GridRowDefinitions { get; } = new ObservableCollection<GridRowDefinition>();

        public override int ContentWidth  => ActualWidth;
        public override int ContentHeight => ActualHeight;

        public Grid()
        {
            GridCells.CollectionChanged += GridCellsOnCollectionChanged;
        }

        /// <summary>
        /// Checks if the new elements are valid for the given collection.
        /// * Index in range
        /// * Index not in use
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="notifyCollectionChangedEventArgs"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void GridCellsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            if(notifyCollectionChangedEventArgs.OldItems != null && notifyCollectionChangedEventArgs.OldItems.Count != 0)
                foreach (var oldItem in notifyCollectionChangedEventArgs.OldItems)
                    ((GridCell) oldItem).PropertyChanged -= GridCell_OnPropertyChanged;
            
            if (notifyCollectionChangedEventArgs.NewItems == null ||
                notifyCollectionChangedEventArgs.NewItems.Count == 0)
                return;
            
            foreach(var newItem in notifyCollectionChangedEventArgs.NewItems)
                ((GridCell)newItem).PropertyChanged += GridCell_OnPropertyChanged;

            if (GridCells.GroupBy(x => x.Coordinates).Any(x => x.Count() > 1))
                throw new InvalidOperationException($"Cannot add multiple cells with the same coordinates.");

            if (GridCells.Any(
                    c => c.Coordinates.X >= GridColDefinitions.Count && GridColDefinitions.Count != 0 || 
                         c.Coordinates.Y >= GridRowDefinitions.Count && GridRowDefinitions.Count != 0 ))
                throw new InvalidOperationException("Cannot add cell to a coordinate that doesnt exist.");
        }

        private void GridCell_OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            TriggerNotifyPropertyChangedFor(sender, propertyChangedEventArgs);
        }

        /// <summary>
        /// Creates a string representation of this class to be rendered by a renderer.
        /// </summary>
        /// <returns></returns>
        public override List<string> Render()
        {
            var cellWidth =  (int)Math.Floor(ContentWidth  / Math.Max(1.0f, GridColDefinitions.Count));
            var cellHeight = (int)Math.Floor(ContentHeight / Math.Max(1.0f, GridRowDefinitions.Count));

            foreach (var cell in GridCells)
            {
                cell.Width = cellWidth;
                cell.Height = cellHeight;
            }

            var lines = new List<string>(100); // arbitrary value
            
            for(var y = 0; y < GridRowDefinitions.Count; ++y)
            {
                // create one row of elements

                var cellsForCurrentRow = GridCells.Where(n => n.Coordinates.Y == y).OrderBy(n => n.Coordinates.X).Select(n => n.Render()).ToList(); // Coordinates as name is inferred.
                if(!cellsForCurrentRow.Any())
                    continue;
                
                var maxLineCount = cellsForCurrentRow.Max(n => n.Count);

                // add placeholder rows if the cells dont match
                foreach (var cell in cellsForCurrentRow)
                    while (cell.Count < maxLineCount)
                        cell.Add(new string(' ', cellWidth));

                for (int i = 0; i < cellsForCurrentRow.First().Count; ++i)
                {
                    var combinedLine = "";
                    foreach (var cell in cellsForCurrentRow)
                        combinedLine += cell[i];
                    lines.Add(combinedLine);
                }
            }

            return lines;
        }

        public void SetContentAt(int x, int y, BaseControl content)
        {
            GridCells.Remove(n => n.X == x && n.Y == y);
            GridCells.Add(new GridCell(x, y, content));
        }
    }

    public enum GridCellSizeModes
    {
        /// <summary>
        /// Makes this cell use the regular size.
        /// </summary>
        Normal,
        /// <summary>
        /// Tells the grid that this cell should take up all remaining space. If multiple cells define this option the space will be divided evenly.
        /// </summary>
        Remaining
    }

    public class GridCell : BaseControl
    {
        public override int ContentWidth  => ActualWidth;
        public override int ContentHeight => ActualHeight;

        private Point _coordinates;
        /// <summary>
        /// Coordinate of this cell in the grid cells.
        /// </summary>
        public Point Coordinates { get => _coordinates; set { _coordinates = value; NotifyPropertyChanged(); } }
        
        public GridCell(int x, int y, BaseControl content)
        {
            this.Content = content;
            this.Coordinates = new Point(x, y);
        }

        public override BaseControl Parent
        {
            get => base.Parent;
            set
            {
                if(typeof(Grid) != value.GetType())
                    throw new ArgumentException($"The only valid parent for a {nameof(GridCell)} is a {nameof(Grid)}");
                base.Parent = value;
            }
        }

        public override List<string> Render()
        {
            return Content.Render();
        }
    }

    public abstract class GridColRowDefinition : BaseModel
    {
        private GridCellSizeModes _sizeMode;
        public GridCellSizeModes SizeMode { get { return _sizeMode; } set { _sizeMode = value; NotifyPropertyChanged(); } }
    }

    public class GridColDefinition : GridColRowDefinition
    {
        private int _widht;
        public int Width
        {
            get => _widht;
            set { _widht = value; NotifyPropertyChanged(); }
        }
    }

    public class GridRowDefinition : GridColRowDefinition
    {
        private int _height;
        public int Height
        {
            get => _height;
            set { _height = value; NotifyPropertyChanged(); }
        }
    }
}