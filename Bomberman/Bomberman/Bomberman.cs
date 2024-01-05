using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BombermanBase;
using BombermanMONO.LogicExtensions;
using System.Timers;
using System.Collections.Generic;
using System;
using BombermanMONO.UIHelpers;

namespace BombermanMONO
{
    public class Bomberman : Game, IBombermanObserver
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private readonly Vector2 _windowSize;
        private Texture2D _backgroundTexture;

        private readonly int _windowBorderSize;
        public Vector2 CenterScreen
            => new Vector2(_graphics.GraphicsDevice.Viewport.Width / 2f, _graphics.GraphicsDevice.Viewport.Height / 2f);

        IBomberman _game;
        PlayerInfoBoard _playerInfoBoard;

        public SpriteFont DialogFont;
        private DialogBox _dialogBox;

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
        }

        protected override void Initialize()
        {
            //apply window size
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = (int)_windowSize.X;
            _graphics.PreferredBackBufferHeight = (int)_windowSize.Y;
            _graphics.ApplyChanges();

            _game = BombermanFactory.CreateGame("GigiGigiGigi");

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

            DialogFont = Content.Load<SpriteFont>("Fonts/DialogFont");

            LoadLevels();
            _playerInfoBoard = new PlayerInfoBoard(this,
                _game.Player.Username,
                new List<string>(),
                new Rectangle(_windowBorderSize / 5,
                    _windowBorderSize / 6,
                    200,
                    (int)(_windowBorderSize / 1.3)));
            Components.Add(_playerInfoBoard);

            _dialogBox = new DialogBox(this)
            {
                Text = "Hello World! Press Enter to proceed.\n" +
                       "I will be on the next pane! " +
                       "And wordwrap will occur, especially if there are some longer words!\n" +
                       "Monospace fonts work best but you might not want Courier New.\n" +
                       "In this code sample, after this dialog box finishes, you can press the O key to open a new one."
            };

            // Initialize the dialog box (this also calls the Show() method)
            _dialogBox.Initialize();
        }

        protected void LoadLevels()
        {
            List<string> levels = new()
            {
                //"C:\\Users\\Legion\\Desktop\\Portofolii\\an3\\IS\\Bomberman repo\\Bomberman\\Bomberman\\Content\\Level\\Level1.txt",
                //"C:\\Users\\Legion\\Desktop\\Portofolii\\an3\\IS\\Bomberman repo\\Bomberman\\Bomberman\\Content\\Level\\Level1.txt",
                //"C:\\Users\\Legion\\Desktop\\Portofolii\\an3\\IS\\Bomberman repo\\Bomberman\\Bomberman\\Content\\Level\\Level1.txt",
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

            _game.Update();
            _game.CheckGameOver();

            UpdatePlayerInfoBoard();

            if (_dialogBox.Active)
            {
                _dialogBox.Update();
                _game.Pause();
            }
            else
            {
                _game.Resume();
            }

            // Debug key to show opening a new dialog box on demand
            if (Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Keys.O))
            {
                if (!_dialogBox.Active)
                {
                    _dialogBox = new DialogBox(this) { Text = "New dialog box!" };
                    _dialogBox.Initialize();
                }
            }

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
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp);

            _spriteBatch.Draw(_backgroundTexture, Vector2.Zero, Color.White);

            //draw all emenets of the game - player, enemies, tiles
            _game.Draw(_spriteBatch);

            //draw the player status board
            _playerInfoBoard.Draw(gameTime);

            //draw the dialog box, if active
            _dialogBox.Draw(_spriteBatch);

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public void OnMoveMade(object sender, MoveEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OnGameOver(object sender, GameOverEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
