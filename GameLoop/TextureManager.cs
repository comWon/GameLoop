using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;


namespace GameLoop
{
    class TextureManager : IDisposable
    {
        Dictionary<string, Texture> _textureDatabase = new Dictionary<string, Texture>();
  
        Bitmap bmpImg;
        public Texture Get(string textureId)
        {
            return _textureDatabase[textureId];
        }

        public void LoadTexture(string imageId, string imagePath)
        {
          
            Texture t = new Texture();
            int[] img = loadImage(imagePath);
            t.Id = img[0];
            t.Width = img[1];
            t.Height = img[2];

            if(t.Id > 0) 
            {
                _textureDatabase.Add(imageId, t);
            }

        }

         int[]  loadImage(Bitmap image)
        {

            //int[] texIDa = new int[1];
            //GL.CreateTextures(TextureTarget.Texture2D, 1, texIDa);
            //Console.WriteLine(GL.GetError());

            //int texID = texIDa[0];
            //Console.WriteLine(texID);

            int texID = GL.GenTexture();

            GL.BindTexture(TextureTarget.Texture2D,  texID);
            Console.WriteLine(GL.GetError());
            Console.WriteLine(GL.GetInteger(GetPName.TextureBinding2D));

            BitmapData data = image.LockBits(new Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);


            GL.TexImage2D( TextureTarget.Texture2D,  0,  PixelInternalFormat.Rgba,  data.Width,  data.Height,  0,  OpenTK.Graphics.OpenGL.PixelFormat.Bgra,  PixelType.UnsignedByte,  data.Scan0);
            
            image.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
           
           
            return new int[] { texID, image.Width, image.Height } ;
        }

        int[] loadImage(string filename)
        {
            try
            {
                bmpImg = new Bitmap(filename);
                return loadImage(bmpImg);
            }
            catch (FileNotFoundException e)
            {
                return new int [] { -1,0,0};
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
