﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ConsoleFrontend.Models
{
    public abstract class BaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName]string property = null)
        {
            if (string.IsNullOrWhiteSpace(property) == false)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}