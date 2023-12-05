using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman.BombermanClasses
{
    internal class Enemy : Player
    {
        private float movementTimer;
        private float movementInterval;
        public Enemy(Texture2D enemyTexture, Vector2 startPosition, float speed, (Vector2, Vector2) boundaries, float movementInterval) : base(enemyTexture, startPosition, speed, boundaries)
        {
            this.movementInterval = movementInterval;
            movementTimer = movementInterval;
        }
        public override void Update(GameTime gameTime)
        {
            movementTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (movementTimer <= 0)
            {
                ChangeDirection(gameTime);

                movementTimer = movementInterval;
            }
        }

        private void ChangeDirection(GameTime gameTime)
        {
        
            Random random = new Random();
            int direction = random.Next(4); 

            switch (direction)
            {
                case 0: // Up
                    _position.Y -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case 1: // Down
                    _position.Y += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case 2: // Left
                    _position.X -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                   
                    break;
                case 3: // Right
                    _position.X += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                    
            }
        }
    }
}
