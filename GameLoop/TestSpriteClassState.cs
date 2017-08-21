using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace GameLoop
{
    class TestSpriteClassState : IGameObject
    {
        TextureManager _textureManager;
        Renderer _renderer = new Renderer();
        Sprite _testSprite = new Sprite();

        Sprite _testSprite2 = new Sprite();
        int _Direction = -1;
        double _Pulse = 200;

        Font _font;
        Text _header;


        public TestSpriteClassState(TextureManager textureManager)
        {
            _textureManager = textureManager;
            _testSprite.Texture = _textureManager.Get("alphaface");
            _testSprite.SetHeight(15);
            _testSprite.SetWidth(15);

            _testSprite2.Texture = _textureManager.Get("alphaface");
            _testSprite2.SetHeight(200);
            _testSprite2.SetWidth(200);
            _testSprite2.SetColor(new Color(1, 0, 0, (float)1));

            _font = new Font(textureManager.Get("font"),
    FontParser.Parse(@"Assets\font.fnt"));
            _header = new Text("Hello", _font);
            _header.SetScale(.25);
            _header.Position(-150, -50);
        }

        public void Render()
        {
            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            GL.Clear( ClearBufferMask.ColorBufferBit );

            //Need to render batches 1 text at a time
            GL.BindTexture(TextureTarget.Texture2D, _testSprite2.Texture.Id);
            _renderer.DrawSprite(_testSprite2);
            //_renderer.Render();

            //GL.BindTexture(TextureTarget.Texture2D, _testSprite.Texture.Id);
            _renderer.DrawSprite(_testSprite);
            _renderer.Render();

            //And Text render from here
            GL.BindTexture(TextureTarget.Texture2D, _textureManager.Get("font").Id);
            _renderer.DrawText(_header);
            _renderer.Render();
        }

        public void Render(int fbo_screen)
        {
            GL.BindFramebuffer(FramebufferTarget.FramebufferExt, fbo_screen);
            Render();
        }

        public void Update(double elapsedTime)
        {
            if (_Direction == -1)
            {
                _Pulse -= elapsedTime * 100;
                if (_Pulse < 0)
                {
                    _Pulse = 0;
                    _Direction = 1;
                }
            } else
            {
                _Pulse += elapsedTime * 100;
                if (_Pulse > 200)
                {
                    _Pulse = 200;
                    _Direction = -1;
                }
            }

            _testSprite2.SetHeight((float)_Pulse);
            _testSprite2.SetWidth((float)_Pulse);
        }

    }
}
