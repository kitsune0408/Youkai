namespace YoukaiKingdom.Logic.Helpers
{
    using System;

    public static class Utility
    {
        private static Random rnd = new Random();
        public static int GetRandom(int from, int to)
        {
            return rnd.Next(from, to);
        }
    }
}


