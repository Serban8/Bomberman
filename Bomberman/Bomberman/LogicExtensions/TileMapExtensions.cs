using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombermanMONO.LogicExtensions
{
    internal static class TileMapExtensions
    {

        public static int WindowBorderSize;
        public static int TileBorderSize = 5;

        public static void Draw(this BombermanBase.TileMap tileMap, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < tileMap.MapSize.Width; i++)
            {
                for (int j = 0; j < tileMap.MapSize.Height; j++)
                {
                    tileMap.Tiles[i, j].Draw(spriteBatch);
                }
            }
        }

        public static BombermanBase.TileMap CreateMap(int windowBorderSize, Vector2 windowSize, string mapFilePath)
        {
            (int tilesWidth, int tilesHeight) size = CalculateMapSize(windowBorderSize, windowSize);
            return new BombermanBase.TileMap(size, mapFilePath);
        }

        private static (int, int) CalculateMapSize(int windowBorderSize, Vector2 windowSize)
        {
            int posX = windowBorderSize;
            int posY = windowBorderSize;

            //calculate the number of tiles that can fit in the window taking the border into account
            int tilesWidth = (int)((windowSize.X - 2 * windowBorderSize) / (TileExtensions.pathTexture.Width + TileBorderSize));
            int tilesHeight = (int)((windowSize.Y - 2 * windowBorderSize) / (TileExtensions.pathTexture.Height + TileBorderSize));
            return (tilesWidth, tilesHeight);
        }
    }
}
