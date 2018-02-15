using ConsoleFrontend.Helpers;
using ConsoleFrontend.Models;
using System;

namespace ConsoleFrontend
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            var dialog = new MessageDialog($"Dies ist ein:{Environment.NewLine}Test.", 3, 3);
            dialog.ZIndex = 1000;
            dialog.HorizontalAnchor = HorizontalAnchor.Left;
            dialog.VerticalAnchor = VerticalAnchor.Top;
            
            var topBar = new StatusBar("I am a top status bar!");
            var bottomBar = new StatusBar($"I am a two-line{Environment.NewLine}status bar :-O");
            bottomBar.VerticalAnchor = VerticalAnchor.Bottom;

            /* 
            var window = new Window
            {
                Top = 6,
                Left = 3,
                ContentWidth = 40,
                ContentHeight = 5,
            };
            */

            var renderer = new Renderer();
            renderer.Render(new BaseControl[] { dialog, topBar, bottomBar });

            Console.CursorVisible = false;
            Console.ReadKey();

            Console.CursorVisible = true;
            Console.Clear();
        }
    }
}
