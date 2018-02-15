using ConsoleFrontend.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ConsoleFrontend.Helpers
{
    class Renderer
    {
        public void Render(IEnumerable<BaseControl> objects)
        {
            var orderedObjects = objects.OrderBy(x => x.ZIndex);
            foreach(var o in orderedObjects)
            {
                Render(o);
            }
        }

        private void Render(BaseControl c)
        {
            switch(c.VerticalAnchor)
            {
                case VerticalAnchor.Top:
                    Console.CursorTop = c.Y;
                    break;
                case VerticalAnchor.Center:
                    Console.CursorTop = (Console.WindowHeight - c.TotalHeight) / 2 + c.Y;
                    break;
                case VerticalAnchor.Bottom:
                    Console.CursorTop = Console.WindowHeight - (c.Y + c.TotalHeight);
                    break;
                default:
                    throw new ArgumentException($"{c.VerticalAnchor} is unknown.");
            }

            int left;
            switch(c.HorizontalAnchor)
            {
                case HorizontalAnchor.Left:
                    left = c.X;
                    break;
                case HorizontalAnchor.Center:
                    left = (Console.WindowWidth - c.TotalWidth) / 2 + c.X;
                    break;
                case HorizontalAnchor.Right:
                    left = Console.WindowWidth - c.TotalWidth - c.X;
                    break;
                default:
                    throw new ArgumentException($"{c.HorizontalAnchor} is unknown.");
            }

	    int width = c.StretchHorizontal ? Console.WindowWidth : c.TotalWidth;
	    int height = c.StretchVertical ? Console.WindowHeight : c.TotalHeight;
            foreach(var line in c.Render(width, height))
            {
                Console.CursorLeft = left;
                Console.Write(line);
                Console.CursorTop++;
            }
        }
    }
}
