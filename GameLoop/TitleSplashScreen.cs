using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace GameLoop
{
    public class TitleSplashScreen : IGameObject
    {
        StateSystem _system;
        double _currentRotation = 0;
        double _Multiplier = 50;
        public TitleSplashScreen(StateSystem system)
        {
            _system = system;
        }

        #region Interface objects
        public void Render()
        {
            //White Screen (Check colours 1-256 or 0 to 255?)
            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.PointSize(5.0f);

            GL.Rotate(_currentRotation, 0, 1, 0);
            GL.Begin(PrimitiveType.TriangleStrip);
            {
                GL.Color4(1.0, 0.0, 0.0, 0.5);
                GL.Vertex3(.5* _Multiplier, 0.5* _Multiplier, 0.5* _Multiplier); //A
                GL.Color4(0.0, 1.0, 0.0, 0.5);
                GL.Vertex3(-0.5*_Multiplier, -0.5 * _Multiplier, 0.5 * _Multiplier); //B
                GL.Color4(0.0, 0.0, 1.0, 0.5);
                GL.Vertex3(-0.5 * _Multiplier, 0.5 * _Multiplier, -0.5 * _Multiplier); //C
                GL.Color4(1.0, 1.0, 1.0, 0.5);
                GL.Vertex3(.5 * _Multiplier, -.5 * _Multiplier, -.5 * _Multiplier); //D
                GL.Color4(1.0, 0.0, 0.0, 0.5*_Multiplier);
                GL.Vertex3(.5 * _Multiplier, 0.5 * _Multiplier, 0.5 * _Multiplier); //A
                GL.Color4(0.0, 1.0, 0.0, 0.5);
                GL.Vertex3(-0.5 * _Multiplier, -0.5 * _Multiplier, 0.5 * _Multiplier); //B
                
            }
            GL.End();

            GL.Finish();
        }

        public void Render(int fbo_screen)
        {
            GL.BindFramebuffer(FramebufferTarget.FramebufferExt, fbo_screen);
            Render();
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
