using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombermanBase
{
    public class TileMap
    {
        private Tile[,] _tiles;
        public Tile[,] Tiles
        {
            get { return _tiles; }
            set { _tiles = value; }
        }

        private (int Width, int Height) _mapSize;
        public (int Width, int Height) MapSize
        {
            get { return _mapSize; }
        }

        public TileMap((int Width, int Height) mapSize)
        {
            _mapSize = mapSize;
            LoadMap();
        }

        public Tile GetTile((int X, int Y) pos)
        {
            if (pos.X < 0 || pos.Y < 0 || pos.X > MapSize.Width || pos.Y > MapSize.Height)
                throw new ArgumentException();
            return Tiles[pos.X, pos.Y];
        }

        private void LoadMap()
        {
            Tiles = new Tile[_mapSize.Width, _mapSize.Height];

            for (int x = 0; x < _mapSize.Width; x++)
            {
                for (int y = 0; y < _mapSize.Height; y++)
                {
                    Tiles[x, y] = new Tile((x, y), TileType.Path);
                }
            }
            Console.WriteLine("Map loaded " + _mapSize.Width.ToString() + " " + _mapSize.Height.ToString());
        }
    }
}
