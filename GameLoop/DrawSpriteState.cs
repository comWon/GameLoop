using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace GameLoop
{
    class DrawSpriteState : IGameObject
    {
        TextureManager _textureManager;
        Texture _texture;
        public DrawSpriteState(TextureManager textureManager)
        {
            _textureManager = textureManager;
        }


        #region IGameObject Parts 
        public void Render()
        {
            //Buffer clear 

            GL.Clear(OpenTK.Graphics.OpenGL.ClearBufferMask.ColorBufferBit | OpenTK.Graphics.OpenGL.ClearBufferMask.DepthBufferBit);


            //Camera View 
            Matrix4 modelview = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
            GL.MatrixMode(OpenTK.Graphics.OpenGL.MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);




            //Size
            double height = 200;
            double width = 200;
            double halfHeight = height / 2;
            double halfWidth = width / 2;

            //Position
            double x = 0;
            double y = 0;
            double z = 0;

            //Texture
            GL.Enable(EnableCap.Texture2D);
            _texture = _textureManager.Get("face");


            //TrianGLes
            GL.BindTexture(TextureTarget.Texture2D, (uint)_texture.Id);
            GL.Begin(PrimitiveType.Triangles);
                {
                GL.TexCoord2(0, 0);
                GL.Vertex3(x - halfWidth, y + halfHeight, z);
                GL.TexCoord2(1, 0);
                GL.Vertex3(x + halfWidth, y + halfHeight, z);
                GL.TexCoord2(0, 1);
                GL.Vertex3(x - halfWidth, y - halfHeight, z);

                GL.TexCoord2(1, 0);
                GL.Vertex3(x + halfWidth, y + halfHeight, z);
                GL.TexCoord2(1, 1);
                GL.Vertex3(x + halfWidth, y - halfHeight, z);
                GL.TexCoord2(0, 1);
                GL.Vertex3(x - halfHeight, y - halfWidth, z);
            }
            GL.End();
            
        }

        public void Render(int fbo_screen)
        {
            GL.BindFramebuffer(FramebufferTarget.FramebufferExt, fbo_screen);
            Render();
        }

        public void Update(double elapsedTime)
        {
            
        }
#endregion
    }
}
