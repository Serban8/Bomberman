using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombermanMONO.UIHelpers
{
    internal class PlayerInfoBoard : DrawableGameComponent
    {
        private SpriteBatch _spriteBatch;
        private SpriteFont _infoFont;
        private SpriteFont _usernameFont;

        private Texture2D _rectangle;
        private Rectangle _rectangleBounds;
        private int _rectBorderSize = 5;

        private List<string> infoLines;
        private string _playerName;

        public PlayerInfoBoard(Game game, string playerName, List<string> infoLines, Rectangle rect) : base(game)
        {
            _playerName = playerName;
            this.infoLines = infoLines;
            _rectangleBounds = rect;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _rectangle = new Texture2D(GraphicsDevice, 1, 1);
            _rectangle.SetData(new[] { new Color(25, 25, 25) });

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _infoFont = Game.Content.Load<SpriteFont>("Fonts/StatusBoardInfoFont");
            _usernameFont = Game.Content.Load<SpriteFont>("Fonts/StatusBoardUsernameFont");
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            // Draw the rectangle
            _spriteBatch.Draw(_rectangle, _rectangleBounds, new Color(25, 25, 25));

            // Draw the username
            Vector2 textPosition = new Vector2(_rectangleBounds.X + _rectangleBounds.Width / 2 - _usernameFont.MeasureString(_playerName).X / 2,
                _rectangleBounds.Y + _rectBorderSize);
            _spriteBatch.DrawString(_usernameFont, _playerName, textPosition, Color.White);

            //draw rest of the info
            string nextLineInfo = "";
            foreach (string line in infoLines)
            {
                nextLineInfo += line + " - ";
            }
            nextLineInfo = nextLineInfo.Substring(0, nextLineInfo.Length - 3);

            textPosition = new Vector2(_rectangleBounds.X + _rectangleBounds.Width / 2 - _infoFont.MeasureString(nextLineInfo).X / 2,
                _rectangleBounds.Y + _rectangleBounds.Height - (_infoFont.MeasureString(nextLineInfo).Y + _rectBorderSize));
            _spriteBatch.DrawString(_infoFont, nextLineInfo, textPosition, Color.White);

            _spriteBatch.End();
            base.Draw(gameTime);

        }

        public void UpdateInfo(List<string> infoLines)
        {
            this.infoLines = infoLines;
        }
    }
}
