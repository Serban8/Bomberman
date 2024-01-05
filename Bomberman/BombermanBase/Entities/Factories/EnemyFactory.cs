﻿using BombermanBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombermanBase
{
    public class EnemyFactory : IEntityFactory
    {
        public IEntity CreateEntity(string username, int noOfBombs = 4, int noOfLifes = 1)
        {
            return new Entity(username, noOfBombs, noOfLifes, (0, 0), new AIMoveStrategy());
        }
    }
}
