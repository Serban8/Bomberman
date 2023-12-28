using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
            LoadMap("C:\\Users\\Legion\\Desktop\\Portofolii\\an3\\IS\\Bomberman repo\\Bomberman\\Bomberman\\Content\\Level\\Level1.txt");
        }

        public Tile GetTile((int X, int Y) pos)
        {
            if (pos.X < 0 || pos.Y < 0 || pos.X > MapSize.Width || pos.Y > MapSize.Height)
                throw new ArgumentException();
            return Tiles[pos.X, pos.Y];
        }

        private void LoadMap(string filemap)
        {
            Tiles = new Tile[_mapSize.Width, _mapSize.Height];
            string[] lines = File.ReadAllLines(filemap);

            for (int y = 0; y < _mapSize.Height; y++)
            {
                string line = lines[y].ToString();

                for (int x = 0; x < _mapSize.Width; x++)
                {
                    if (line[x].ToString() == "p")
                    {
                        Tiles[x, y] = new Tile((x, y), TileType.Path);
                    }
                    else if (line[x].ToString() == "u")
                    {
                        Tiles[x, y] = new Tile((x, y), TileType.UnbreakableWall);
                    }
                    else if (line[x].ToString() == "b")
                    {
                        Tiles[x, y] = new Tile((x, y), TileType.BreakableWall);
                    }
                }
            }
            Console.WriteLine("Map loaded " + _mapSize.Width.ToString() + " " + _mapSize.Height.ToString());
        }
    }
}
