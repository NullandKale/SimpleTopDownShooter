using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace nullEngine
{
    [Serializable]
    public class WorldData
    {
        int seed;
        int ChunkSize;
        int WorldSize;
        VillageData[] Villaga;
        string TAtlasLoc;

        public TextureAtlas tAtlas
        {
            get
            {
                if(tAtlasCache.notEmpty)
                {
                    return tAtlasCache;
                }
                else
                {
                    tAtlasCache = new TextureAtlas("Content/overworld.png", 21, 9, 16, 16, 0);
                    return tAtlasCache;
                }
            }
        }

        [NonSerialized]
        private TextureAtlas tAtlasCache;


    }

    [Serializable]
    public class VillageData
    {
        Point Loc;
        Point ConnectedVillageLoc;
    }
}
