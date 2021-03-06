﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace bCurses.Models
{
    /// <summary>
    /// Base class for all models that implements <see cref="INotifyPropertyChanged"/>.
    /// </summary>
    public abstract class BaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName]string property = null)
        {
            if (string.IsNullOrWhiteSpace(property) == false)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        protected void TriggerNotifyPropertyChangedFor(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(sender, e);
        }
    }
}
