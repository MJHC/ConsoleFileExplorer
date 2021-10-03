using System;
using System.Collections.Generic;

namespace NiceMenu
{
    public static class CConsole
    {
        public const  int CursorOffset = 2;
        public static int CursorPos = CursorOffset + 1;

        public static void ColorPrint(string str, ConsoleColor foreground, ConsoleColor background)
        {
            Console.ForegroundColor = foreground;
            Console.BackgroundColor = background;
            Console.Write(str);
            Console.ResetColor();
        }
        public static void ColorPrint(string str, ConsoleColor foreground)
        {
            Console.ForegroundColor = foreground;
            Console.Write(str);
            Console.ResetColor();
        }
        public static void ColorPrint(ConsoleColor background, string str)
        {
            Console.BackgroundColor = background;
            Console.Write(str);
            Console.ResetColor();
        }
        public static void InfoMessage(string str)
        {
            Console.SetCursorPosition(0, 1);
            Console.Write("\r" + str + new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, CursorPos);
        }

        public static void Cursor(string str)
        {
            ColorPrint(str, ConsoleColor.Black, ConsoleColor.Cyan);
        }

        public static void CursorReset()
        {
            CursorPos = CursorOffset + 1;
        }
    }
}