using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;

namespace NiceMenu
{
    public class Menu
    {
        private readonly List<MenuItem> _items = new();
        private int _cursorPos;
        private int _itemIndex;
        private readonly int _offset = 2;

        public void Start()
        {
            Console.CursorVisible = false;
            
            ColorWrite("Welcome to NiceMenu Selector\n\n\n", ConsoleColor.Black, ConsoleColor.Red);

            for (int i = 0; i < _items.Count; i++)
            {
                if(i != _items.Count -1)
                    Console.WriteLine(_items[i].Title);
                else
                    ColorWrite($"{_items[i].Title}", ConsoleColor.Cyan, ConsoleColor.Black);
            }

            while (true)
            {
                Selector();
            }

        }

        public void Add(MenuItem Item)
        {
            _items.Add(Item);
            _cursorPos = _items.Count + _offset;
        }

        public void Selector()
        {
            ConsoleKeyInfo keyPress = Console.ReadKey(true);

            switch (keyPress.Key)
            {

                case ConsoleKey.W: MoveCursor(-1); break;
                case ConsoleKey.S: MoveCursor(1); break;
                case ConsoleKey.Enter:
                    
                    Console.SetCursorPosition(0, 1);
                    Console.Write("\r");
                    Console.Write($"Selected! {_items[_itemIndex].Title}" + new string(' ', 24));
                    Console.SetCursorPosition(0, _cursorPos);
                    
                    break;
                default:
                    
                    Console.SetCursorPosition(0, _items.Count + _offset + 4);

                    ColorWrite("  Controls  ", ConsoleColor.White, ConsoleColor.Red);
                    Console.Write("\t");
                    ColorWrite("  W: Up  ", ConsoleColor.White, ConsoleColor.Black);
                    Console.Write("\t");
                    ColorWrite("  S: Down  ", ConsoleColor.White, ConsoleColor.Black);
                    Console.Write("\t");
                    ColorWrite("  Enter: Select  ", ConsoleColor.White, ConsoleColor.Black);
                    
                    break;
            }
        }

        void MoveCursor(int pos)
        {
            if (_cursorPos + pos <= _items.Count + _offset && _cursorPos + pos > _offset)
            {
                _cursorPos += pos;
                _itemIndex = _cursorPos - 1 - _offset;

                // Clears line 
                Console.Write("\r");
                
                // Writes the last selected line in normal text
                Console.Write(_items[_itemIndex + (pos * -1)].Title);
                
                ColorSelect(pos);
            }
            else
            {
                Console.SetCursorPosition(0, 1);
                Console.Write("No More Items!" + new string(' ', 24));
                ColorSelect(pos);
            }
        }

        void ColorSelect(int pos)
        {
            Console.SetCursorPosition(0, _cursorPos);

            // Highlights the newly selected line
            Console.Write("\r");
            ColorWrite(_items[_itemIndex].Title, ConsoleColor.Cyan, ConsoleColor.Black);
        }

        void ColorWrite(string @string, ConsoleColor backgroundColor, ConsoleColor foregroundColor)
        {
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.Write(@string);
            Console.ResetColor();
        }
        
    }

    //public class FileSystemMenu {};

    public class MenuItem : IMenuItem
    {
        public string Title { get; set; }

        public MenuItem(string @string)
        {
            Title = @string;
        }

        public void NewPage()
        {
            Console.WriteLine("yes");
        }
    }
    
    
}