using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Timer = System.Timers.Timer;

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

        private static Timer bombTimer;

        public TileMap((int Width, int Height) mapSize)
        {
            _mapSize = mapSize;
            LoadMap("C:\\Users\\mihai\\OneDrive\\Materiale cursuri\\Anul3\\IS\\proiect\\Bomberman\\Bomberman\\Bomberman\\Content\\Level\\Level1.txt");
        }

        public Tile GetTile((int X, int Y) pos)
        {
            if (pos.X < 0 || pos.Y < 0 || pos.X > MapSize.Width || pos.Y > MapSize.Height)
                throw new ArgumentException();
            return Tiles[pos.X, pos.Y];
        }

        public void PlaceBomb(Entity player)
        {
            if (player.NoOfBombs > 0)
            {
                Tile currentTile = GetTile(player.Position);
                currentTile.AddBomb();
                player.RemoveBomb();
                bombTimer = new Timer(5000);
                bombTimer.Elapsed += (sender, e) => Explode(currentTile);
                bombTimer.AutoReset = false;
                bombTimer.Start();
            }
        }

        public void Explode(Tile tile)
        {
            (int X, int Y) position = tile.Position;

            tile.Explode();

            int[] rowOffsets = { -1, 1, 0, 0, -2, 2, 0, 0 };
            int[] colOffsets = { 0, 0, -1, 1, 0, 0, -2, 2 };

            for (int i = 0; i < rowOffsets.Length; i++)
            {
                int newRow = position.X + rowOffsets[i];
                int newCol = position.Y + colOffsets[i];
                if (newRow > 0 && newRow < MapSize.Width && newCol > 0 && newCol < MapSize.Height)
                {
                    if (Tiles[newRow, newCol].Type == TileType.BreakableWall)
                    {
                        Tiles[newRow, newCol].Explode();
                    }
                }
            }
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
                    else
                    {
                        //throw new Exception("Invalid map format");
                        Tiles[x, y] = new Tile((x, y), TileType.Path);
                    }
                }
            }
            //Console.WriteLine("Map loaded " + _mapSize.Width.ToString() + " " + _mapSize.Height.ToString());
        }
    }
}
