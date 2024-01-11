using BombermanBase.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace BombermanMONO.LogicExtensions
{
    public static class EnemyExtensions
    {
        public static Texture2D EnemyTexture;
        public static SpriteEffects effects;

        public static void Draw(Vector2 screenCoords, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(EnemyTexture, screenCoords, null, Color.White, 0f, Vector2.Zero, 1f, effects, 0);
        }

        //used to draw enemies after death
        public static void Draw(this IEntity entity, SpriteBatch spriteBatch, Color tintColor)
        {
            var screenCoords = entity.GetScreenCoords();
            spriteBatch.Draw(EnemyTexture, screenCoords, null, tintColor, 0f, Vector2.Zero, 1f, effects, 0);
        }
    }
}
