using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BombermanBase;
using BombermanMONO.LogicExtensions;
using System.Timers;
using System.Collections.Generic;
using BombermanMONO.UIHelpers;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Threading;

using Timer = System.Timers.Timer;
using BombermanBase.Entities;

namespace BombermanMONO
{
    public class Bomberman : Game, IBombermanObserver
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private readonly Vector2 _windowSize;
        private Texture2D _backgroundTexture;
        private Song _backgroundSong;

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
            _game.AddObserver(this);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _backgroundTexture = Content.Load<Texture2D>("Backgrounds/DarkGrayWithStars");
            _backgroundSong = Content.Load<Song>("Sounds/background");
            MediaPlayer.Play(_backgroundSong);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.2f;
            //MediaPlayer.MediaStateChanged += (sender, e) =>
            //{
            //    MediaPlayer.Volume = 0.1f;
            //};

            TileExtensions.pathTexture = Content.Load<Texture2D>("Paths/path-small-rounded-v2");
            TileExtensions.unbreakableWallTexture = Content.Load<Texture2D>("Paths/wall");
            TileExtensions.breakableWallTexture = Content.Load<Texture2D>("Paths/breakablewall");
            TileExtensions.pathWithBombTexture = Content.Load<Texture2D>("paths/path-with-bomb");

            PlayerExtensions.playerTexture = Content.Load<Texture2D>("Sprites/player");
            PlayerExtensions.footstepSoundInstance = Content.Load<SoundEffect>("Sounds/footstep").CreateInstance();
            PlayerExtensions.footstepSoundInstance.Volume = 0.3f;
            PlayerExtensions.footstepSoundInstance.IsLooped = true;

            PlayerExtensions.explosionSoundInstance = Content.Load<SoundEffect>("Sounds/explosion").CreateInstance();
            PlayerExtensions.explosionSoundInstance.Volume = 0.3f;

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
                Text = "Welcome to BomberMan! Your goal is to kill all the enemies using bombs." +
                       "Press [enter] to continue or press [x] to skip this dialog\n" +
                       "Move using the arrow keys and place bombs using the [space] key!\n" +
                       "Use your bombs wisely, as you only get a limited number!\n" +
                       "Be careful when moving! If you touch an enemy, you will lose a life!\n" +
                       "You can see how many lives and bombs you have left in the top left corner.\n" +
                       "Press [enter] to start the game. Good luck!\n"
            };

            // Initialize the dialog box (this also calls the Show() method)
            _dialogBox.Initialize();
        }

        protected void LoadLevels()
        {
            List<string> levels = new()
            {
                Content.RootDirectory + "\\Level\\Level1.txt",
                Content.RootDirectory + "\\Level\\Level2.txt" ,
                Content.RootDirectory + "\\Level\\Level3.txt"
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
            {
                if (!_dialogBox.Active)
                {
                    _dialogBox = new DialogBox(this) { Text = "The game is paused! Press enter to resume or simply close the window to quit." };
                    _dialogBox.Initialize();
                }
            }

            _game.Update();
            _game.CheckLevelOver();

            UpdatePlayerInfoBoard();
            _dialogBox.Update();

            # region Debug key to show opening a new dialog box on demand
            if (Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Keys.O))
            {
                if (!_dialogBox.Active)
                {
                    _dialogBox = new DialogBox(this) { Text = "New dialog box!" };
                    _dialogBox.Initialize();
                }
            }
            #endregion

            #region Debug key to stop enemies
            if (Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Keys.P))
            {
                _game.PauseEnemies();
            }
            #endregion

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

        public void Pause()
        {
            _game.PauseGame();
            MediaPlayer.Volume = 0.2f;
        }

        public void Resume()
        {
            _game.ResumeGame();
            MediaPlayer.Volume = 0.1f;
        }

        public void OnPlayerMoved(object sender, MoveEventArgs e)
        {
            if (e.Entity == _game.Player)
            {
                PlayerExtensions.footstepSoundInstance.Play();
                Timer timer = new Timer(500);
                timer.Elapsed += (sender, e) =>
                {
                    PlayerExtensions.footstepSoundInstance.Pause();
                };
                timer.AutoReset = false;
                timer.Start();
            }
        }
        public void OnBombExplosion(object sender)
        {
            PlayerExtensions.explosionSoundInstance.Play();
        }
        public void OnLevelOver(object sender, LevelOverEventArgs e)
        {
            string text;
            if (e.Winner == _game.Player)
            {
                text = e.Winner.Username + " is the winner! Congratulations!\n";
            }
            else
            {
                text = "The enemies won! Don't worry, you will get your revenge!\n";
            }
            text += "Press enter to start a new level!";
            _dialogBox.Initialize(text);
            _game.LoadNextLevel();
            Pause();
        }
        public void OnPlayerLoseLife(object sender)
        {
            //alternate between white and red player tint 10 times (blinks)
            var blinkThread = new Thread(() =>
            {
                int maxBlinks = 10;
                int crtBlinks = 0;
                while (crtBlinks < maxBlinks)
                {
                    if (crtBlinks % 2 == 0)
                    {
                        PlayerExtensions.playerTintColor = Color.White;
                    }
                    else
                    {
                        PlayerExtensions.playerTintColor = Color.Red;
                    }
                    crtBlinks++;
                    Thread.Sleep(150);
                }
                //reset the tint
                PlayerExtensions.playerTintColor = Color.White;
            });

            blinkThread.Start();

        }
        public void OnEnemyDied(object sender, IEntity enemy)
        {
            Thread thread = new Thread(() =>
            {
                Color tint = Color.White;
                Thread blinkThread = new Thread(() =>
                {
                    int maxBlinks = 10;
                    int crtBlinks = 0;
                    while (crtBlinks < maxBlinks)
                    {
                        if (crtBlinks % 2 == 0)
                        {
                            tint = Color.White;
                        }
                        else
                        {
                            tint = Color.Red;
                        }
                        crtBlinks++;
                        Thread.Sleep(150);
                    }
                    //reset the tint
                    tint = Color.White;
                });
                blinkThread.Start();
                while (blinkThread.IsAlive)
                {
                    //enemy.Draw(spriteBatch, tint);
                }
            });
            thread.Start();
        }
    }
}
