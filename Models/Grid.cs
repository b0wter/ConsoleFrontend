using ConsoleFrontend.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace ConsoleFrontend.Models
{
    public class Grid : BaseControl
    {
        private readonly ObservableCollection<GridCell> _gridCells = new ObservableCollection<GridCell>();
        
        public override int ContentWidth  => ActualWidth;
        public override int ContentHeight => ActualHeight;

        public Grid()
        {
            _gridCells.CollectionChanged += GridCellsOnCollectionChanged;
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
            if (notifyCollectionChangedEventArgs.NewItems == null ||
                notifyCollectionChangedEventArgs.NewItems.Count == 0)
                return;
            
            //_gridCells.GroupBy(x =>)
        }

        public override List<string> Render()
        {
            throw new System.NotImplementedException();
        }
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
            throw new System.NotImplementedException();
        }
    }

    public class GridColDefinition : BaseModel
    {
        private int _widht;
        public int Width
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