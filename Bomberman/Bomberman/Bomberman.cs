using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BombermanBase;
using BombermanMONO.LogicExtensions;
using System.Timers;
using System.Collections.Generic;
using System;

namespace Bomberman
{
    public class Bomberman : Game, IObserver<BombermanBase.Bomberman>
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private readonly Vector2 _windowSize;
        private Texture2D _backgroundTexture;

        //private BombermanBase.Entity _player;
        //private BombermanBase.TileMap _tileMap;
        //private BombermanBase.Entity _enemy;

        private readonly int _windowBorderSize;
        //Timer aTimer = new System.Timers.Timer(1000);

        BombermanBase.Bomberman _game;

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _game.Player.Update(_game.CrtLevel);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(25, 25, 25));
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
