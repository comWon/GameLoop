using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Input;

namespace GameLoop
{
    public class Input
    {
        public Point MousePosition { get; set; }
        public KeyboardState KeyboardState { get; set; }
        public MouseState MouseState { get; set; }
    }
}
