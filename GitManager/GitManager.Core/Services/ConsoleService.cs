using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitManager.Core.Services
{
    public class ConsoleService
    {
        private static int DisplaySelect(List<string> options)
        {
            var selectedItem = 0;
            bool done = false;

            while (!done)
            {
                for (int i = 0; i < options.Count; i++)
                {
                    if (selectedItem == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("> ");
                    }
                    else
                        Console.Write("  ");

                    Console.WriteLine(options[i]);
                    Console.ResetColor();
                }

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedItem = Math.Max(0, selectedItem - 1);
                        break;
                    case ConsoleKey.DownArrow:
                        selectedItem = Math.Min(options.Count - 1, selectedItem + 1);
                        break;
                    case ConsoleKey.Enter:
                        done = true;
                        break;
                }

                if (!done)
                    Console.CursorTop = Console.CursorTop - options.Count;
            }
            return selectedItem;
        }

    }
}
