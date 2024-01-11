using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Timer = System.Timers.Timer;
using BombermanBase.Entities;

namespace BombermanBase
{
    public static class TileMapFactory
    {
        public static ITileMap CreateTileMap((int Width, int Height) mapSize, string mapFilePath)
        {
            return new TileMap(mapSize, mapFilePath);
        }
    }
    internal class TileMap : ITileMap
    {
        private ITile[,] _tiles;
        public ITile[,] Tiles
        {
            get { return _tiles; }
        }

        private (int Width, int Height) _mapSize;
        public (int Width, int Height) MapSize { get => _mapSize; }

        private (int X, int Y) _playerSpawnPoint;
        public (int X, int Y) PlayerSpawnPoint { get => _playerSpawnPoint; }

        private List<(int X, int Y)> _enemySpawnPoints;
        public List<(int X, int Y)> EnemySpawnPoints { get => _enemySpawnPoints; }

        public TileMap((int Width, int Height) mapSize, string mapFilePath)
        {
            _mapSize = mapSize;
            _tiles = new Tile[mapSize.Width, mapSize.Height];
            _enemySpawnPoints = new List<(int X, int Y)>();
            LoadMap(mapFilePath);
        }

        public ITile GetTile((int X, int Y) pos)
        {
            if (pos.X < 0 || pos.Y < 0 || pos.X >= MapSize.Width || pos.Y >= MapSize.Height)
                throw new ArgumentException();
            return Tiles[pos.X, pos.Y];
        }

        public void Explode(ITile tile)
        {
            (int X, int Y) position = tile.Position;

            tile.Explode();

            for (int i = 0; i < ExplosionOffsets.RowOffsets.Length; i++)
            {
                int newRow = position.X + ExplosionOffsets.RowOffsets[i];
                int newCol = position.Y + ExplosionOffsets.ColOffsets[i];
                if (newRow > 0 && newRow < MapSize.Width && newCol > 0 && newCol < MapSize.Height)
                {
                    if (Tiles[newRow, newCol].Type == TileType.BreakableWall)
                    {
                        Tiles[newRow, newCol].Explode();
                    }
                }
            }
        }

        /// <summary>
        /// Loads a map from a txt file. The map is a grid of characters, where each character represents a tile.
        /// The tiles are encoded as follows: p = path, u = unbreakable wall, b = breakable wall, s = spawn point, e = enemy spawn point
        /// </summary>
        /// <param name="mapFilePath"></param>
        public void LoadMap(string mapFilePath)
        {
            //modify to use ITile and CreateTile
            string[] lines = File.ReadAllLines(mapFilePath);

            int width = MapSize.Width;
            int height = MapSize.Height;
            if (width == 0 || height == 0)
            {
                width = lines[0].Length;
                height = lines.Length;
            }

            for (int y = 0; y < height; y++)
            {
                string line = lines[y].ToString();

                for (int x = 0; x < width; x++)
                {
                    if (line[x].ToString() == "p")
                    {
                        Tiles[x, y] = TileFactory.CreateTile((x, y), TileType.Path);
                    }
                    else if (line[x].ToString() == "u")
                    {
                        Tiles[x, y] = TileFactory.CreateTile((x, y), TileType.UnbreakableWall);
                    }
                    else if (line[x].ToString() == "b")
                    {
                        Tiles[x, y] = TileFactory.CreateTile((x, y), TileType.BreakableWall);
                    }
                    else if (line[x].ToString() == "s")
                    {
                        Tiles[x, y] = TileFactory.CreateTile((x, y), TileType.Path);
                        _playerSpawnPoint = (x, y);
                    }
                    else if (line[x].ToString() == "e")
                    {
                        _enemySpawnPoints.Add((x, y));
                        Tiles[x, y] = TileFactory.CreateTile((x, y), TileType.Path);
                    }
                    else
                    {
                        //throw new Exception("Invalid map format");
                        Tiles[x, y] = TileFactory.CreateTile((x, y), TileType.Path);
                    }
                }
            }
        }
    }
}
