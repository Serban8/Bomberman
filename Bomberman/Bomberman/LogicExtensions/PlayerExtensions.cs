using BombermanBase;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BombermanMONO.LogicExtensions
{
    public static class PlayerExtensions
    {
        public static Texture2D playerTexture;
        public static SpriteEffects effects;
        public static void Draw(Vector2 screenCoords, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerTexture, screenCoords, null, Color.White, 0f, Vector2.Zero, 1f, effects, 0);
        }
        public static Vector2 GetScreenCoords(this BombermanBase.Player player)
        {
            //size of the texture plus the border
            var tileTextureSize = new Vector2(TileExtensions.pathTexture.Width + TileMapExtensions.TileBorderSize, TileExtensions.pathTexture.Height + TileMapExtensions.TileBorderSize);
            var borderSize = TileMapExtensions.WindowBorderSize;
            return new Vector2((player.Position.X * tileTextureSize.X ) + borderSize, (player.Position.Y * tileTextureSize.Y) + borderSize);
        }

        public static void Update(this BombermanBase.Player player)
        {
            BombermanMONO.Keyboard.GetState();

            if (BombermanMONO.Keyboard.IsKeyPressed(Keys.Left))
            {
                int newX = player.Position.X - 1;
                int newY = player.Position.Y;
                player.Position = (newX, newY);

                PlayerExtensions.effects = SpriteEffects.FlipHorizontally;
            }
            else if (BombermanMONO.Keyboard.IsKeyPressed(Keys.Right))
            {
                int newX = player.Position.X + 1;
                int newY = player.Position.Y;
                player.Position = (newX, newY);

                PlayerExtensions.effects = SpriteEffects.None;
            }

            if (BombermanMONO.Keyboard.IsKeyPressed(Keys.Up))
            {
                int newX = player.Position.X;
                int newY = player.Position.Y - 1;
                player.Position = (newX, newY);
            }
            else if (BombermanMONO.Keyboard.IsKeyPressed(Keys.Down))
            {
                int newX = player.Position.X;
                int newY = player.Position.Y + 1;
                player.Position = (newX, newY);
            }
        }

    }
}
