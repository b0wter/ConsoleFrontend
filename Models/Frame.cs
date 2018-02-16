using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ConsoleFrontend.Models
{
    /// <summary>
    /// A frame holds content but is otherwise invisible.
    /// Used as a root object for the scene.
    /// </summary>
    public class Frame : BaseControl
    {
        public override int ContentWidth => Width;
        public override int ContentHeight => Height;

        public override List<string> Render()
        {
            if(this.Width == 0 || this.Height == 0)
                throw new InvalidOperationException("Cannot render a frame with Width or Height = 0. Frames are only used as composition roots.");
            return Content.Render();
        }
    }
}