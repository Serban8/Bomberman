using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace BombermanBase
{
    public class Unsubscriber : IDisposable
    {
        private List<IObserver<Bomberman>> _observers;
        private IObserver<Bomberman> _observer;

        public Unsubscriber(List<IObserver<Bomberman>> observers, IObserver<Bomberman> observer)
        {
            _observers = observers;
            _observer = observer;
        }

        public void Dispose()
        {
            if (_observers.Contains(_observer))
            {
                _observers.Remove(_observer);
            }
        }
    }

    public class Bomberman : IBomberman, IObservable<Bomberman>
    {
        private readonly List<IObserver<Bomberman>> _observers;

        private BombermanBase.TileMap _tileMap;
        private BombermanBase.Entity _player;
        private BombermanBase.Entity _enemy;


        Timer enemyMoveTimer = new System.Timers.Timer(800);

        public Bomberman()
        {
            _observers = new List<IObserver<Bomberman>>();
        }

        public void CreateGame(TileMap tileMap)
        {
            _player = new BombermanBase.PlayerFactory().CreateEntity("abc", 1, 1);
            _enemy = new BombermanBase.EnemyFactory().CreateEntity("abc", 4, 1);
            _tileMap = tileMap;

            enemyMoveTimer.Elapsed += (sender, e) => { _enemy.Move(_tileMap); NotifyMoveMade(); };
            enemyMoveTimer.AutoReset = true;
            enemyMoveTimer.Enabled = true;
        }

        public void NotifyMoveMade()
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(this);
            }
        }

        public IDisposable Subscribe(IObserver<Bomberman> observer)
        {
            if(!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }

            return new Unsubscriber(_observers, observer);
        }
    }
}
