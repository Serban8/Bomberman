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
            if(Type == TileType.BreakableWall)
            {
                Type = TileType.Path;
            }
        }
        public void AddBomb()
        {
            if(Type == TileType.Path)
            {
                Type = TileType.PathWithBomb;
            }
        }
        //public void Explode(Player player)
        public void Explode()
        {
            if(Type != TileType.UnbreakableWall)
            {
                Type = TileType.Path;
            }
          
            //player.AddBomb();
        }

        public bool IsWalkable()
        {
            return Type == TileType.Path || Type == TileType.PathWithBomb;
        }
    }
}
