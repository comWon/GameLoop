using OpenGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace GameLoop
{
    public partial class Form1 : Form
    {
        FastLoop _fastLoop;
        bool _fullscreen = false;
    
        StateSystem _system = new StateSystem();

        public GlControl OpenGLControl { get => _openGLControl; set => _openGLControl = value; }

        public Form1()
        {
            //Generate Working Properties
            LoadStateSystem();
            InitializeComponent();
            SetUp2Dgraphics(ClientSize.Width, ClientSize.Height);

            //Start OpenGL
            _openGLControl.InitializeLifetimeService();

            //Start StateSystem (splash screen)
            _system.ChangeState("splash");

            //Set Starting Sizes
            if (_fullscreen)
            {
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                ClientSize = new Size(1280, 960);
            }

            //Start GameLoop
            _fastLoop = new FastLoop(GameLoop);
        }

        private void LoadStateSystem()
        {
            _system.AddState("splash", new SplashScreen(_system));
            _system.AddState("titleMenu", new TitleSplashScreen(_system));

        }

        void GameLoop(double elapsedTime)
        {
            _system.Update(elapsedTime);
            _system.Render();
            _openGLControl.Refresh();

        }

        protected override void OnClientSizeChanged(EventArgs e)
        {
            base.OnClientSizeChanged(e);
            Gl.Viewport(0, 0, this.ClientSize.Width, this.ClientSize.Height);
            SetUp2Dgraphics(ClientSize.Width, ClientSize.Height);
        }

        private void SetUp2Dgraphics(double width, double height)
        {
            double halfWidth = width / 2;
            double halfHeight = height / 2;

            Gl.MatrixMode(MatrixMode.Projection);
            Gl.LoadIdentity();
            Gl.Ortho(-halfWidth, halfWidth, -halfHeight, halfHeight, -100, 100);
            Gl.MatrixMode(MatrixMode.Modelview);
            Gl.LoadIdentity();
        }
    }
}
