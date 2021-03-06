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

        Font _font;
        Text _header;
        Text _position;
        Circle _Marker = new Circle();
        private double _maxPulse=200;

        lRectangle TextBox = new lRectangle();

        public TestSpriteClassState(TextureManager textureManager)
        {
            _textureManager = textureManager;
            _testSprite.Texture = _textureManager.Get("alphaface");
            _testSprite.SetHeight(15);
            _testSprite.SetWidth(15);

            _testSprite2.Texture = _textureManager.Get("face");
            _testSprite2.SetHeight(200);
            _testSprite2.SetWidth(200);
            _testSprite2.SetColor(new Color(1, 0, 0, (float)1));

            _font = new Font(texture: textureManager.Get("font"),
                            characterData: FontParser.Parse(@"Assets\font.fnt"));

            _header = new Text("Hello", _font);
            _header.SetScale(1);
            _header.Position(0, 0);

            _position = new Text("x: 0, y: 0", _font);
            _position.SetScale(1);
            _position.Position(0,0);

            _Marker = new Circle(_testSprite.GetPosition(), _maxPulse / 2)
            {
                Color = new Color(1.0f, 0.0f, 0.0f, 1.0f)
            };

            TextBox.Center = new Vector(_header.Height / 2 , _header.Width / 2 , 0); //-150+_header.Height / 2 ,-50+ _header.Width / 2 , 0)
            TextBox.Height = _header.Height + 10.0;
            TextBox.Width = _header.Width + 10.0;
                
        }

        public Input Input { get; internal set; }

        public void Render()
        {
            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            GL.Clear( ClearBufferMask.ColorBufferBit );

            //And Text render from here
            GL.BindTexture(TextureTarget.Texture2D, _textureManager.Get("font").Id);
            _renderer.DrawText(_header);
            _renderer.Render();

            _position = new Text("x: " + _testSprite.VertexPositions[0] + ", y: "+ _testSprite.VertexPositions[1], _font);

            //None Textured bits 
            GL.BindTexture(TextureTarget.Texture2D, 0);
            if (_Marker.Intersects(Input.MousePosition))
            {
                _Marker.Color = new Color(0, 1, 0, 1);
                _testSprite2.SetColor(new Color(0, 1, 0, 1));
            }
            else
            {
                _Marker.Color = new Color(1, 0, 0, 1);
                _testSprite2.SetColor(new Color(1, 0, 0, 1));
            }
                _Marker.Draw();

            //Recalc TextBox
            TextBox.Center = new Vector(_header.Center.X, _header.Center.Y, 0);
            TextBox.Height = _header.Height + 10.0;
            TextBox.Width = _header.Width + 10.0;

            TextBox.Draw();

            //Need to render batches 1 texture at a time
            GL.BindTexture(TextureTarget.Texture2D, _testSprite2.Texture.Id);
            _renderer.DrawSprite(_testSprite2);
            _renderer.Render();

            GL.BindTexture(TextureTarget.Texture2D, _testSprite.Texture.Id);
            _renderer.DrawSprite(_testSprite);
            _renderer.Render();




        }

        public void Render(int fbo_screen)
        {
            GL.BindFramebuffer(FramebufferTarget.FramebufferExt, fbo_screen);
            Render();
        }

        public void Update(double elapsedTime)
        {
            //Check for move request
            if (Input.MouseState[OpenTK.Input.MouseButton.Left])
            {
                if (_Marker.Intersects(Input.MousePosition))
                {
                    _testSprite.SetPosition(new Vector(Input.MousePosition.X, Input.MousePosition.Y, 0));
                    _testSprite2.SetPosition(new Vector(Input.MousePosition.X, Input.MousePosition.Y, 0));
                    _Marker = new Circle(new Vector(Input.MousePosition.X, Input.MousePosition.Y, 0), _maxPulse/2);
                    _Pulse = _maxPulse;
                    _Direction = -1;

                }
            }

            //Pulse markers

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
                if (_Pulse > _maxPulse)
                {
                    _Pulse = _maxPulse;
                    _Direction = -1;
                }
            }

            _testSprite2.SetHeight((float)_Pulse);
            _testSprite2.SetWidth((float)_Pulse);
        }

    }
}
