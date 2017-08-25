using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace GameLoop
{
    public class Circle
    {
        Vector Position { get; set; }
        double Radius { get; set; }
        Color _color = new Color(1, 1, 1, 1);
        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public Circle()
        {
            Position = Vector.Zero;
            Radius = 1;
        }

        public Circle(Vector position, double radius)
        {
            Position = position;
            Radius = radius;
        }

        public void Draw()
        {
            GL.Color3(_color.Red, _color.Green, _color.Blue);

            // Determines how round the circle will appear.
            int vertexAmount = 100;
            double twoPI = 2.0 * Math.PI;

            // A line loop connects all the vertices with lines
            // The last vertex is connected to the first vertex
            // to make a loop.
            GL.Begin(PrimitiveType.LineLoop);
            {
                for (int i = 0; i <= vertexAmount; i++)
                {
                    double xPos = Position.X + Radius * Math.Cos(i * twoPI / vertexAmount);
                    double yPos = Position.Y + Radius * Math.Sin(i * twoPI / vertexAmount);
                    GL.Vertex2(xPos, yPos);
                }
            }
            GL.End();
        }

        public bool Intersects(Point point)
        {
            // Change point to a vector
            Vector vPoint = new Vector(point.X, point.Y, 0);
            Vector vFromCircleToPoint = Position - vPoint;
            double distance = vFromCircleToPoint.Length();

            if (distance > Radius)
            {
                return false;
            }
            return true;

        }
    }

}
