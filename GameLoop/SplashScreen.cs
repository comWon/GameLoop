using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGL;

namespace GameLoop
{
    public class SplashScreen : IGameObject
    {

        StateSystem _system;
        double _delayInSeconds = 3;

        public SplashScreen(StateSystem system)
        {
            _system = system;
        }

        #region Interface objects
        public void Render()
        {
            //White Screen (Check colours 1-256 or 0 to 255?)
            Gl.ClearColor(1, 1, 1, 1);
            Gl.Clear(ClearBufferMask.ColorBufferBit);
            Gl.Finish();
        }

        public void Update(double elapsedTime)
        {
            _delayInSeconds -= elapsedTime;
            if (_delayInSeconds <= 0)
            {
                _delayInSeconds = 3;
                _system.ChangeState("titleMenu");
            }
        }
        #endregion
    }
}

