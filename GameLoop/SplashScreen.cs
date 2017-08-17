using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLoop
{
    class SplashScreen : IGameObject
    {
        public void Render()
        {
           Console.WriteLine("Render Command in Splash Screen");
        }

        public void Update(double elapsedTime)
        {
            Console.WriteLine("Update Command in Splash Screen");
        }
    }
}
