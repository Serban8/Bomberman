using BombermanBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombermanBase
{
    public interface ITileMap
    {
        ITile[,] Tiles { get; }
        (int Width, int Height) MapSize { get; }
        ITile GetTile((int X, int Y) pos);
        void PlaceBomb(IEntity player);
        void Explode(ITile tile);
        void LoadMap(string mapFilePath);
    }
}
