using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace GameLoop
{
    class coOrdMatchStatecs : IGameObject
    {
        private TextureManager _textureManager;
        public Input input { get; set; }
        public int windowSizeX {get;set;}
        public int windowSizeY { get; set; }

        public coOrdMatchStatecs(TextureManager textureManager)
        {
            _textureManager = textureManager;
            
        }

        public void Render()
        {
            GL.BindTexture(TextureTarget.Texture2D, 0);
            
            if ( this.windowSizeX * windowSizeY != 0)
            {
                //xLoop
                

                for (int i = -windowSizeX/20; i <= windowSizeX/20; i++)
                {
                    GL.Begin(PrimitiveType.Lines);
                    GL.Vertex3(i * 10, -windowSizeY / 2, 0);
                    GL.Vertex3(i * 10, windowSizeY / 2, 0);
                    GL.End();
                }

                for (int i = -windowSizeY / 20; i <= windowSizeY / 20; i++)
                {
                    GL.Begin(PrimitiveType.Lines);
                    GL.Vertex3(-windowSizeX/2,i * 10, 0);
                    GL.Vertex3(windowSizeX/2,i * 10, 0);
                    GL.End();
                }


            }
        }

        public void Render(int fbo_screen)
        {
            Render();
        }

        public void Update(double elapsedTime)
        {
            throw new NotImplementedException();
        }
    }
}
