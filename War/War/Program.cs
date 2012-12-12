using System;

namespace War
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (War game = new War())
            {
                game.Run();
            }
        }
    }
#endif
}

