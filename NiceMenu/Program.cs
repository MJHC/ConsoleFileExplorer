using System;
using System.Runtime.CompilerServices;

namespace NiceMenu
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu myMenu = new Menu("Menu");
            
            
            myMenu.Add(new MenuItem("Menu Item 1", "This is menu 1"));
            myMenu.Add(new MenuItem("Menu Item 2", "some"));
            myMenu.Add(new MenuItem("Menu Item 3", "thing"));
            myMenu.Add(new MenuItem("Menu Item 4","is"));
            myMenu.Add(new MenuItem("Menu Item 5", "yeet"));
            myMenu.Add(new Menu("Sub Menu 1",
                new MenuItem("Sub Menu Item 1", "this is item"),
                          new MenuItem("Sub Menu Item 2", "this is too"),
                          new Menu("Nein", 
                              new MenuItem("subsub", "subsuyb"))));
            myMenu.Add(new FileExplorer("C:/"));
            myMenu.Start();
        }
    }
}