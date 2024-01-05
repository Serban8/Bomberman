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
        (int X, int Y) PlayerSpawnPoint { get; }
        List<(int X, int Y)> EnemySpawnPoints { get; }
        ITile GetTile((int X, int Y) pos);
        void Explode(ITile tile);
        void LoadMap(string mapFilePath);
    }
}
