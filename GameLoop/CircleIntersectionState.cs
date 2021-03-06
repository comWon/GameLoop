﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace GameLoop
{
    class CircleIntersectionState : IGameObject
    {
        Circle _circle = new Circle(Vector.Zero, 200);
        Input _input;
        public CircleIntersectionState(Input input)
        {
            _input = input;
            GL.LineWidth(3);
            GL.Disable(EnableCap.Texture2D);
        }
        #region IGameObject Members

        public void Update(double elapsedTime)
        {
            if (_circle.Intersects(_input.MousePosition))
            {
                _circle.Color = new Color(1, 0, 0, 1);
            }
            else
            {
                // If the circle’s not intersected turn it back to white.
                _circle.Color = new Color(1, 1, 1, 1);
            }

        }

        public void Render()
        {
            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            _circle.Draw();

            // Draw the mouse cursor as a point
            GL.PointSize(5);
            GL.Begin(PrimitiveType.Points);
            {
                GL.Vertex2(_input.MousePosition.X,
                    _input.MousePosition.Y);
            }
            GL.End();

        }

        public void Render(int fbo_screen)
        {
            Render();
        }
        #endregion

    }
}
