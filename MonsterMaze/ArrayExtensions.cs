namespace MonsterMaze
{
    public static class ArrayExtensions
    {
        public static int GetHeight(this char[,] array)
        {
            return array.GetUpperBound(1);
        } 
        public static int GetWidth(this char[,] array)
        {
            return array.GetUpperBound(0);
        }
    }
}