using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombermanBase
{
    internal class Tile : ITile
    {
        public (int X, int Y) Position { get; }
        private TileType _type;
        public TileType Type { get => _type; }
        public Tile((int, int) tilePosition, TileType tileType)
        {
            Position = tilePosition;
            _type = tileType;
        }

        public void Destroy()
        {
            if (Type == TileType.BreakableWall)
            {
                _type = TileType.Path;
            }
        }
        public void AddBomb()
        {
            if (Type == TileType.Path)
            {
                _type = TileType.PathWithBomb;
            }
        }

        public void Explode()
        {
            if (Type != TileType.UnbreakableWall)
            {
                _type = TileType.Path;
            }
        }

        public bool IsWalkable()
        {
            return Type == TileType.Path || Type == TileType.PathWithBomb;
        }
    }
}
