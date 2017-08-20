using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLoop
{
    public interface IGameObject
    {
        void Update(double elapsedTime);
        void Render();
        void Render(int fbo_screen);
    }
}
