using BombermanBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace BombermanBase
{
    public class MoveEventArgs : EventArgs { };
    public class GameOverEventArgs : EventArgs { };

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

        public Bomberman(IEntity player, List<IEntity> enemies)
        {
            _player = player;
            _enemies = enemies;
            _levels = new List<ITileMap>();
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
                }
            };
            enemyMoveTimer.AutoReset = true;
            enemyMoveTimer.Enabled = true;
        }

        public void AddObserver(IBombermanObserver observer)
        {
            throw new NotImplementedException();
        }

        public void RemoveObserver(IBombermanObserver observer)
        {
            throw new NotImplementedException();
        }

        private void NotifyMoveMade()
        {
            throw new NotImplementedException();
        }
        private void NotifyGameOver()
        {
            throw new NotImplementedException();
        }
    }
}
