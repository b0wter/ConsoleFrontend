using ConsoleFrontend.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ConsoleFrontend.Helpers
{
    class Renderer
    {
        private readonly Dictionary<string, Action> _commands;
        private readonly string[] _commandNames;
        private readonly string[] _commandDelimiters = new string[]{"$$"};

        public Renderer()
        {
            // Add some regular commands.
            _commands = new Dictionary<string, Action>
            {
                {"NC", Console.ResetColor},
            };

            // Automatically add the commands to add color.
            foreach (var color in GetConsoleColors())
            {
                _commands.Add("F" + color.Key + "", () => Console.ForegroundColor = color.Value);
                _commands.Add("B" + color.Key + "", () => Console.BackgroundColor = color.Value);
            }

            _commandNames = (new List<string>(_commands.Keys)).ToArray();
        }

        private Dictionary<string, ConsoleColor> GetConsoleColors()
        {
            var names = Enum.GetNames(typeof(ConsoleColor));
            var colors = new Dictionary<string, ConsoleColor>(names.Length);
            foreach(var name in names)
                colors.Add(name, (ConsoleColor)Enum.Parse(typeof(ConsoleColor), name));
            return colors;
        }
        
        public void Render(BaseControl root)
        {
            root.Width = Console.WindowWidth-1;
            root.Height = Console.WindowHeight;

            var rendered = root.Render();

            Console.CursorTop = 0;
            Console.CursorVisible = false;

            foreach (var line in rendered)
            {
                Console.CursorLeft = 0;
                RenderLine(line);
                Console.CursorTop++;
            }
        }

        private void RenderLine(string line)
        {
            var parts = line.Split(_commandDelimiters, StringSplitOptions.RemoveEmptyEntries);

            foreach (var part in parts)
            {
                if (_commands.ContainsKey(part))
                    _commands[part]();
                else
                    Console.Write(part);
            }
        }
        /*
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
        */
    }
}
