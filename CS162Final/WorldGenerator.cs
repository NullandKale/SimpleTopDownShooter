using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using nullEngine.Managers;
using nullEngine.Entity___Component;

namespace nullEngine.WorldGen
{
    class WorldGenerator
    {
        public WorldData wData;
        private int tileSize;
        private double scale;
        private OpenSimplexNoise noise;

        private CollisionManager cMan;
        private List<cCollider> colliders;

        public WorldGenerator(int seed, int worldSize, int chunkSize, double scale, int tileSize, CollisionManager collisionManager)
        {
            cMan = collisionManager;
            noise = new OpenSimplexNoise(seed);
            this.scale = scale;
            this.tileSize = tileSize;

            wData = new WorldData(seed, worldSize, chunkSize, "Content/overworld.png");
        }

        public Chunk GenerateWorld(Point tempChunkPos)
        {
            Chunk tempChunk = new Chunk(wData.chunkSize, tempChunkPos.X, tempChunkPos.Y);

            double[,] height = new double[wData.chunkSize, wData.chunkSize];

            double maxHeight = 0;

            for (int x = 0; x < wData.chunkSize; x++)
            {
                for (int y = 0; y < wData.chunkSize; y++)
                {
                    double xLoc = (((double)x / (double)wData.chunkSize) + (tempChunkPos.X * wData.chunkSize)) * scale;
                    double yLoc = (((double)y / (double)wData.chunkSize) + (tempChunkPos.Y * wData.chunkSize)) * scale;
                    height[x, y] = noise.Evaluate(xLoc, yLoc);

                    if (maxHeight < height[x, y])
                    {
                        maxHeight = height[x, y];
                    }
                }
            }

            for (int x = 0; x < wData.chunkSize; x++)
            {
                for (int y = 0; y < wData.chunkSize; y++)
                {
                    tempChunk.backgroundTiles[x, y].graphics.tAtlas = wData.tAtlas;

                    double region0 = 0.25 * maxHeight;
                    double region1 = 0.50 * maxHeight;
                    double region2 = 0.75 * maxHeight;

                    if (height[x, y] < region0)
                    {
                        int tile = Game.rng.Next(0, 3);

                        if (tile == 0)
                        {
                            tempChunk.backgroundTiles[x, y].graphics.TexID = (int)WorldTexID.grass0;
                        }
                        else if (tile == 1)
                        {
                            tempChunk.backgroundTiles[x, y].graphics.TexID = (int)WorldTexID.grass1;
                        }
                        else
                        {
                            tempChunk.backgroundTiles[x, y].graphics.TexID = (int)WorldTexID.grass2;
                        }

                        tempChunk.backgroundTiles[x, y].isCollideable = false;
                    }
                    else if (height[x, y] >= region0 && height[x, y] < region1)
                    {
                        int tile = Game.rng.Next(0, 5);

                        if (tile == 0)
                        {
                            tempChunk.backgroundTiles[x, y].graphics.TexID = (int)WorldTexID.sand0;
                        }
                        else if (tile == 1)
                        {
                            tempChunk.backgroundTiles[x, y].graphics.TexID = (int)WorldTexID.sand1;
                        }
                        else if (tile == 2)
                        {
                            tempChunk.backgroundTiles[x, y].graphics.TexID = (int)WorldTexID.sand2;
                        }
                        else if (tile == 3)
                        {
                            tempChunk.backgroundTiles[x, y].graphics.TexID = (int)WorldTexID.sand3;
                        }
                        else
                        {
                            tempChunk.backgroundTiles[x, y].graphics.TexID = (int)WorldTexID.sand4;
                        }

                        tempChunk.backgroundTiles[x, y].isCollideable = false;
                    }
                    else if (height[x, y] >= region1 && height[x, y] < region2)
                    {
                        int tile = Game.rng.Next(0, 2);

                        if (tile == 0)
                        {
                            tempChunk.backgroundTiles[x, y].graphics.TexID = (int)WorldTexID.water0;
                        }
                        else
                        {
                            tempChunk.backgroundTiles[x, y].graphics.TexID = (int)WorldTexID.water1;
                        }

                        tempChunk.backgroundTiles[x, y].isCollideable = true;

                    }
                    else
                    {
                        tempChunk.backgroundTiles[x, y].graphics.TexID = (int)WorldTexID.water2;
                        tempChunk.backgroundTiles[x, y].isCollideable = true;
                    }
                }
            }
            return tempChunk;
        }

        public void GenerateColliders(worldTile[,] tempChunk)
        {
            if (colliders == null)
            {
                colliders = new List<cCollider>();
            }
            else
            {
                for (int i = 0; i < colliders.Count; i++)
                {
                    CollisionManager.removeCollider(colliders[i]);
                }
                colliders.Clear();
            }


            for (int x = 0; x < wData.chunkSize; x++)
            {
                for (int y = 0; y < wData.chunkSize; y++)
                {
                    if (tempChunk[x, y].isCollideable)
                    {
                        colliders.Add(new cCollider(getTileRect(x, y)));
                    }
                }
            }
        }

        public Rectangle getTileRect(Point tilePos)
        {
            tilePos.X = tilePos.X * tileSize;
            tilePos.Y = tilePos.Y * tileSize;
            return new Rectangle(tilePos.X, tilePos.Y, tileSize, tileSize);
        }

        public Rectangle getTileRect(int X, int Y)
        {
            X = X * tileSize;
            Y = Y * tileSize;
            return new Rectangle(X, Y, tileSize, tileSize);
        }

        ///////////////////////////////////////////////////////////////////////////
        //                  World Generation Functions Galore                    //
        //                         HERE BE DRAGONS                               //
        ///////////////////////////////////////////////////////////////////////////

        private void GenerateWorldData()
        {
            
        }

        private void GenerateVillageChunkLocations()
        {
            int numberVillages = 5;
            int minChunkDist = 2;
        }
    }
}
