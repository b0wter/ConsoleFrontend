using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace bCurses.Helpers
{
    [Obsolete("Current implementation not satisfying.")]
    public class DisplayConfiguration
    {
        public Dictionary<string, Action> Commands { get; }
        public string[] CommandNames { get; }
        public string[] CommandDelimiters { get; } = new string[] { "$$" };

        public DisplayConfiguration()
        {
            // Add some regular commands.
            Commands = new Dictionary<string, Action>
            {
                {"NC", Console.ResetColor},
            };

            // Automatically add the commands to add color.
            foreach (var color in GetConsoleColors())
            {
                Commands.Add("F" + color.Key + "", () => Console.ForegroundColor = color.Value);
                Commands.Add("B" + color.Key + "", () => Console.BackgroundColor = color.Value);
            }

            CommandNames = (new List<string>(Commands.Keys)).ToArray();
        }

        private Dictionary<string, ConsoleColor> GetConsoleColors()
        {
            var names = Enum.GetNames(typeof(ConsoleColor));
            var colors = new Dictionary<string, ConsoleColor>(names.Length);
            foreach (var name in names)
                colors.Add(name, (ConsoleColor)Enum.Parse(typeof(ConsoleColor), name));
            return colors;
        }
    }
}
