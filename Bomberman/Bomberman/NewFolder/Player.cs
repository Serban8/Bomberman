using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman.NewFolder
{
    internal class Player
    {
        private SpriteEffects flip = SpriteEffects.None;
        private Texture2D texture;

        private string username;
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        private int noOfBombs;
        public int NoOfBombs
        {
            get { return noOfBombs; }
            set { noOfBombs = value; }
        }

        private int noOfLives;
        public int NoOfLives
        {
            get { return noOfLives; }
            set { noOfLives = value; }
        }

        private float speed;
        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        private Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public Player(Texture2D playerTexture, Vector2 startPosition, float speed)
        {
            texture = playerTexture;
            Position = startPosition;
            Speed = speed;
        }
        public void Update(GameTime gameTime)
        {
            HandleInput(gameTime);
            Move();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, null, Color.White, 0f, Vector2.Zero, 1f, flip, 0);
        }
        private void HandleInput(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                position.X -=Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                flip = SpriteEffects.FlipHorizontally;
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                position.X += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                flip = SpriteEffects.None;
            }

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                position.Y -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                position.Y += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        private void Move()
        {
            position.X = MathHelper.Clamp(Position.X, 0, GraphicsDeviceManager.DefaultBackBufferWidth - texture.Width);
            position.Y = MathHelper.Clamp(Position.Y, 0, GraphicsDeviceManager.DefaultBackBufferHeight - texture.Height);
        }
    }
}
