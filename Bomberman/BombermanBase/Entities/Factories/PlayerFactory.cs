using BombermanBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombermanBase
{
    public class PlayerFactory : IEntityFactory
    {
        public IEntity CreateEntity(string username, (int, int) position, int? noOfBombs = null, int? noOfLives = null)
        {
            if (noOfBombs != null && noOfLives != null)
            {
                return new Entity(username, (int)noOfBombs, (int)noOfLives, position, new PlayerMoveStrategy());
            }

            return new Entity(username, 999, 999, position, new PlayerMoveStrategy());
        }
    }
}
