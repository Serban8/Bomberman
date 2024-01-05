using BombermanBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace BombermanBase
{
    public class MoveEventArgs : EventArgs
    {
        public IEntity Entity { get; set; }
        public MoveEventArgs(IEntity entity)
        {
            Entity = entity;
        }
    };
    public class GameOverEventArgs : EventArgs
    {
        public IEntity Winner { get; set; }
        public GameOverEventArgs(IEntity entity)
        {
            Winner = entity;
        }
    };

    public static class BombermanFactory
    {
        public static IBomberman CreateGame(string username)
        {
            IEntity player = new PlayerFactory().CreateEntity(username);
            List<IEntity> enemies = new List<IEntity>() { new EnemyFactory().CreateEntity("YoloBOMB"), new EnemyFactory().CreateEntity("enemy2") };
            Bomberman game = new Bomberman(player, enemies);

            return game;
        }
    }

    internal class Bomberman : IBomberman
    {
        private List<ITileMap> _levels;

        private ITileMap _crtLevel;
        public ITileMap CrtLevel { get => _crtLevel; }

        private IEntity _player;
        public IEntity Player { get => _player; }
        private List<IEntity> _enemies;
        public List<IEntity> Enemies { get => _enemies; }

        Timer enemyMoveTimer = new System.Timers.Timer(1000);

        private bool _paused = false;
        private List<IBombermanObserver> _observers;

        public Bomberman(IEntity player, List<IEntity> enemies)
        {
            _player = player;
            _enemies = enemies;
            _levels = new List<ITileMap>();
            _observers = new List<IBombermanObserver>();
        }

        public void AddLevel(ITileMap tileMap)
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
                    NotifyMoveMade(enemy);
                }
            };
            enemyMoveTimer.AutoReset = true;
            enemyMoveTimer.Enabled = true;
        }

        public void MovePlayer(int x, int y)
        {
            if (!_paused)
            {
                _player.Move(_crtLevel, x, y);
                NotifyMoveMade(_player);
            }
        }

        public void CheckGameOver()
        {
            if (_player.NoOfLives == 0)
            {
                NotifyGameOver(_enemies[0]);
            }
            else if (_enemies.Count == 0)
            {
                NotifyGameOver(_player);
            }
        }

        public void Pause()
        {
            _paused = true;
            if (enemyMoveTimer.Enabled)
                enemyMoveTimer.Stop();
        }

        public void Resume()
        {
            _paused = false;
            if (!enemyMoveTimer.Enabled)
                enemyMoveTimer.Start();
        }

        #region Observer-related methods
        public void AddObserver(IBombermanObserver observer)
        {
            _observers.Add(observer);
        }
        public void RemoveObserver(IBombermanObserver observer)
        {
            _observers.Remove(observer);
        }

        private void NotifyMoveMade(IEntity entity)
        {
            foreach (var observer in _observers)
            {
                observer.OnMoveMade(this, new MoveEventArgs(entity));
            }
        }
        private void NotifyGameOver(IEntity entity)
        {
            foreach (var observer in _observers)
            {
                observer.OnGameOver(this, new GameOverEventArgs(entity));
            }
        }
        #endregion
    }
}
