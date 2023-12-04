using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman.NewFolder
{
    internal class Bomb
    {
        private Texture2D texture;

        public Vector2 Position { get; set; }
        public static int ExplosionTimer = 3;
        public static int ExplosionRadius = 5;
        

        public Bomb(Texture2D bombTexture, Vector2 bombPosition, float explosionTimer, float explosionRadius)
        {
            texture = bombTexture;
            Position = bombPosition;
          
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }
    }
}
