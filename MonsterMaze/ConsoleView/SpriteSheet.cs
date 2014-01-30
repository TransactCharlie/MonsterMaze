using System;
using System.Collections.Generic;

namespace MonsterMaze.ConsoleView
{
    public static class SpriteSheet
    {
        public static readonly Dictionary<char, Tuple<string, ConsoleColor>> Sprites =
            new Dictionary<char, Tuple<string, ConsoleColor>>
                {
                    {'.', Tuple.Create(".", ConsoleColor.DarkBlue)},
                    {'#', Tuple.Create("#", ConsoleColor.Green)},
                    {'@', Tuple.Create("@", ConsoleColor.Yellow)},
                    {'0', Tuple.Create("0", ConsoleColor.Gray)},
                    {'O', Tuple.Create("O", ConsoleColor.DarkGray)},
                    {'P', Tuple.Create("P", ConsoleColor.Blue)},
                    {'m', Tuple.Create("m", ConsoleColor.Cyan)},
                    {'H', Tuple.Create("H", ConsoleColor.Magenta)},
                    {'h', Tuple.Create("h", ConsoleColor.DarkMagenta)},
                    {'M', Tuple.Create("M", ConsoleColor.Red)},
                    {'b', Tuple.Create("#", ConsoleColor.DarkRed)}
                };
    }
}