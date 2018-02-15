using System.Linq;
using System;

namespace ConsoleFrontend.Helpers
{
    public interface IControlContent
    {
        string[] Render(int width);
        int GetHeight(int width);
    }

    public class TextContent : IControlContent
    {
        private string _content;
        public string Content { get { return _content; } set { _content = value; } }

        public TextContent(string content)
        {
            _content = content;
        }

        public override int GetHeight(int width)
        {
            return (int)Math.Ceiling(Content.Length / (float)width);
        }

        public string[] Render(int width)
        {
            var lines = Enumerable.Range(0, (int)Math.Ceiling(Content.Length / (float)width))
                        .Select(index => Content.Skip(index * width).Take(width))
                        .Select(x => String.Concat(x));
            return lines.ToArray();    
        }
    }
}