using bCurses.Helpers;
using bCurses.Models;
using ConsoleFrontend.Helpers;
using System;

namespace ConsoleFrontEnd
{
    class Program
    {
        [Obsolete("Current implementation not satisfying.")]
        public static Singleton<DisplayConfiguration> DisplayConfiguration { get; } = new Singleton<DisplayConfiguration>();

        private static Renderer _renderer = new Renderer();
        private static Grid _grid;
        private static BaseControl _root;

        static void Main(string[] args)
        {
            Console.Clear();

            var testModel = new TestModel("Testcontent");
            var watch = new Watch();

            var binding = new Binding(testModel, nameof(TestModel.Text));
            var timeBinding = new Binding(watch, nameof(watch.Time));

            var dialog1 = new BindingWindow(binding, binding, binding, timeBinding);
            var dialog2 = new BindingDialog(testModel, nameof(TestModel.Text));

            /*
            _grid = new Grid();
            _grid.GridRowDefinitions.Add(new GridRowDefinition());
            _grid.GridRowDefinitions.Add(new GridRowDefinition());
            _grid.GridColDefinitions.Add(new GridColDefinition());
            _grid.GridColDefinitions.Add(new GridColDefinition());
            _grid.SetContentAt(0, 0, dialog1);
            _grid.SetContentAt(0, 1, dialog2);
            */

            _root = new Frame();
            _root.Content = dialog1;
            _root.PropertyChanged += Grid_PropertyChanged;
            /*
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

            _renderer.Render(_root);
            //_renderer.Render(_grid);
            //_grid.PropertyChanged += Grid_PropertyChanged;
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
            if(e.PropertyName == "Content" || e.PropertyName == "Text")
                _renderer.Render(_root);
        }
    }
}
