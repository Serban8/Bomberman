using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BombermanBase;
using BombermanMONO.LogicExtensions;
using System.Timers;
using System.Collections.Generic;
using System;
using BombermanMONO;

namespace Bomberman
{
    public class Bomberman : Game, IObserver<BombermanBase.Bomberman>
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private readonly Vector2 _windowSize;
        private Texture2D _backgroundTexture;

        private readonly int _windowBorderSize;

        BombermanBase.Bomberman _game;
        PlayerInfoBoard _playerInfoBoard;

        public Bomberman()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            //set the window to be 80% of the screen size
            int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            _windowSize = new Vector2((float)(0.8 * screenWidth + 0.5), (float)(0.8 * screenHeight + 0.5));
            _windowBorderSize = (int)(0.05 * screenWidth + 0.5);

            TileMapExtensions.WindowBorderSize = _windowBorderSize;
            //

            _game = new BombermanBase.Bomberman();
        }

        protected override void Initialize()
        {
            //apply window size
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = (int)_windowSize.X;
            _graphics.PreferredBackBufferHeight = (int)_windowSize.Y;
            _graphics.ApplyChanges();

            _game.CreateGame();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _backgroundTexture = Content.Load<Texture2D>("Backgrounds/DarkGrayWithStars");

            TileExtensions.pathTexture = Content.Load<Texture2D>("Paths/path-small-rounded-v2");
            TileExtensions.unbreakableWallTexture = Content.Load<Texture2D>("Paths/wall");
            TileExtensions.breakableWallTexture = Content.Load<Texture2D>("Paths/breakablewall");
            TileExtensions.pathWithBombTexture = Content.Load<Texture2D>("paths/path-with-bomb");

            PlayerExtensions.playerTexture = Content.Load<Texture2D>("Sprites/player");

            EnemyExtensions.EnemyTexture = Content.Load<Texture2D>("Sprites/enemy2");

            LoadLevels();
            _playerInfoBoard = new PlayerInfoBoard(this, 
                _game.Player.Username, 
                new List<string>(), 
                new Rectangle(_windowBorderSize/5, 
                    _windowBorderSize/6, 
                    200, 
                    (int)(_windowBorderSize/1.3)));
            Components.Add(_playerInfoBoard);
        }

        protected void LoadLevels()
        {
            List<string> levels = new()
            { 
                "C:\\Users\\mihai\\OneDrive\\Materiale cursuri\\Anul3\\IS\\proiect\\Bomberman\\Bomberman\\Bomberman\\Content\\Level\\Level1.txt",
                "C:\\Users\\mihai\\OneDrive\\Materiale cursuri\\Anul3\\IS\\proiect\\Bomberman\\Bomberman\\Bomberman\\Content\\Level\\Level2.txt" ,
                "C:\\Users\\mihai\\OneDrive\\Materiale cursuri\\Anul3\\IS\\proiect\\Bomberman\\Bomberman\\Bomberman\\Content\\Level\\Level3.txt" 
            };

            //load the levels
            foreach (var levelPath in levels)
            {
                var level = TileMapExtensions.CreateMap(_windowBorderSize, _windowSize, levelPath);
                _game.AddLevel(level);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _game.Player.Update(_game.CrtLevel);
            
            UpdatePlayerInfoBoard();

            base.Update(gameTime);
        }

        private void UpdatePlayerInfoBoard()
        {
            string playerLivesInfo = $"Lives: {_game.Player.NoOfLives}";
            string playerBombosInfo = $"Bombs: {_game.Player.NoOfBombs}";

            List<string> playerInfo = new List<string>()
            {
                playerLivesInfo,
                playerBombosInfo
            };

            _playerInfoBoard.UpdateInfo(playerInfo);
            //_playerInfoBoard = new PlayerInfoBoard(this, _game.Player.Username, playerInfo, new Rectangle(_windowBorderSize/5, _windowBorderSize/6, 200, (int)(_windowBorderSize/1.3)));
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);

            _spriteBatch.Draw(_backgroundTexture, Vector2.Zero, Color.White);

            _game.CrtLevel.Draw(_spriteBatch);

            var screenCoords = _game.Player.GetScreenCoords();
            PlayerExtensions.Draw(screenCoords, _spriteBatch);

            foreach (var enemy in _game.Enemies)
            {
                var screenCoordsEnemy = enemy.GetScreenCoords();
                EnemyExtensions.Draw(screenCoordsEnemy, _spriteBatch);
            }

            _playerInfoBoard.Draw(gameTime);

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(BombermanBase.Bomberman value)
        {
            //_game = value;
        }
    }
}
