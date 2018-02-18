using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ConsoleFrontend.Models
{
    public class Frame : BaseControl
    {
        public override int ContentWidth => Width;
        public override int ContentHeight => Height;

        public override List<string> Render()
        {
            return Content?.Render();
        }
    }
}