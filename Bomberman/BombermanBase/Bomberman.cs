using BombermanBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Xml.Linq;

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

        private Timer enemyMoveTimer = new System.Timers.Timer(800);
        private Timer bombTimer;

        private bool _paused = false;
        private bool _enemiesPaused = false;
        public bool EnemiesPaused { get => _enemiesPaused; }
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

            StartEnemiesTimer();
        }

        public void PlaceBomb()
        {
            if (_player.NoOfBombs > 0)
            {
                _player.RemoveBomb();
                
                ITile currentTile = _crtLevel.GetTile(_player.Position);
                currentTile.AddBomb();
                
                //after 5 seconds, the bomb will explode
                //on explosion, remove the bomb, notify the observers and check for collision with breakable walls or enemies
                bombTimer = new Timer(5000);
                bombTimer.Elapsed += (sender, e) =>
                {
                    //NotifyBombExploded(currentTile);
                    OnBombExplosion(currentTile);

                };
                bombTimer.AutoReset = false;
                bombTimer.Start();
            }
        }

        public void MovePlayer(int x, int y)
        {
            if (!_paused)
            {
                _player.Move(_crtLevel, x, y);
                NotifyMoveMade(_player);
                CheckPlayerEnemyCollision();
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

        public void ResumeGame()
        {
            _paused = false;
            //ResumeEnemies();
        }

        public void PauseGame()
        {
            _paused = true;
            PauseEnemies();
        }
        
        private void StartEnemiesTimer()
        {
            enemyMoveTimer.Elapsed += (sender, e) =>
            {
                foreach (var enemy in _enemies)
                {
                    enemy.Move(_crtLevel);

                    if (!CheckEnemyEnemyCollision(enemy))
                    {
                        NotifyMoveMade(enemy);
                        CheckPlayerEnemyCollision();
                    }
                }
            };
            enemyMoveTimer.AutoReset = true;
            enemyMoveTimer.Enabled = true;
        }

        public void PauseEnemies()
        {
            if (enemyMoveTimer.Enabled)
                enemyMoveTimer.Stop();
            _enemiesPaused = true;
        }

        private void ResumeEnemies()
        {
            if (!enemyMoveTimer.Enabled)
                enemyMoveTimer.Start();
            _enemiesPaused = false;
        }

        private void OnBombExplosion(ITile tile)
        {
            //destroy the bomb and detect collision with breakable walls
            _crtLevel.Explode(tile);

            //detect collision with enemies or players
            PauseEnemies();

            for (int i = 0; i < ExplosionOffsets.RowOffsets.Length; i++)
            {
                int newRow = tile.Position.X + ExplosionOffsets.RowOffsets[i];
                int newCol = tile.Position.Y + ExplosionOffsets.ColOffsets[i];
                (int X, int Y) newPosition = (newRow, newCol);

                if (newRow >= 0 && newRow < _crtLevel.MapSize.Width && newCol >= 0 && newCol < _crtLevel.MapSize.Height)
                {
                    foreach (var enemy in _enemies)
                    {
                        if (enemy.Position == newPosition)
                        {
                            _enemies.Remove(enemy);
                            //NotifyEnemyKilled(enemy);
                            break;
                        }
                    }
                }
            }
            ResumeEnemies();
        }

        private bool CheckEnemyEnemyCollision(IEntity enemy)
        {
            foreach (var otherEnemy in _enemies)
            {
                if (enemy.Position == otherEnemy.Position && enemy != otherEnemy)
                {
                    return true;
                }
            }
            return false;
        }

        private void CheckPlayerEnemyCollision()
        {
            foreach (var enemy in _enemies)
            {
                if (_player.Position == enemy.Position)
                {
                    _player.RemoveLife();
                    NotifyPlayerEnemyCollision();

                    //give player 1.5 seconds to move away from the enemy: pause the enemies and make them immortal
                    PauseEnemies();
                    _player.Immortal = true;
                    Timer resumeEnemiesTimer = new Timer(1500);
                    resumeEnemiesTimer.Elapsed += (sender, e) => { ResumeEnemies(); _player.Immortal = false; };
                    resumeEnemiesTimer.AutoReset = false;
                    resumeEnemiesTimer.Enabled = true;
                }
            }
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
        private void NotifyPlayerEnemyCollision()
        {
            foreach (var observer in _observers)
            {
                observer.OnPlayerEnemyCollision(this);
            }
        }
        #endregion
    }
}
