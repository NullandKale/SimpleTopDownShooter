using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nullEngine
{
    public class TextureAtlas
    {
        public int pixelWidth;
        public int pixelHeight;

        public int tilePixelWidth;
        public int tilePixelHeight;

        public int tileWidth;
        public int tileHeight;

        public string path;

        private Texture2D baseTexture;
        private int _padding;

        public TextureAtlas(string TexturePath, int xTileCount, int yTileCount, int pixelsPerTileX, int pixelsPerTileY, int padding)
        {
            baseTexture = Managers.TextureManager.LoadTexture(TexturePath, false);

            path = TexturePath;

            pixelWidth = baseTexture.width + 1;
            pixelHeight = baseTexture.height + 1;

            tilePixelHeight = pixelsPerTileY;
            tilePixelWidth = pixelsPerTileX;

            tileWidth = xTileCount;
            tileHeight = yTileCount;
            _padding = padding;
        }

        public Texture2D getTile(int index)
        {
            int xOffset = (index % tileWidth);
            int yOffset = (index / tileWidth);

            float xPixels, yPixels, xPixEnd, yPixEnd;
            if(xOffset != 0)
            {
                xPixels = ((float)xOffset * (tilePixelWidth + _padding)) / pixelWidth;
                xPixEnd = (((float)xOffset * (tilePixelWidth + _padding)) + tilePixelWidth) / pixelWidth;
            }
            else
            {
                xPixels = 0f;
                xPixEnd = (float)tilePixelWidth / pixelWidth;
            }

            if (yOffset != 0)
            {
                yPixels = ((float)yOffset * (tilePixelHeight + _padding)) / pixelHeight;
                yPixEnd = (((float)yOffset * (tilePixelHeight + _padding)) + tilePixelHeight) / pixelHeight;
            }
            else
            {
                yPixels = 0f;
                yPixEnd = (float)tilePixelHeight / pixelHeight;
            }

            return new Texture2D(baseTexture.id, pixelWidth, pixelHeight, xPixels, yPixels, xPixEnd, yPixEnd);
        }
    }
}
