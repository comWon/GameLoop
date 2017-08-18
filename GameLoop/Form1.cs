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
            _fastLoop = new FastLoop(GameLoop);
            LoadStateSystem();
            InitializeComponent();
            _openGLControl.InitializeLifetimeService();

            _system.ChangeState("splash");

            if (_fullscreen)
            {
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
            }
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
        }
    }
}
