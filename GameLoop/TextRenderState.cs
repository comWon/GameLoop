using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace GameLoop
{
    class TextRenderState : IGameObject
    {
        TextureManager _textureManager;
        Font _font;
        Text _helloWorld;
        Renderer _renderer = new Renderer();

        public TextRenderState(TextureManager textureManager)
        {
            _textureManager = textureManager;
            _font = new Font(textureManager.Get("font"),
                FontParser.Parse(@"Assets\font.fnt"));
            _helloWorld = new Text("Hello", _font);
        }

        public void Render()
        {
            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            GL.Clear( ClearBufferMask.ColorBufferBit);
            _renderer.DrawText(_helloWorld);
        }

        public void Render(int fbo_screen)
        {
            GL.BindFramebuffer(FramebufferTarget.FramebufferExt, fbo_screen);
            Render();
        }

        public void Update(double elapsedTime)
        {
        }
    }

}
