using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using BombermanBase;

namespace BombermanMONO.LogicExtensions
{
    internal static class TileExtensions
    {
        public static Texture2D pathTexture;
        public static Texture2D unbreakableWallTexture;
        public static Texture2D breakableWallTexture;
        public static Texture2D pathWithBombTexture;

        public static void Draw(this ITile tile, SpriteBatch spriteBatch)
        {
            //List<SpriteEffects> rotation = new List<SpriteEffects>() { SpriteEffects.None, SpriteEffects.FlipHorizontally, SpriteEffects.FlipVertically, SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically };
            (int x, int y) screenPosition = tile.CalculateTileScreenPosition();
            if (tile.Type == TileType.Path)
            {
                //select a random effect from the list
                //Random rnd = new Random();
                //int effectIndex = rnd.Next(0, rotation.Count);
                //SpriteEffects effect = rotation[effectIndex];

                //spriteBatch.Draw(pathTexture, new Vector2(screenPosition.x, screenPosition.y), null, Color.White, 0f, Vector2.Zero, 1f, effect, 0);
                spriteBatch.Draw(pathTexture, new Vector2(screenPosition.x, screenPosition.y), Color.White);
            }
            else if (tile.Type == TileType.UnbreakableWall)
            {
                spriteBatch.Draw(unbreakableWallTexture, new Vector2(screenPosition.x, screenPosition.y), Color.White);
            }
            else if (tile.Type == TileType.BreakableWall)
            {
                spriteBatch.Draw(breakableWallTexture, new Vector2(screenPosition.x, screenPosition.y), Color.White);
            }
            else if (tile.Type == TileType.PathWithBomb)
            {
                spriteBatch.Draw(pathWithBombTexture, new Vector2(screenPosition.x, screenPosition.y), Color.White);
            }
        }

        private static (int, int) CalculateTileScreenPosition(this ITile tile)
        {
            int posX = tile.Position.X * (pathTexture.Width + TileMapExtensions.TileBorderSize) + TileMapExtensions.WindowBorderSize;
            int posY = tile.Position.Y * (pathTexture.Height + TileMapExtensions.TileBorderSize) + TileMapExtensions.WindowBorderSize;
            return (posX, posY);
        }
    }
}
