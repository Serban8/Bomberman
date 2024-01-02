using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombermanBase
{
    public class PlayerFactory : IEntityFactory
    {
        public Entity CreateEntity(string username, int noOfBombs = 4, int noOfLifes = 3)
        {
            return new Entity(username, noOfBombs, noOfLifes, (0, 0), new PlayerMoveStrategy());
        }
    }
}
