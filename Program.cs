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

            var root = new Frame();
            var dialog = new MessageDialog($"Dies ist ein:{Environment.NewLine}Test.sadfsadfsafdsafdsafsafdasfdsafdsafdlkjlqwjrlwqjrljwqrl;wqjre;lqwjr;lwqjre;lwqjre;lwqjoicoeboiebrqwewqwqfewqefwqefwqfewqfwqfejl;j;lj;ljas;ofdijsa;ofdjw;oejowqeoweowoqiejqofeqe", 3, 3, 20, 6);
            //var dialog = new MessageDialog($"Dies ist ein:{Environment.NewLine}Test.");
            dialog.ZIndex = 1000;
            dialog.HorizontalAnchor = HorizontalAnchor.Left;
            dialog.VerticalAnchor = VerticalAnchor.Top;

            //root.Content = dialog;
            
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
            //renderer.Render(root);
            renderer.Render(dialog);
            //renderer.Render(new BaseControl[] { dialog, topBar, bottomBar });

            Console.CursorVisible = false;
            Console.ReadKey();

            Console.CursorVisible = true;
            Console.Clear();
        }
    }
}
