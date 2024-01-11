using BombermanBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombermanBase
{
    public interface IBombermanObserver
    {
        void OnPlayerMoved(object sender, MoveEventArgs e);
        void OnBombExplosion(object sender);
        void OnLevelOver(object sender, LevelOverEventArgs e);
        void OnPlayerLoseLife(object sender);
        void OnEnemyDied(object sender, IEntity enemy);
    }
}
