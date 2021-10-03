using System;
using System.Collections.Generic;
using System.IO;

namespace NiceMenu
{
    public class Menu : IMenuItem
    {
        private int _itemIndex;
        private bool _selectorTurnedOn = false;
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCloseButton { get; }
        
        private List<IMenuItem> _items = new();
        

        public Menu(string title)
        {
            Console.CursorVisible = false;
            Title = title;
            IsCloseButton = false;
        }
        
        public Menu(string title, params IMenuItem[] items)
        {
            Title = title;
            IsCloseButton = false;

            foreach (IMenuItem item in items)
            {
                Add(item);
            }
        }
        
        public void Start()
        {
            _selectorTurnedOn = true;

            DisplayItems(_items);

            while (_selectorTurnedOn)
            {
                Selector();
            }
        }
        
        public void Add(IMenuItem item)
        {
            _items.Add(item);
        }

        private void DisplayItems(List<IMenuItem> items)
        {
            Console.Clear();
            
            _itemIndex = 0;
            CConsole.CursorReset();
            
            CConsole.ColorPrint($"{Title}\n\n\n",ConsoleColor.Red, ConsoleColor.Black);
            
            for (int i = 0; i < items.Count; i++)
            {
                if (i == 0)
                    CConsole.Cursor($"{items[i].Title}\n");
                else
                    Console.WriteLine(items[i].Title);
            }
            
            Console.SetCursorPosition(0, CConsole.CursorPos);
        }

        private void Selector()
        {
            ConsoleKeyInfo keyPress = Console.ReadKey(true);

            switch (keyPress.Key)
            {

                case ConsoleKey.W: case ConsoleKey.UpArrow:
                    MoveCursor(-1); 
                    break;
                case ConsoleKey.S: case ConsoleKey.DownArrow: 
                    MoveCursor(1); 
                    break;
                case ConsoleKey.Enter:
                    _items[_itemIndex].Select();
                    if(_items[_itemIndex] is Menu) 
                        DisplayItems(_items);
                    break;

            }
        }

        private void MoveCursor(int pos)
        {
            if (CConsole.CursorPos + pos > _items.Count + CConsole.CursorOffset ||
                CConsole.CursorPos + pos <= CConsole.CursorOffset) return;
            
            CConsole.CursorPos += pos;
            _itemIndex = CConsole.CursorPos - 1 - CConsole.CursorOffset;
            int old = _itemIndex + (pos * -1);

            // Writes the last selected line in normal text
            if(!_items[old].IsCloseButton)
                Console.Write($"\r{_items[old].Title}");
            else
                CConsole.ColorPrint($"\r{_items[old].Title}", ConsoleColor.Green);
            Console.SetCursorPosition(0, CConsole.CursorPos);
                
            //Selects new item as cursor
            CConsole.Cursor(_items[_itemIndex].Title);
        }

        public void Select()
        {
            _items.Insert(0, new MenuItem("...", this));
            Start();
        }

        public void Close()
        {
            _items.RemoveAt(0);
            _selectorTurnedOn = false;
        }
    }

    public class MenuItem : IMenuItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public  bool IsCloseButton { get; }
        
        private readonly Menu _menu;


        public MenuItem(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public MenuItem(string title, Menu menu)
        {
            Title = title;
            _menu = menu;
            IsCloseButton = true;
        }

        public MenuItem(string title)
        {
            Title = title;
        }

        public void Select()
        {
            if(_menu != null)
                _menu.Close();
            else
                CConsole.InfoMessage(Description);
        }
    }

    public class FileExplorer : Menu
    {
        private static int _indexedFolders = 0;
        public FileExplorer(string dir) : base(dir)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dir);
            bool isRoot = dirInfo.Root.FullName.Equals(dirInfo.FullName);
            bool isSystem = dirInfo.Attributes.HasFlag(FileAttributes.System);
            
            foreach (var item in Directory.GetDirectories(dir))
            {
                try
                {
                    if (Directory.GetDirectories(item).Length != 0)
                        Add(new FileExplorer(item));
                    else Add(new MenuItem(item));
                    Console.SetCursorPosition(0, 0);
                    CConsole.ColorPrint("\rIndexing File System...", ConsoleColor.White);
                    CConsole.ColorPrint($" {_indexedFolders} ", ConsoleColor.Green);
                    CConsole.ColorPrint("Indexed!" + new string(' ', Console.WindowWidth), ConsoleColor.White);
                }
                catch (UnauthorizedAccessException)
                {
                    
                }

                _indexedFolders++;
            }
        }

        public FileExplorer(string dir, params IMenuItem[] items) : base(dir, items)
        {
        }
        
    }
}