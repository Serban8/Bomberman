using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombermanBase.Entities
{
    public static class EntityDefaults
    {
        public static int NoOfBombs = 9;
        public static int NoOfLives = 6;
    }

    public interface IEntity
    {
        string Username { get; }
        int NoOfBombs { get; }
        int NoOfLives { get; }
        (int X, int Y) Position { get; set; }
        bool Immortal { get; set; }
        void Reset((int, int) newPos);
        void RemoveBomb();
        void AddBomb();
        void RemoveLife();
        void Move(ITileMap tileMap, int x = 0, int y = 0);
    }
}
