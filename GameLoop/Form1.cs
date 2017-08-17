using OpenGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace StartingGraphics
{
    public partial class Form1 : Form
    {
        FastLoop _fastLoop;
        bool _fullscreen = false;

        public Form1()
        {
            _fastLoop = new FastLoop(GameLoop);

            InitializeComponent();
            _openGLControl.InitializeLifetimeService();


            if (_fullscreen)
            {
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
            }
        }

        void GameLoop(double elapsedTime)
        {
            Gl.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            Gl.Clear( ClearBufferMask.ColorBufferBit);

            // Uncomment this line to make the point bigger
            //Gl.glPointSize(5.0f); 

            //Gl.Begin(Gl.POINTS);
            //{
            //    Gl.Vertex3(0, 0, 1);
            //}
            //Gl.End();

            // Uncomment these lines to draw a triangle

            //// Uncomment this line to rotate the triangle
            // //Gl.Rotate(10 * elapsedTime, 0, 1, 0);
            //Gl.Begin((PrimitiveType)Gl.TRIANGLES);
            //{
            //    Gl.Color3(1.0, 0.0, 0.0);
            //    Gl.Vertex3(-0.5, 0, 0);
            //    Gl.Color3(0.0, 1.0, 0.0);
            //    Gl.Vertex3(0.5, 0, 0);
            //    Gl.Color3(0.0, 0.0, 1.0);
            //    Gl.Vertex3(0, 0.5, 0);
            //}
            //Gl.End();

            

            Gl.Finish();
            _openGLControl.Refresh();

        }
    }
}
