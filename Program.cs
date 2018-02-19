using ConsoleFrontend.Helpers;
using ConsoleFrontend.Models;
using System;

namespace ConsoleFrontend
{
    class Program
    {
        [Obsolete("Current implementation not satisfying.")]
        public static Singleton<DisplayConfiguration> DisplayConfiguration { get; } = new Singleton<DisplayConfiguration>();

        private static Renderer _renderer = new Renderer();
        private static Grid _grid;

        static void Main(string[] args)
        {
            Console.Clear();

            var testModel = new TestModel("Testcontent");
            var dialog1 = new MessageDialog($"Dies ist ein:{Environment.NewLine}Test.");
            var dialog2 = new BindingDialog(testModel, nameof(TestModel.Text));

            _grid = new Grid();
            _grid.GridRowDefinitions.Add(new GridRowDefinition());
            _grid.GridRowDefinitions.Add(new GridRowDefinition());
            _grid.GridColDefinitions.Add(new GridColDefinition());
            _grid.GridColDefinitions.Add(new GridColDefinition());
            _grid.SetContentAt(0, 0, dialog1);
            _grid.SetContentAt(1, 0, dialog2);

            /*
            var root = new Frame();
            var dialog = new MessageDialog($"Dies ist ein:{Environment.NewLine}Test.sadfsadfsafdsafdsafsafdasfdsafdsafdlkjlqwjrlwqjrljwqrl;wqjre;lqwjr;lwqjre;lwqjre;lwqjoicoeboiebrqwewqwqfewqefwqefwqfewqfwqfejl;j;lj;ljas;ofdijsa;ofdjw;oejowqeoweowoqiejqofeqe", 3, 3, 20, 6);
            //var dialog = new MessageDialog($"Dies ist ein:{Environment.NewLine}Test.");
            dialog.ZIndex = 1000;
            dialog.HorizontalAnchor = HorizontalAnchors.Left;
            dialog.VerticalAnchor = VerticalAnchors.Top;

            //root.Content = dialog;
            
            var topBar = new StatusBar("I am a top status bar!");
            var bottomBar = new StatusBar($"I am a two-line{Environment.NewLine}status bar :-O");
            bottomBar.VerticalAnchor = VerticalAnchors.Bottom;

            /*
            var window = new Window
            {
                Top = 6,
                Left = 3,
                ContentWidth = 40,
                ContentHeight = 5,
            };
            */

            //renderer.Render(root);
            _renderer.Render(_grid);
            _grid.PropertyChanged += Grid_PropertyChanged;
            //renderer.Render(new BaseControl[] { dialog, topBar, bottomBar });

            testModel.Text = "new text";
            dialog2.ZIndex = 10;
            Console.CursorVisible = false;
            Console.ReadKey();

            Console.CursorVisible = true;
            Console.Clear();
        }

        private static void Grid_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _renderer.Render(_grid);
        }
    }
}
