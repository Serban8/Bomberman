using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombermanBase
{
    public class PlayerMoveStrategy : IMoveStrategy
    {
        public (int, int) Move(TileMap tileMap, (int X, int Y) crtPos, int xMove, int yMove)
        {
            try
            {
                Tile nextTile = tileMap.GetTile((crtPos.X + xMove, crtPos.Y + yMove));
                if (nextTile.IsWalkable())
                    return (crtPos.X + xMove, crtPos.Y + yMove);
            }
            catch
            {
                return crtPos;
            }

            return crtPos;
        }
    }
}
