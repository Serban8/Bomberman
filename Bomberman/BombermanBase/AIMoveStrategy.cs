﻿using System;
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

            Random rnd = new Random();
            int x = rnd.Next(-1, 2);
            int y = rnd.Next(-1, 2);

            try
            {
                Tile nextTile = tileMap.GetTile((crtPos.X + x, crtPos.Y + y));

                System.Console.WriteLine(nextTile.Position.ToString());

                if (nextTile.IsWalkable())
                {
                    return (crtPos.X + x, crtPos.Y + y);
                }
            }
            catch
            {
                var mapDimensions = tileMap.MapSize;

                bool xInvalid = false;
                bool yInvalid = true;

                int randomX = x;
                int randomY = y;

                if (crtPos.X + x >= mapDimensions.Width)
                {
                    x--;
                    xInvalid = true;
                }
                else if (crtPos.X < 0)
                {
                    x++;
                    xInvalid = true;
                }
                
                if (crtPos.Y + y >= mapDimensions.Height)
                {
                    y--;
                    yInvalid = true;
                }
                else if (crtPos.Y < 0)
                {
                    y++;
                    yInvalid = true;
                }

                if (xInvalid && !yInvalid)
                {
                    x = crtPos.X;
                }
                else if (yInvalid && !xInvalid) 
                {
                    y = crtPos.Y;
                }
                else
                {
                    x = crtPos.X;
                    y = crtPos.Y;
                }


                Tile nextTile = tileMap.GetTile((crtPos.X + x, crtPos.Y + y));

                System.Console.WriteLine(nextTile.Position.ToString());

                if (nextTile.IsWalkable())
                {
                    return (crtPos.X + y, crtPos.Y + y);
                }
            }
            return crtPos;
        }
    }
}