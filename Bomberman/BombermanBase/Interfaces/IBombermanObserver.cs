using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombermanBase
{
    public interface IBombermanObserver
    {
        void OnMoveMade(object sender, MoveEventArgs e);
        void OnLevelOver(object sender, LevelOverEventArgs e);
        void OnPlayerEnemyCollision(object sender);
    }
}
