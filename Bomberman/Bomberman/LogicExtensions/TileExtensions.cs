﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombermanMONO.LogicExtensions
{
    internal static class TileExtensions
    {
        public static Texture2D pathTexture;

        public static void Draw(this BombermanBase.Tile tile, SpriteBatch spriteBatch)
        {
            (int x, int y) screenPosition = tile.CalculateTileScreenPosition();
            if (tile.Type == BombermanBase.TileType.Path)
            {
                spriteBatch.Draw(pathTexture, new Vector2(screenPosition.x, screenPosition.y), Color.White);
            }
        }

        private static (int, int) CalculateTileScreenPosition(this BombermanBase.Tile tile)
        {
            int posX = tile.Position.X * (pathTexture.Width + TileMapExtensions.TileBorderSize) + TileMapExtensions.WindowBorderSize;
            int posY = tile.Position.Y * (pathTexture.Height + TileMapExtensions.TileBorderSize) + TileMapExtensions.WindowBorderSize;
            return (posX, posY);
        }
    }
}
