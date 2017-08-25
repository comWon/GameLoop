
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
using OpenTK.Input;

namespace GameLoop
{
    public class GameWindow : OpenTK.GameWindow
    {
        
        PreciseTimer preciseTimer = new PreciseTimer();
        //Screen Settings
        int _ScreenX = 1920;
        int _ScreenY = 1080;
        bool _fullScreen = false;
        private bool _resourcesLoaded = false;
        private bool _imageLibraryLoaded = false;

        //INput Controls
        Input _input = new Input();
        MouseDevice mouse = new MouseDevice();


        float time = 0.0f;
        private const int MAX_LIGHTS = 5;

        const int TextureSize = 256;
        private int fbo_screen;


        StateSystem _system = new StateSystem();
        TextureManager _textureManager = new TextureManager();


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Title = "Generic Title";

            GL.ClearColor(new Color4(0, 0, 128, 0));
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Enable(EnableCap.Texture2D);
            
           

            LoaderManager();
        }

        public void LoaderManager()
        {

            //Graphics loader

            _ScreenX = ClientSize.Height;
            _ScreenY = ClientSize.Width;
            SetUp2Dgraphics(_ScreenX, _ScreenY);
            LoadImageLibrary();


            //Generate Working Properties

            LoadStateSystem();

            //Start StateSystem (splash screen)
            _system.ChangeState("TestSprite");




        }

        private void LoadImageLibrary()
        {
            _textureManager.LoadTexture("face", @"Assets\face.tif");
            _textureManager.LoadTexture("alphaface", @"Assets\face_alpha.tif");
            _textureManager.LoadTexture("font", @"Assets\font.tif");

            _imageLibraryLoaded = true;
        }

        private void LoadStateSystem()
        {
            _system.AddState("splash", new SplashScreen(_system));
            _system.AddState("titleMenu", new TitleSplashScreen(_system));
            _system.AddState("sprite", new DrawSpriteState(_textureManager));
            _system.AddState("TestSprite", new TestSpriteClassState(_textureManager) { Input = _input });
            _system.AddState("TextTest", new TextRenderState(_textureManager));
            _system.AddState("Bounce", new CharacterBounceState(_textureManager));

            _resourcesLoaded = true;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
          //  GL.BindFramebuffer(FramebufferTarget.Framebuffer, fbo_screen);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GenericUpdate();
            _system.Update(preciseTimer.GetElapsedTime());
            _system.Render(); //fbo_screen
            SwapBuffers();
        }

        private void GenericUpdate()
        {
            //   throw new NotImplementedException();
            _input.MousePosition = new Point(
                mouse.X - this.Size.Width / 2,
                mouse.Y - this.Size.Height / 2
                );
                
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
            
            //GL.Viewport(0, 0, (int)height, (int)width);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-halfWidth, halfWidth, -halfHeight, halfHeight, -100, 100);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

        }
    }
}
