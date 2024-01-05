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

    public static class ExplosionOffsets
    {
        public static readonly int[] RowOffsets = { -1, 1, 0, 0, 0, -2, 2, 0, 0 };
        public static readonly int[] ColOffsets = { 0, 0, -1, 1, 0, 0, 0, -2, 2 };
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
