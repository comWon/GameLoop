using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenGL;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using DevILSharp;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace GameLoop
{
    class TextureManager : IDisposable
    {
        Dictionary<string, Texture> _textureDatabase = new Dictionary<string, Texture>();
        int tempx;
        int tempy;

        public Texture Get(string textureId)
        {
            return _textureDatabase[textureId];
        }

        public void LoadTexture(string imageId, string imagePath)
        {
            Texture t = new Texture();

            t.Id = loadImage(imagePath);
            t.Width = tempx;
            t.Height = tempy;

            if(t.Id >= 0) //0 based Id's
            {
                _textureDatabase.Add(imageId, t);
            }
            tempx = 0;
            tempy = 0;

        }

         int  loadImage(Bitmap image)
        {
            int texID = GL.GenTexture();

            GL.BindTexture(OpenTK.Graphics.OpenGL.TextureTarget.Texture2D, texID);
            BitmapData data = image.LockBits(new System.Drawing.Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(OpenTK.Graphics.OpenGL.TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, OpenTK.Graphics.OpenGL.PixelType.UnsignedByte, data.Scan0);

            tempx = image.Width;
            tempy = image.Height;

            image.UnlockBits(data);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            return texID;
        }

        int loadImage(string filename)
        {
            try
            {
                Bitmap file = new Bitmap(filename);
                return loadImage(file);
            }
            catch (FileNotFoundException e)
            {
                return -1;
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            foreach (Texture t in _textureDatabase.Values)
            {
                GL.DeleteTexture(t.Id );
            }
        }

        #endregion

    }
}
