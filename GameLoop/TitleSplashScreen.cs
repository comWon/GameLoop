using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLoop
{
    class TitleSplashScreen : IGameObject
    {
        public void Render()
        {
            Console.WriteLine("Render Title Screen");
        }

        public void Update(double elapsedTime)
        {
            Console.WriteLine("Update Title Screen");
        }
    }
}
