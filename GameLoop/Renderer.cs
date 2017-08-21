using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace GameLoop
{
    class Renderer
    {
        TextureManager _textureManager;
        public Renderer()
        {
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
       
        }

        public Renderer(TextureManager textureManager) : this()
        {
            _textureManager = textureManager;
        }

        public void DrawImmediateModeVertex(Vector position, Color color, Point uvs)
        {
            GL.Color4(color.Red, color.Green, color.Blue, color.Alpha);
            GL.TexCoord2(uvs.X, uvs.Y);
            GL.Vertex3(position.X, position.Y, position.Z);

            }

        Batch _batch = new Batch();

        public void DrawSprite(Sprite sprite)
        {
            _batch.AddSprite(sprite);
        }

        public void Render()
        {
            _batch.Draw();
        }



        public void DrawText(Text text)
        {
            foreach (CharacterSprite c in text.CharacterSprites)
            {
                DrawSprite(c.Sprite);
            }

        }
    }
}
