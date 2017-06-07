using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nullEngine.Managers
{
    public class WorldManager
    {
        public static WorldManager man;

        public TextureAtlas overworldTileAtlas;

        public WorldManager()
        {
            if(man == null)
            {
                man = this;
            }
            else
            {
                throw new SingletonException(this);
            }

            overworldTileAtlas = new TextureAtlas("Content/overworld.png", 21, 9, 16, 16, 0);
        }
    }
}
