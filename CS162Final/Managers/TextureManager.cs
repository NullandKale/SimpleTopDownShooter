using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Drawing.Imaging;

namespace nullEngine.Managers
{
    public class TextureManager
    {
        static int currentTexture = -1;

        public static Texture2D LoadTexture(string filePath, bool LinearFiltering)
        {
            Bitmap bitmap = new Bitmap(filePath);
            int id = GL.GenTexture();

            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.BindTexture(TextureTarget.Texture2D, id);

            GL.TexImage2D(TextureTarget.Texture2D, 0,
                PixelInternalFormat.Rgba, bitmap.Width, bitmap.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
                PixelType.UnsignedByte, bmpData.Scan0);

            bitmap.UnlockBits(bmpData);

            if(LinearFiltering)
            {
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                    (int)TextureMinFilter.Linear);

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                    (int)TextureMagFilter.Linear);
            }
            else
            {
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                    (int)TextureMinFilter.Nearest);

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                    (int)TextureMinFilter.Nearest);
            }

            return new Texture2D(id, bitmap.Width, bitmap.Height);
        }

        public static Texture2D TextureFrom1DTileMap(Tile[] tiles)
        {
            int tileSizeX = tiles[0].tAtlas.tilePixelWidth;
            int tileSizeY = tiles[0].tAtlas.tilePixelHeight;
            int xTileCount = tiles[0].tAtlas.tileWidth;
            int yTileCount = tiles[0].tAtlas.tileHeight;
            String filePath = tiles[0].tAtlas.path;

            Bitmap atlas = new Bitmap(filePath);

            Bitmap final = new Bitmap(tileSizeX * tiles.Length + 1, tileSizeY);
            int id = GL.GenTexture();

            for (int i = 0; i < tiles.Length; i++)
            {
                int tilePosY = tiles[i].TexID / xTileCount;
                int tilePosX = tiles[i].TexID % xTileCount;

                for(int k = 0; k < tileSizeY; k++)
                {
                    for(int j = 0; j < tileSizeX; j++)
                    {
                        int xCord = (i * tileSizeX) + j;
                        int yCord = k;

                        int tileCordX = ((tilePosX) * tileSizeX) + j;
                        int tileCordY = ((tilePosY) * tileSizeY) + k;

                        Color c = atlas.GetPixel(tileCordX, tileCordY);

                        final.SetPixel(xCord, yCord, c);
                    }
                }
            }

            BitmapData bmpData = final.LockBits(new Rectangle(0, 0, final.Width, final.Height),
                    ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.BindTexture(TextureTarget.Texture2D, id);

            GL.TexImage2D(TextureTarget.Texture2D, 0,
                PixelInternalFormat.Rgba, final.Width, final.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
                PixelType.UnsignedByte, bmpData.Scan0);

            final.UnlockBits(bmpData);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                (int)TextureMinFilter.Nearest);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                (int)TextureMagFilter.Nearest);

            return new Texture2D(id, final.Width, final.Height);

        }

        public static Texture2D TextureFrom2DTileMap(Tile[,] tiles)
        {
            int tileSizeX = tiles[0,0].tAtlas.tilePixelWidth;
            int tileSizeY = tiles[0,0].tAtlas.tilePixelHeight;
            int xTileCount = tiles[0,0].tAtlas.tileWidth;
            int yTileCount = tiles[0,0].tAtlas.tileHeight;
            String filePath = tiles[0,0].tAtlas.path;

            Bitmap atlas = new Bitmap(filePath);

            Bitmap final = new Bitmap(tileSizeX * tiles.Length + 1, tileSizeY * tiles.Length + 1);
            int id = GL.GenTexture();

            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    int tilePosY = tiles[x,y].TexID / xTileCount;
                    int tilePosX = tiles[x,y].TexID % xTileCount;

                    for (int k = 0; k < tileSizeY; k++)
                    {
                        for (int j = 0; j < tileSizeX; j++)
                        {
                            int xCord = (x * tileSizeX) + j;
                            int yCord = (y * tileSizeY) + k;

                            int tileCordX = ((tilePosX) * tileSizeX) + j;
                            int tileCordY = ((tilePosY) * tileSizeY) + k;

                            Color c = atlas.GetPixel(tileCordX, tileCordY);

                            final.SetPixel(xCord, yCord, c);
                        }
                    }
                }
            }

            BitmapData bmpData = final.LockBits(new Rectangle(0, 0, final.Width, final.Height),
                    ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.BindTexture(TextureTarget.Texture2D, id);

            GL.TexImage2D(TextureTarget.Texture2D, 0,
                PixelInternalFormat.Rgba, final.Width, final.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
                PixelType.UnsignedByte, bmpData.Scan0);

            final.UnlockBits(bmpData);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                (int)TextureMinFilter.Nearest);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                (int)TextureMagFilter.Nearest);

            return new Texture2D(id, final.Width, final.Height);

        }

        public static void GLSetTexture(int id)
        {
            if(currentTexture == -1 || currentTexture != id)
            {
                GL.BindTexture(TextureTarget.Texture2D, id);
                currentTexture = id;
            }
        }

        //ONLY USE IF SURE THE TEXTURE IS NOT IN USE SOMEWHERE ELSE
        public static void GLDestoryTexture(int id)
        {
            GL.DeleteTexture(id);
        }
    }
}
