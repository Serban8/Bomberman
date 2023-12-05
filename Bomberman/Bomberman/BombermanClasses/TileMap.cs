using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman.BombermanClasses
{
    internal class TileMap
    {
        private Tile[,] _tiles;
        public Tile[,] Tiles
        {
            get { return _tiles; }
        }

        private Point _mapSize;
        public Point MapSize
        {
            get { return _mapSize; }
        }

        private readonly int _tileBorderSize = 5;

        private ContentManager _content;

        public TileMap(ContentManager content, Vector2 windowSize, int windowBorderSize)
        {
            _content = content;
            _tiles = new Tile[_mapSize.X, _mapSize.Y];
            GenerateMap(windowBorderSize, windowSize);
        }

        private void GenerateMap(int windowBorderSize, Vector2 windowSize)
        {
            int posX = windowBorderSize;
            int posY = windowBorderSize;

            Texture2D tileTexture = _content.Load<Texture2D>("Paths/path-small-rounded-v2");

            //calculate the number of tiles that can fit in the window taking the border into account
            int tilesWidth = (int)((windowSize.X - 2 * windowBorderSize) / (tileTexture.Width + _tileBorderSize));
            int tilesHeight = (int)((windowSize.Y - 2 * windowBorderSize) / (tileTexture.Height + _tileBorderSize));
            _mapSize = new Point(tilesWidth, tilesHeight);
            _tiles = new Tile[tilesWidth, tilesHeight];

            for (int x = 0; x < _mapSize.X; x++)
            {
                for (int y = 0; y < _mapSize.Y; y++)
                {
                    _tiles[x, y] = new Tile(tileTexture, new Vector2(posX, posY), TileType.Path);
                    posY += tileTexture.Height + _tileBorderSize;
                }
                posX += tileTexture.Width + _tileBorderSize;
                posY = windowBorderSize;
            }
        }

        public (Vector2, Vector2) GetPlayerBoundries()
        {
            Texture2D tileTexture = _content.Load<Texture2D>("Paths/path-small-rounded-v2");

            return (Tiles[0, 0].Position,
                new Vector2(Tiles[_mapSize.X - 1, _mapSize.Y - 1].Position.X + tileTexture.Width,
                            Tiles[_mapSize.X - 1, _mapSize.Y - 1].Position.Y + tileTexture.Height));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _mapSize.X; i++)
            {
                for (int j = 0; j < _mapSize.Y; j++)
                {
                    _tiles[i, j].Draw(spriteBatch);
                }
            }
        }
    }
}
