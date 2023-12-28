using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BombermanBase;
using BombermanMONO.LogicExtensions;
using System.Timers;

namespace Bomberman
{
    public class Bomberman : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private readonly Vector2 _windowSize;

        private Texture2D _backgroundTexture;

        private BombermanBase.Player _player;
        private BombermanBase.TileMap _tileMap;
        private BombermanBase.Player _enemy;


        private readonly int _windowBorderSize;
        Timer aTimer = new System.Timers.Timer(1000);


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
            //
        }

        protected override void Initialize()
        {
            //apply window size
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = (int)_windowSize.X;
            _graphics.PreferredBackBufferHeight = (int)_windowSize.Y;
            _graphics.ApplyChanges();

            TileMapExtensions.WindowBorderSize = _windowBorderSize;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //load the background texture
            _backgroundTexture = Content.Load<Texture2D>("Backgrounds/DarkGrayWithStars");

            TileExtensions.pathTexture = Content.Load<Texture2D>("Paths/path-small-rounded-v2");
            TileExtensions.unbreakableWallTexture = Content.Load<Texture2D>("Paths/wall");
            TileExtensions.breakableWallTexture = Content.Load<Texture2D>("Paths/breakablewall");
            TileExtensions.pathWithBombTexture = Content.Load<Texture2D>("paths/path-with-bomb");

            _tileMap = TileMapExtensions.CreateMap(_windowBorderSize, _windowSize);

            PlayerExtensions.playerTexture = Content.Load<Texture2D>("Sprites/player");
            _player = new BombermanBase.Player("bro", 2, 3, (0, 0), new PlayerMoveStrategy());

            EnemyExtensions.EnemyTexture = Content.Load<Texture2D>("Sprites/enemy2");
            _enemy = new BombermanBase.Player("abc", 4, 1, (0,0), new AIMoveStrategy());

            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += (sender, e) => _enemy.Update(_tileMap);
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            _player.Update(_tileMap);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(25, 25, 25));
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);

            _spriteBatch.Draw(_backgroundTexture, Vector2.Zero, Color.White);

            _tileMap.Draw(_spriteBatch);

            var screenCoords = _player.GetScreenCoords();
            PlayerExtensions.Draw(screenCoords, _spriteBatch);
            var screenCoordsEnemy = _enemy.GetScreenCoords();
            EnemyExtensions.Draw(screenCoordsEnemy, _spriteBatch);  
            
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
