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
        public (int Width, int Height) MapSize
        {
            get { return _mapSize; }
        }

        private static Timer bombTimer;

        public TileMap((int Width, int Height) mapSize, string mapFilePath)
        {
            _mapSize = mapSize;
            _tiles = new Tile[mapSize.Width, mapSize.Height];
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

        public void LoadMap(string mapFilePath)
        {
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

            //Console.WriteLine("Map loaded " + MapSize.Width.ToString() + " " + MapSize.Height.ToString());
        }
    }
}
