﻿using System;
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

        public TestSpriteClassState(TextureManager textureManager)
        {
            _textureManager = textureManager;
            _testSprite.Texture = _textureManager.Get("alphaface");
            _testSprite.SetHeight(15);
            _testSprite.SetWidth(15);

            _testSprite2.Texture = _textureManager.Get("alphaface");
            _testSprite2.SetHeight(200);
            _testSprite2.SetWidth(200);
            _testSprite2.SetColor(new Color(1, 0, 0, (float)0.2));
        }

        public void Render()
        {
            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            GL.Clear( ClearBufferMask.ColorBufferBit );
            _renderer.DrawSprite(_testSprite2);
            _renderer.DrawSprite(_testSprite);
            GL.Finish();
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
