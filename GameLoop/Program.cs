using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GameLoop
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (GameWindow game = new GameWindow())
            {
                /*The Run method of the GameWindow has multiple overloads.
                 * With a sinGLe float parameter, Run will give your window 30 UpdateFrame events a second, and as many RenderFrame events per second as the computer will process.*/
                game.Run(30.0);
            }
        }
    }
}
