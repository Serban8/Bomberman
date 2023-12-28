using BombermanBase;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;


namespace BombermanMONO.LogicExtensions
{
    public static class PlayerExtensions
    {
        public static Texture2D playerTexture;
        public static SpriteEffects effects;
        private static Timer bombTimer;
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

        public static void Update(this BombermanBase.Player player, TileMap tileMap)
        {
            BombermanMONO.Keyboard.GetState();

            bool updated = false;
            if (BombermanMONO.Keyboard.IsKeyPressed(Keys.Left))
            {
                player.Move(tileMap, -1, 0);
                updated = true;

                PlayerExtensions.effects = SpriteEffects.FlipHorizontally;
            }
            else if (BombermanMONO.Keyboard.IsKeyPressed(Keys.Right))
            {
                player.Move(tileMap, 1, 0);
                updated = true;

                PlayerExtensions.effects = SpriteEffects.None;
            }

            if (BombermanMONO.Keyboard.IsKeyPressed(Keys.Up))
            {
                player.Move(tileMap, 0, -1);
                updated = true;

            }
            else if (BombermanMONO.Keyboard.IsKeyPressed(Keys.Down))
            {
                player.Move(tileMap, 0, 1);
                updated = true;
            }
            else if (BombermanMONO.Keyboard.IsKeyPressed(Keys.Space))
            {
                if(player.NoOfBombs>0)
                {
                    Tile currentTile = tileMap.GetTile(player.Position);
                    currentTile.AddBomb();
                    player.RemoveBomb();
                    bombTimer = new Timer(5000);
                    bombTimer.Elapsed += (sender, e) => currentTile.Explode(player);
                    bombTimer.AutoReset = false;
                    bombTimer.Start();
                }
            }

            if (!updated)
            {
                player.Move(tileMap);
            }


        }

    }
}
