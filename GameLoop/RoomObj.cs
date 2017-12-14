using System.Collections.Generic;
using OpenTK.Graphics;
using System.Linq;

namespace GameLoop
{
    internal class RoomObj
    {
        public List<Point> Room { get; set; }
        public Color Color { get; set; }


        public RoomObj(List<Point> room)
        {
            this.Room = room;
            Color = new Color(255, 255, 255, 1);
        }

        public bool Intersects (Point p)
        {
            Vector vPoint = new Vector(p);
            Point t = new Point();
            t = Room[0];

            List<Point> temp = Room.ToList();
            temp.Add(t);
            Point[] RoomArray = temp.ToArray();
            
            bool returnVal = false;
            int intersections = 0;
            //Test Each Side for intersection

            for (int steps=1; steps< RoomArray.Length; steps++)
            {
                int chk = IntersectMaths(ref p, RoomArray, steps);

                intersections = intersections + chk;
            }

            //Check intersections
            if (intersections%2 ==1 ) { returnVal = true; }

            return returnVal;
        }

        private static int IntersectMaths(ref Point p, Point[] RoomArray, int steps)
        {
            //return variable
            int chk = 0;
            //Get eq for side in form aY+bX+c = 0
            float a1 = RoomArray[steps - 1].Y - RoomArray[steps].Y;
            float b1 = RoomArray[steps - 1].X - RoomArray[steps].X;
            float c1 = (RoomArray[steps].X * RoomArray[steps - 1].Y) - (RoomArray[steps - 1].X * RoomArray[steps].Y);

            //Zero to Point Line
            float Zp = c1;
            float pPoint = a1 * p.Y + b1 * p.X + c1;

            //if Sign of Zp == sign pPoint then can stop here, no intersection
            if ((Zp < 0 && pPoint > 0) || (Zp > 0 && pPoint < 0))
            {
                //Expression for line 0 to p == pY + pX + 0 = 0
                float checka = RoomArray[steps - 1].Y * p.Y + RoomArray[steps - 1].X * p.X;
                float checkb = RoomArray[steps].Y * p.Y + RoomArray[steps].X * p.X;
                if ((checka < 0 && checkb > 0) || (checkb < 0 && checka > 0)) chk = 1;
            }

            return chk;
        }
    }
}