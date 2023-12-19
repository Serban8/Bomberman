using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static void Update(this BombermanBase.Enemy enemy)
        {
            enemy.ChangeDirection();
        }
    }
}
