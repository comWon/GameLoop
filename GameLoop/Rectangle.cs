using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace GameLoop
{
    class lRectangle
    {

        public Vector Center { get; set; }
        public double Width { get; set; }
        public double Height { get;set;}
        public Color Color { get; set; }

        double _Angle { get; set; }
        public double Angle
        {
            get { return _Angle; }
            set
            {
                double v = value;
                while (v > Math.PI * 2)
                {
                    v -= Math.PI * 2;
                }
                while (v < 0)
                {
                    v += Math.PI * 2;
                }
                _Angle = v;
            }
        }//For non flat rect


        public lRectangle()
        {
            Angle = Math.PI/2;
            Color = new Color(1, 1, 1, 1);
        }

        private double HalfH ()
        {
            return Height / 2;
        }
        private double HalfW()
        {
            return Width / 2;
        }
        
        private Vector Corner (int step)
        {
            if (step <1 || step > 4)
            {
                Exception e = new ArgumentOutOfRangeException("step", "Step should be in range 1-4");
                throw e;
            }
            ///Step 
            ///1: -H, +L
            ///2: -H, -L
            ///3: +H,-L
            ///4: +H,+L
            double Xp1 = HalfH() * Math.Cos(_Angle);
            double Xp2 = HalfW() * Math.Sin(_Angle);
            double Yp1 = HalfH() * Math.Sin(_Angle);
            double Yp2 = HalfW() * Math.Cos(_Angle);

            switch (step)
            {
                case 1:
                    {
                        Xp1 *= -1;
                        Yp1 *= -1;
                        break;
                    }

                case 2:
                    {
                        Xp1 *= -1;
                        Yp1 *= -1;
                        Xp2 *= -1;
                        Yp2 *= -1;
                        break;
                    }
                case 3:
                    {
                        Xp2 *= -1;
                        Yp2 *= -1;
                        break;
                    }
                case 4:
                    {
                        break;
                    }
                default: { break; }
            }

            return new Vector(Center.X + Xp1 + Xp2, Center.Y + Yp1 + Yp2, Center.Z);
        }

        public void Draw()
        {
            GL.Color4(Color.Red, Color.Green, Color.Blue, Color.Alpha);
            GL.Begin(PrimitiveType.LineLoop);
            {
                for (int i = 1; i <= 4; i++)
                {
                    Vector v = Corner(i);
                    GL.Vertex2(v.X, v.Y);
                }
            }
            GL.End();
        }
    }
}
