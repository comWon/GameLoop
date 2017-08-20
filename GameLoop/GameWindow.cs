
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using OpenTK.Platform;
using OpenTK;

namespace GameLoop
{
    public partial class GameWindow : OpenTK.GameWindow
    {
        //FastLoop _fastLoop;
        PreciseTimer preciseTimer = new PreciseTimer();

        bool _fullscreen = false;
        private bool resourcesLoaded = false;

        float time = 0.0f;
        private const int MAX_LIGHTS = 5;

        /// <summary>
        /// Width/Height of the texture for the TV screen
        /// </summary>
        const int TextureSize = 256;
        private int fbo_screen;


        StateSystem _system = new StateSystem();
        TextureManager _textureManager = new TextureManager();


        protected override void  OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Title = "Generic Title";

            GL.ClearColor(Color.CornflowerBlue);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            LoaderManager();
        }

        public void LoaderManager()
        {


            //Generate Working Properties

            LoadStateSystem();
            
            //Start GameLoop
            //_fastLoop = new FastLoop(GameLoop);

            //Graphics loader
            SetUp2Dgraphics(1280, 960);
            LoadImageLibrary();

            //Start StateSystem (splash screen)
            _system.ChangeState("sprite");

            ////Set Starting Sizes
            //if (_fullscreen)
            //{
            //    FormBorderStyle = FormBorderStyle.None;
            //    WindowState = FormWindowState.Maximized;
            //}
            //else
            //{
            //    ClientSize = new Size(1280, 960);
            //    SetUp2Dgraphics(1280, 960);
            //}


        }

        private void LoadImageLibrary()
        {
            

            _textureManager.LoadTexture("face", @"Assets\face.tif");

        }

        private void LoadStateSystem()
        {
            _system.AddState("splash", new SplashScreen(_system));
            _system.AddState("titleMenu", new TitleSplashScreen(_system));
            _system.AddState("sprite", new DrawSpriteState(_textureManager));
        }

        //void GameLoop(double elapsedTime)
        //{
        //    _system.Update(elapsedTime);
        //    _system.Render();

        //}

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, fbo_screen);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            _system.Update(preciseTimer.GetElapsedTime());
            _system.Render(fbo_screen);
            SwapBuffers();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, this.ClientSize.Width, this.ClientSize.Height);
            SetUp2Dgraphics(ClientSize.Width, ClientSize.Height);
        }

        private void SetUp2Dgraphics(double width, double height)
        {
            double halfWidth = width / 2;
            double halfHeight = height / 2;
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-halfWidth, halfWidth, -halfHeight, halfHeight, -100, 100);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
        }
    }
}
