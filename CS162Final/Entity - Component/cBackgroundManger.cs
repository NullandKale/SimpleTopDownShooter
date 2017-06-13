using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace nullEngine.Entity___Component
{
    class cBackgroundManger : iComponent
    {
        private Point displayedChunk;

        public cBackgroundManger()
        {
            displayedChunk = Managers.WorldManager.man.currentChunkPos;
        }

        public void Run(renderable r)
        {
            if (displayedChunk != Managers.WorldManager.man.currentChunkPos)
            {
                r.tex = Managers.WorldManager.worldTex;
                displayedChunk = Managers.WorldManager.man.currentChunkPos;
            }
        }
    }
}
