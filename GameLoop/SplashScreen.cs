using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

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
            GL.ClearColor(1, 1, 1, 1);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Finish();
        }

        public void Render(int fbo_screen)
        {
            GL.BindFramebuffer(FramebufferTarget.FramebufferExt, fbo_screen);
            Render();
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

