using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLoop
{
    class StateCharWindow : IGameObject
    {
        private TextureManager _textureManager;

        public StateCharWindow()
        {
        }

        public StateCharWindow(TextureManager textureManager): this()
        {
            _textureManager = textureManager;
        }

        public void Render()
        {
            throw new NotImplementedException();
        }

        public void Render(int fbo_screen)
        {
            throw new NotImplementedException();
        }

        public void Update(double elapsedTime)
        {
            throw new NotImplementedException();
        }
    }
}
