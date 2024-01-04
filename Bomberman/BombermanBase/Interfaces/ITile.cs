using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombermanBase
{
    public enum TileType
    {
        Path,
        UnbreakableWall,
        BreakableWall,
        PathWithBomb
    }
    public interface ITile
    {
        (int X, int Y) Position { get; }
        TileType Type { get; }
        void Destroy();
        void AddBomb();
        void Explode();
        bool IsWalkable();
    }
}
