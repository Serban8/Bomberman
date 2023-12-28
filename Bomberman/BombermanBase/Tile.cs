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

    public class Tile
    {
        public (int X, int Y) Position { get; set; }
        public TileType Type { get; set; }
        public Tile((int, int) tilePosition, TileType tileType)
        {
            Position = tilePosition;
            Type = tileType;
        }
       
        public void Destroy()
        {
            Type = TileType.Path;
        }
        public void AddBomb()
        {
            Type = TileType.PathWithBomb;
        }

        public bool IsWalkable()
        {
            return Type == TileType.Path || Type == TileType.PathWithBomb;
        }
    }
}
