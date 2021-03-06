﻿using System;
using System.Collections.Generic;
using System.Text;

namespace bCurses.Helpers
{
    public class Singleton<T> where T : new()
    {
        private T _instance;
        public T Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new T();
                return _instance;
            }
        }
    }
}
