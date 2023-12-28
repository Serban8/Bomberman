using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BombermanBase
{
    public interface IMoveStrategy
    {
        (int, int) Move(TileMap tileMap, (int X, int Y) crtPos, int xMove, int yMove);
    }
}
