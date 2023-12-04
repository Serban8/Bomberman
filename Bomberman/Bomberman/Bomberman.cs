using Bomberman.NewFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Bomberman
{
    public class Bomberman : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private readonly Vector2 _windowSize;

        private Texture2D _backgroundTexture;

        private Player _player;

        private Tile[,] _tiles;
        private Point _tilesSize;
        private readonly int _tileBorderSize = 5;
        private Texture2D _tileTexture;
        private readonly int _windowBorderSize;

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
            
            //load the tile texture
            _tileTexture = Content.Load<Texture2D>("Paths/path-small-rounded-v2");

            //calculate the number of tiles that can fit in the window taking the border into account
            int tilesWidth = (int)((_windowSize.X - 2 * _windowBorderSize) / (_tileTexture.Width + _tileBorderSize));
            int tilesHeight = (int)((_windowSize.Y - 2 * _windowBorderSize) / (_tileTexture.Height + _tileBorderSize));
            _tilesSize = new Point(tilesWidth, tilesHeight);
            _tiles = new Tile[tilesWidth, tilesHeight];

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //load the background texture
            _backgroundTexture = Content.Load<Texture2D>("Backgrounds/DarkGrayWithStars");

            int posX = _windowBorderSize;
            int posY = _windowBorderSize;
            for (int x = 0; x < _tilesSize.X; x++)
            {
                for (int y = 0; y < _tilesSize.Y; y++)
                {
                    _tiles[x, y] = new Tile(_tileTexture, new Vector2(posX, posY), TileType.Path);
                    posY += _tileTexture.Height + _tileBorderSize;
                }
                posX += _tileTexture.Width + _tileBorderSize;
                posY = _windowBorderSize;
            }

            _player = new Player(Content.Load<Texture2D>("Sprites/player"),
                      new Vector2(_windowBorderSize, _windowBorderSize),
                      200f,
                      (_tiles[0, 0].Position, 
                        new Vector2(_tiles[_tilesSize.X - 1, _tilesSize.Y - 1].Position.X + _tileTexture.Width, 
                                   _tiles[_tilesSize.X - 1, _tilesSize.Y - 1].Position.Y + _tileTexture.Height)));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _player.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(25, 25, 25));
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);


            _spriteBatch.Draw(_backgroundTexture, Vector2.Zero, Color.White);

            for (int i = 0; i < _tilesSize.X; i++)
            {
                for (int j = 0; j < _tilesSize.Y; j++)
                {
                    _tiles[i, j].Draw(_spriteBatch);
                }
            }

            _player.Draw(_spriteBatch);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
