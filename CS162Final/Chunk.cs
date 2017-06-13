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

        private Texture2D backgroundTexture;
        private Bitmap backgroundBitmap;
        private bool textureGenerated;
        int size;

        public Chunk(int xSize, int xCord, int yCord)
        {
            key = new Point(xCord, yCord);
            backgroundTiles = new worldTile[xSize, xSize];
            foregroundTiles = new worldTile[xSize, xSize];
            size = xSize;
            textureGenerated = false;
        }

        public Texture2D getBackgroundTexture()
        {
            if(textureGenerated)
            {
                return backgroundTexture;
            }
            else
            {
                backgroundBitmap = Managers.TextureManager.BitmapFrom2DTileMap(backgroundTiles);
                backgroundTexture = Managers.TextureManager.TextureFromBitmap(backgroundBitmap);
                textureGenerated = true;
                return backgroundTexture;
            }
        }
    }
}
