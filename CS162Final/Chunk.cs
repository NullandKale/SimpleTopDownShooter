using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing.Imaging;
using System.Drawing;

namespace nullEngine
{
    [Serializable]
    public class Chunk
    {
        public worldTile[,] backgroundTiles;
        public worldTile[,] foregroundTiles;
        public Point key;

        private Bitmap backgroundBitmap;
        private bool textureGenerated;
        private bool textureOld;
        private int size;

        [NonSerialized]
        private Texture2D backgroundTexture;

        public Chunk(int xSize, int xCord, int yCord)
        {
            key = new Point(xCord, yCord);
            backgroundTiles = new worldTile[xSize, xSize];
            foregroundTiles = new worldTile[xSize, xSize];
            size = xSize;
            textureGenerated = false;
            textureOld = false;
        }

        public Texture2D getBackgroundTexture()
        {
            if(textureGenerated)
            {
                return backgroundTexture;
            }
            else
            {
                if(backgroundBitmap == null || textureOld)
                {
                    backgroundBitmap = Managers.TextureManager.BitmapFrom2DTileMap(backgroundTiles);
                    backgroundTexture = Managers.TextureManager.TextureFromBitmap(backgroundBitmap);
                    textureGenerated = true;
                    return backgroundTexture;
                }
                else
                {
                    backgroundTexture = Managers.TextureManager.TextureFromBitmap(backgroundBitmap);
                    textureGenerated = true;
                    return backgroundTexture;
                }
            }
        }

        public void AfterDiskLoad(TextureAtlas backgroundtAtlas)
        {
            for(int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    backgroundTiles[x, y].graphics.tAtlas = backgroundtAtlas;
                    foregroundTiles[x, y].graphics.tAtlas = backgroundtAtlas;
                }
            }
        }
    }
}
