using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombermanBase
{
    public class AIMoveStrategy : IMoveStrategy
    {
        public (int, int) Move(TileMap tileMap, (int X, int Y) crtPos, int xMove, int yMove)
        {
            //xMove and yMove are not used - instead determine where the AI should move

            //try to determine a valid move - capped at 100 tries to avoid infinite loops
            for (int i = 0; i < 100; ++i)
            {
                Random rnd = new Random();
                int x = rnd.Next(-1, 2);
                int y = rnd.Next(-1, 2);

                try
                {
                    Tile nextTile = tileMap.GetTile((crtPos.X + x, crtPos.Y + y));

                    //System.Console.WriteLine(nextTile.Position.ToString());

                    if (nextTile.IsWalkable())
                    {
                        return (crtPos.X + x, crtPos.Y + y);
                    }
                }
                catch
                {
                    var mapDimensions = tileMap.MapSize;

                    int randomX = crtPos.X + x;
                    int randomY = crtPos.Y + y;

                    if (randomX >= mapDimensions.Width)
                    {
                        randomX = mapDimensions.Width - 1;
                    }
                    else if (randomX < 0)
                    {
                        randomX = 0;
                    }

                    if (randomY >= mapDimensions.Height)
                    {
                        randomY = mapDimensions.Height - 1;
                    }
                    else if (randomY < 0)
                    {
                        randomY = 0;
                    }

                    Tile nextTile = tileMap.GetTile((randomX, randomY));

                    //System.Console.WriteLine(nextTile.Position.ToString());

                    if (nextTile.IsWalkable())
                    {
                        return (randomX, randomY);
                    }
                }
            }
            return crtPos;
        }
    }
}
