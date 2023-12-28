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
        public void Explode(Player player, TileMap tileMap)
        {
            Type = TileType.Path;
            int[] rowOffsets = { -1, 1, 0, 0, -2, 2, 0, 0 };
            int[] colOffsets = { 0, 0, -1, 1, 0, 0, -2, 2 };
            for (int i = 0; i < rowOffsets.Length; i++)
            {
                int newRow = Position.X + rowOffsets[i];
                int newCol = Position.Y + colOffsets[i];
                if(newRow > 0 && newRow < tileMap.MapSize.Width && newCol > 0 && newCol< tileMap.MapSize.Height)
                {
                    if (tileMap.Tiles[newRow,newCol].Type == TileType.BreakableWall)
                    {
                        tileMap.Tiles[newRow, newCol].Type=TileType.Path;
                    }
                }
            }
            
            player.AddBomb();
        }

        public bool IsWalkable()
        {
            return Type == TileType.Path || Type == TileType.PathWithBomb;
        }
    }
}
