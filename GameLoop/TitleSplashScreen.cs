using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGL;

namespace GameLoop
{
    public class TitleSplashScreen : IGameObject
    {
        StateSystem _system;
        double _currentRotation = 0;

        public TitleSplashScreen(StateSystem system)
        {
            _system = system;
        }

        #region Interface objects
        public void Render()
        {
            //White Screen (Check colours 1-256 or 0 to 255?)
            Gl.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            Gl.Clear(ClearBufferMask.ColorBufferBit);

            Gl.PointSize(5.0f);

            Gl.Rotate(_currentRotation, 0, 1, 0);
            Gl.Begin(PrimitiveType.TriangleStrip);
            {
                Gl.Color4(1.0, 0.0, 0.0, 0.5);
                Gl.Vertex3(.5, 0.5, 0.5); //A
                Gl.Color4(0.0, 1.0, 0.0, 0.5);
                Gl.Vertex3(-0.5, -0.5, 0.5); //B
                 Gl.Color4(0.0, 0.0, 1.0, 0.5);
                Gl.Vertex3(-0.5, 0.5, -0.5); //C
                Gl.Color4(1.0, 1.0, 1.0, 0.5);
                Gl.Vertex3(.5, -.5, -.5); //D
                Gl.Color4(1.0, 0.0, 0.0, 0.5);
                Gl.Vertex3(.5, 0.5, 0.5); //A
                Gl.Color4(0.0, 1.0, 0.0, 0.5);
                Gl.Vertex3(-0.5, -0.5, 0.5); //B
                Gl.Color4(0.0, 1.0, 0.0, 0.5);
            }
            Gl.End();

            Gl.Finish();
        }

        //Note no graphical code at all in Update - all handled in render. steps may becalled out of sync
        //(in which case the render would show an unchanged screen till an update is called)
        public void Update(double elapsedTime)
        {
            _currentRotation = 10 * elapsedTime;
        }
        #endregion
    }
}
