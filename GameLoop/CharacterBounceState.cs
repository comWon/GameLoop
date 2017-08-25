using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace GameLoop
{
    class CharacterBounceState : IGameObject
    {
         Font _font;
        Text _text;
        Renderer _renderer = new Renderer();
        double _totalTime = 0;

        public CharacterBounceState(TextureManager manager)
        {
            _font = new Font(manager.Get("font"), FontParser.Parse(@"Assets\font.fnt"));
            _text = new Text("Hello", _font);
        }

        public void Update(double elapsedTime)
        {
            double frequency = 7;

            int xAdvance = 0;
            foreach (CharacterSprite cs in _text.CharacterSprites)
            {
                Vector position = cs.Sprite.GetPosition();
                position.Y = 0 + Math.Sin((_totalTime + xAdvance) * frequency) * 25;
                cs.Sprite.SetPosition(position);
                xAdvance++;
            }

            _totalTime += elapsedTime;

        }

        public void Render()
        {
            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            _renderer.DrawText(_text);
            _renderer.Render();
        }

        public void Render(int fbo_screen)
        {
            Render();
        }
    }
}
