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
                Console.WriteLine("Generating World Texture");
                return TextureManager.TextureFrom2DTileMap(man.GenerateWorld());
            }
        }

        public TextureAtlas overworldTileAtlas;

        public CollisionManager cMan;

        public int worldMaxX
        {
            get
            {
                return worldSizeX * tileSize;
            }
        }

        public int worldMaxY
        {
            get
            {
                return worldSizeY * tileSize;
            }
        }

        OpenSimplexNoise noise;
        int worldSizeX;
        int worldSizeY;
        int tileSize;
        double scale;

        worldTile[,] world;
        bool generated;

        List<cCollider> colliders;

        public WorldManager(int seed, int worldSize, double scale, int tileSize, CollisionManager collisionManager)
        {
            if(man == null)
            {
                man = this;
            }
            else
            {
                throw new SingletonException(this);
            }

            cMan = collisionManager;

            overworldTileAtlas = new TextureAtlas("Content/overworld.png", 21, 9, 16, 16, 0);
            noise = new OpenSimplexNoise(seed);

            worldSizeX = worldSize;
            worldSizeY = worldSize;
            this.tileSize = tileSize;
            this.scale = scale;

            world = new worldTile[worldSizeX, worldSizeY];
            generated = false;

            GenerateWorld();
        }

        public worldTile[,] GenerateWorld()
        {
            if(generated)
            {
                return world;
            }

            double[,] height = new double[worldSizeX, worldSizeY];

            double maxHeight = 0;

            Console.WriteLine("Generating Height Map");

            for(int x = 0; x < worldSizeX; x++)
            {
                for(int y = 0; y < worldSizeY; y++)
                {
                    double xLoc = ((double)x / (double)worldSizeX) * scale;
                    double yLoc = ((double)y / (double)worldSizeY) * scale;
                    height[x,y] = noise.Evaluate(xLoc, yLoc);

                    if(maxHeight < height[x,y])
                    {
                        maxHeight = height[x, y];
                    }

                }
            }

            Console.WriteLine("Generating Tilemap");

            for (int x = 0; x < worldSizeX; x++)
            {
                for (int y = 0; y < worldSizeY; y++)
                {
                    world[x, y].graphics.tAtlas = overworldTileAtlas;

                    double region0 = 0.25 * maxHeight;
                    double region1 = 0.50 * maxHeight;
                    double region2 = 0.75 * maxHeight;

                    if (height[x,y] < region0)
                    {
                        int tile = Game.rng.Next(0, 3);

                        if (tile == 0)
                        {
                            world[x, y].graphics.TexID = (int)WorldTexID.grass0;
                        }
                        else if (tile == 1)
                        {
                            world[x, y].graphics.TexID = (int)WorldTexID.grass1;
                        }
                        else
                        {
                            world[x, y].graphics.TexID = (int)WorldTexID.grass2;
                        }

                        world[x, y].isCollideable = false;
                    }
                    else if (height[x, y] >= region0 && height[x, y] < region1)
                    {
                        int tile = Game.rng.Next(0, 5);

                        if (tile == 0)
                        {
                            world[x, y].graphics.TexID = (int)WorldTexID.sand0;
                        }
                        else if (tile == 1)
                        {
                            world[x, y].graphics.TexID = (int)WorldTexID.sand1;
                        }
                        else if (tile == 2)
                        {
                            world[x, y].graphics.TexID = (int)WorldTexID.sand2;
                        }
                        else if (tile == 3)
                        {
                            world[x, y].graphics.TexID = (int)WorldTexID.sand3;
                        }
                        else
                        {
                            world[x, y].graphics.TexID = (int)WorldTexID.sand4;
                        }

                        world[x, y].isCollideable = false;
                    }
                    else if (height[x, y] >= region1 && height[x, y] < region2)
                    {
                        int tile = Game.rng.Next(0, 2);

                        if (tile == 0)
                        {
                            world[x, y].graphics.TexID = (int)WorldTexID.water0;
                        }
                        else
                        {
                            world[x, y].graphics.TexID = (int)WorldTexID.water1;
                        }

                        world[x, y].isCollideable = true;

                    }
                    else
                    {
                        world[x, y].graphics.TexID = (int)WorldTexID.water2;
                        world[x, y].isCollideable = true;
                    }
                }
            }
            generated = true;
            GenerateColliders();
            return world;
        }

        public worldTile[,] GenTestPattern()
        {
            for (int x = 0; x < worldSizeX; x++)
            {
                for (int y = 0; y < worldSizeY; y++)
                {
                    world[x, y].graphics.tAtlas = overworldTileAtlas;
                    world[x, y].graphics.TexID = (y * worldSizeX + x) % 4; 
                }
            }

            return world;
        }

        public void GenerateColliders()
        {
            Console.WriteLine("Generating World Colliders");
            if(colliders == null)
            {
                colliders = new List<cCollider>();
            }
            else
            {
                for(int i = 0; i < colliders.Count; i++)
                {
                    CollisionManager.removeCollider(colliders[i]);
                }
                colliders.Clear();
            }


            for (int x = 0; x < worldSizeX; x++)
            {
                for (int y = 0; y < worldSizeY; y++)
                {
                    if(world[x,y].isCollideable)
                    {
                        colliders.Add(new cCollider(getTileRect(x, y)));
                    }
                }
            }
        }

        public static Tile worldTileToTile(worldTile tile)
        {
            return tile.graphics;
        }

        public static Point worldToTile(Point worldPos)
        {
            if(worldPos.X >= man.worldSizeX * man.tileSize || worldPos.Y >= man.worldSizeY * man.tileSize)
            {
                return new Point(0, 0);
            }
            return new Point(worldPos.X / 16 * Entity___Component.transform.masterScale, worldPos.Y / 16 * Entity___Component.transform.masterScale);
        }

        public static Point worldToTile(int X, int Y)
        {
            if (X >= man.worldSizeX * man.tileSize || Y >= man.worldSizeY * man.tileSize)
            {
                return new Point(0, 0);
            }
            return new Point(X / man.tileSize, Y / man.tileSize);
        }

        public static Rectangle getTileRect(Point tilePos)
        {
            tilePos.X = tilePos.X * man.tileSize;
            tilePos.Y = tilePos.Y * man.tileSize;
            return new Rectangle(tilePos.X, tilePos.Y, man.tileSize, man.tileSize);
        }

        public static Rectangle getTileRect(int X, int Y)
        {
            X = X * man.tileSize;
            Y = Y * man.tileSize;
            return new Rectangle(X, Y, man.tileSize, man.tileSize);
        }
    }
}
