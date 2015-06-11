using System;

namespace YoukaiKingdom
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            //comment by nstanevski - just to test GitHub collaboration
            using (MainGame game = new MainGame())
            {
                game.Run();
            }
        }
    }
#endif
}

