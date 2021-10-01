using System;

namespace NiceMenu
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu myMenu = new Menu();
            
            myMenu.Add(new MenuItem("Menu Item 1"));
            myMenu.Add(new MenuItem("Menu Item 2"));
            myMenu.Add(new MenuItem("Menu Item 3"));
            myMenu.Add(new MenuItem("Menu Item 4"));
            myMenu.Add(new MenuItem("Menu Item 5"));
            myMenu.Start();
        }
    }
}