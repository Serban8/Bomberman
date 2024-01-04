using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombermanBase.Entities
{
    public interface IEntity
    {
        string Username { get; }
        int NoOfBombs { get; }
        int NoOfLives { get; }
        (int X, int Y) Position { get; }
        void RemoveBomb();
        void AddBomb();
        void Move(ITileMap tileMap, int x = 0, int y = 0);
    }
}
