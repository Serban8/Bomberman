using BombermanBase;
using BombermanBase.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;


namespace BombermanMONO.LogicExtensions
{
    public static class PlayerExtensions
    {
        public static Texture2D playerTexture;
        public static Color playerTintColor = Color.White;
        public static SpriteEffects effects;
        public static SoundEffectInstance footstepSoundInstance;
        public static SoundEffectInstance explosionSoundInstance;
        public static void Draw(Vector2 screenCoords, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerTexture, screenCoords, null, playerTintColor, 0f, Vector2.Zero, 1f, effects, 0);
        }
        public static Vector2 GetScreenCoords(this IEntity player)
        {
            //size of the texture plus the border
            var tileTextureSize = new Vector2(TileExtensions.pathTexture.Width + TileMapExtensions.TileBorderSize, TileExtensions.pathTexture.Height + TileMapExtensions.TileBorderSize);
            var borderSize = TileMapExtensions.WindowBorderSize;
            return new Vector2((player.Position.X * tileTextureSize.X) + borderSize, (player.Position.Y * tileTextureSize.Y) + borderSize);
        }
    }
}
