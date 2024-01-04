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

        private List<TileMap> _levels;

        private TileMap _crtLevel;
        public TileMap CrtLevel { get => _crtLevel; }
        
        private Entity _player;
        public Entity Player { get => _player; }
        private List<Entity> _enemies;
        public List<Entity> Enemies { get => _enemies; }


        Timer enemyMoveTimer = new System.Timers.Timer(1000);

        public Bomberman()
        {
            _observers = new List<IObserver<Bomberman>>();
        }

        public void CreateGame()
        {
            _player = new PlayerFactory().CreateEntity("GigelGigel");
            _enemies = new List<Entity>() { new EnemyFactory().CreateEntity("YoloBOMB"), new EnemyFactory().CreateEntity("enemy2") };
            _levels = new List<TileMap>();
        }

        public void AddLevel(TileMap tileMap)
        {
            _levels.Add(tileMap);

            if (_levels.Count == 1)
            {
                LoadLevel(0);
            }
        }

        public void LoadLevel(int levelNo)
        {
            if (levelNo < 0 || levelNo >= _levels.Count)
                throw new ArgumentException();

            _crtLevel = _levels[levelNo];

            enemyMoveTimer.Elapsed += (sender, e) => 
            {
                foreach (var enemy in _enemies)
                {
                    enemy.Move(_crtLevel); 
                    NotifyMoveMade();
                }
            };
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
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }

            return new Unsubscriber(_observers, observer);
        }
    }
}
