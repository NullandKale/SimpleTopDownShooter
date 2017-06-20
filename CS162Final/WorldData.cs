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
        public int seed;
        public int chunkSize;
        public int worldSize;
        public List<VillageData> Villages;
        public TextureAtlas tAtlas
        {
            get
            {
                if (tAtlasCache != null)
                {
                    return tAtlasCache;
                }
                else
                {
                    tAtlasCache = new TextureAtlas(tAtlasLoc, 21, 9, 16, 16, 0);
                    return tAtlasCache;
                }
            }
        }

        [NonSerialized]
        private TextureAtlas tAtlasCache;

        private string tAtlasLoc;

        public WorldData(int seed, int worldSize, int chunkSize, string TatlasLoc)
        {
            this.seed = seed;
            this.worldSize = worldSize;
            this.chunkSize = chunkSize;
            this.tAtlasLoc = TatlasLoc;

            Villages = new List<VillageData>();
            tAtlasCache = new TextureAtlas(tAtlasLoc, 21, 9, 16, 16, 0);
        }
    }

    [Serializable]
    public class VillageData
    {
        public Point Loc;
        public Point ConnectedVillageLoc;
        public List<Point> roadChunks;
    }

    [Serializable]
    public struct DungeonData
    {
        public Point DoorChunkLoc;
        public Point DoorLoc;
        public int sizeX;
        public int sizeY;

        public worldTile[,] backgroundTiles;
        public worldTile[,] foregroundTiles;
    }
}
