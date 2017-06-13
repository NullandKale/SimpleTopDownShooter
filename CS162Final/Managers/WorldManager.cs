using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using nullEngine.Entity___Component;

namespace nullEngine.Managers
{
    public class WorldManager
    {
        public static WorldManager man;
        public static Texture2D worldTex
        {
            get
            {
                return man.currentChunk.getBackgroundTexture();
            }
        }

        public Point currentChunkPos;
        public Chunk currentChunk;

        public int worldMaxX
        {
            get
            {
                return chunkSizeX * tileSize;
            }
        }

        public int worldMaxY
        {
            get
            {
                return chunkSizeY * tileSize;
            }
        }

        private int worldSizeX;
        private int worldSizeY;
        private int chunkSizeX;
        private int chunkSizeY;
        private int tileSize;
        private double scale;

        private WorldGen.WorldGenerator wGen;
        private Dictionary<Point, Chunk> worldCache;

        public WorldManager(int seed, int worldSize, int chunkSize, double scale, int tileSize, CollisionManager collisionManager, Point curretChunk)
        {
            if(man == null)
            {
                man = this;
            }
            else
            {
                throw new SingletonException(this);
            }

            worldSizeX = worldSize;
            worldSizeY = worldSize;
            chunkSizeX = chunkSize;
            chunkSizeY = chunkSize;
            currentChunkPos = curretChunk;
            currentChunk = new Chunk(chunkSize, currentChunkPos.X, currentChunkPos.Y);

            this.tileSize = tileSize;
            this.scale = scale;

            wGen = new WorldGen.WorldGenerator(seed, worldSize, chunkSize, scale, tileSize, collisionManager);
            worldCache = new Dictionary<Point, Chunk>();
            LoadChunk();
        }

        public void LoadChunk()
        {
            if (worldCache.ContainsKey(currentChunkPos))
            {
                currentChunk = worldCache[currentChunkPos];
            }
            else
            {
                currentChunk = wGen.GenerateWorld(currentChunkPos);
                worldCache.Add(currentChunk.key, currentChunk);
            }

            wGen.GenerateColliders(currentChunk.backgroundTiles);
        }

        public static Tile worldTileToTile(worldTile tile)
        {
            return tile.graphics;
        }

        public static Point worldToTile(Point worldPos)
        {
            if(worldPos.X >= man.chunkSizeX * man.tileSize || worldPos.Y >= man.chunkSizeY * man.tileSize)
            {
                return new Point(0, 0);
            }
            return new Point(worldPos.X / 16 * Entity___Component.transform.masterScale, worldPos.Y / 16 * Entity___Component.transform.masterScale);
        }

        public static Point worldToTile(int X, int Y)
        {
            if (X >= man.chunkSizeX * man.tileSize || Y >= man.chunkSizeY * man.tileSize)
            {
                return new Point(0, 0);
            }
            return new Point(X / man.tileSize, Y / man.tileSize);
        }

        public void ChangeCurrentChunk(int X, int Y)
        {
            if(X == 0 && Y == 0)
            {
                return;
            }

            if(currentChunkPos.X + X < 0)
            {
                currentChunkPos.X = worldSizeX - 1;
            }
            else if(currentChunkPos.X + X >= worldSizeX)
            {
                currentChunkPos.X = 0;
            }
            else
            {
                currentChunkPos.X += X;
            }

            if (currentChunkPos.Y + Y < 0)
            {
                currentChunkPos.Y = worldSizeY - 1;
            }
            else if (currentChunkPos.Y + Y >= worldSizeY)
            {
                currentChunkPos.Y = 0;
            }
            else
            {
                currentChunkPos.Y += Y;
            }
            LoadChunk();
        }
    }
}
