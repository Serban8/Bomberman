using BombermanBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombermanBase
{
    public class EnemyFactory : IEntityFactory
    {
        public IEntity CreateEntity(string username, (int, int) position, int? noOfBombs = null, int? noOfLives = null)
        {
            return new Entity(username, EntityDefaults.NoOfBombs, EntityDefaults.NoOfLives, position, new AIMoveStrategy());
        }
    }
}
