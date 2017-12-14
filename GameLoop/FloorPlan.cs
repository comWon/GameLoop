using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLoop
{
    class FloorPlan : IGameObject
    {
        //basic Graphics controls
        TextureManager _textureManager;
        Renderer _renderer = new Renderer();


        public PlayerObject playerObject { get; set; }
        private List<RoomObj> corners { get; set; }

        public Input Input { get; internal set; }

        public FloorPlan (TextureManager textureManager)
        {
            TextureManager _textureManager = textureManager;
            corners = BasicRoom();
        }

        public FloorPlan(string MapName, TextureManager textureManager)
        {
            TextureManager _textureManager = textureManager;

            throw new NotImplementedException();
        }

        private List<RoomObj> BasicRoom()
        {
            //Creates a 450 pixel Square Zone
            List<RoomObj> returnVal = new List<RoomObj>();

            List<Point> room = new List<Point>();

            room.Add(new Point(50, 50));
            room.Add(new Point(500, 50));
            room.Add(new Point(500, 500));
            room.Add(new Point(50, 500));
            room.Add(new Point(50, 50));

            RoomObj rm = new RoomObj(room);
            returnVal.Add(rm);

            room = new List<Point>();
            room.Add(new Point(-250, 50));
            room.Add(new Point( 0, 50));
            room.Add(new Point(0, 100));
            room.Add(new Point(-250, 100));
            room.Add(new Point(-250, 50));

            return returnVal;
        }

        public void Render()
        {
            //Frame Clear
            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit);
           
            //None Textured bits 
            GL.BindTexture(TextureTarget.Texture2D, 0);
            
            
            //RoomLines 

            foreach (var c in corners)
            {
                GL.Color4(c.Color.Red,c.Color.Green,c.Color.Blue, c.Color.Alpha);
                GL.Begin(PrimitiveType.LineLoop);
                {
                    foreach (Point cn in c.Room) 
                    {
                        Vector v = new Vector(cn);
                        GL.Vertex2(v.X, v.Y);
                    }
                }
                GL.End();
                
            }
           _renderer.Render();
            //throw new NotImplementedException();
        }

        public void Render(int fbo_screen)
        {
            Render();
        }

        public void Update(double elapsedTime)
        {
            foreach (RoomObj r in corners) {
                if (r.Intersects(Input.MousePosition)) { r.Color = new Color(0, 255, 0, 1); } else { r.Color = new Color(255, 255, 255, 1); }
                    }
            //throw new NotImplementedException();
        }
    }
}
