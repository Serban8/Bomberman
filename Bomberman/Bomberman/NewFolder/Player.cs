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
        private Texture2D _texture;

        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        private int _noOfBombs;
        public int NoOfBombs
        {
            get { return _noOfBombs; }
            set { _noOfBombs = value; }
        }

        private int _noOfLives;
        public int NoOfLives
        {
            get { return _noOfLives; }
            set { _noOfLives = value; }
        }

        private float _speed;
        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        private Vector2 _position;
        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        private (Vector2, Vector2) _boundaries;
        public (Vector2 TopLeft, Vector2 BottomRight) Boundaries
        {
            get { return _boundaries; }
            set { _boundaries = value; }
        }
        public Player(Texture2D playerTexture, Vector2 startPosition, float speed, (Vector2, Vector2) boundaries)
        {
            _texture = playerTexture;
            Position = startPosition;
            Speed = speed;
            Boundaries = boundaries;
        }
        public void Update(GameTime gameTime)
        {
            HandleInput(gameTime);
            Move();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null, Color.White, 0f, Vector2.Zero, 1f, flip, 0);
        }
        private void HandleInput(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                _position.X -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                flip = SpriteEffects.FlipHorizontally;
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                _position.X += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                flip = SpriteEffects.None;
            }

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                _position.Y -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                _position.Y += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        private void Move()
        {
            _position.X = MathHelper.Clamp(Position.X, Boundaries.TopLeft.X, Boundaries.BottomRight.X - _texture.Width);
            _position.Y = MathHelper.Clamp(Position.Y, Boundaries.TopLeft.Y, Boundaries.BottomRight.Y - _texture.Height);
        }
    }
}
