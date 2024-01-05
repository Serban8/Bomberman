using BombermanBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombermanBase
{
    public interface IEntityFactory
    {
        IEntity CreateEntity(string username, (int, int) position, int? noOfBombs = null, int? noOfLifes = null);
    }
}
