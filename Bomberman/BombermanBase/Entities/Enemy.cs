using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombermanBase
{
    public class Enemy : Entity
    {
        private float movementTimer;
        private float movementInterval;
        public Enemy(string username, int noOfBombs, int noOfLives, (int, int) position, float movementInterval, IMoveStrategy ms) 
            : base(username, noOfBombs, noOfLives, position, ms)
        {
            this.movementInterval = movementInterval;
            movementTimer = movementInterval;
        }

        public void ChangeDirection()
        {

            Random random = new Random();
            int direction = random.Next(4);

            switch (direction)
            {
                case 0: // Up
                    Position = (Position.X - 1, Position.Y);
                    break;
                case 1: // Down
                    Position = (Position.X + 1, Position.Y);
                    break;
                case 2: // Left
                    Position = (Position.X, Position.Y - 1);
                    break;
                case 3: // Right
                    Position = (Position.X, Position.Y + 1);
                    break;


            }
        }
    }
}
