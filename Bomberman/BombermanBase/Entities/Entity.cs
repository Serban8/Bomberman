using BombermanBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BombermanBase
{
    internal class Entity : IEntity
    {
        private string _username;
        public string Username { get => _username; }

        private int _noOfBombs;
        public int NoOfBombs { get => _noOfBombs; }

        private int _noOfLives;
        public int NoOfLives { get => _noOfLives; }

        private (int, int) _position;
        public (int X, int Y) Position { get => _position; }

        private IMoveStrategy _moveStrategy;

        public Entity(string username, int noOfBombs, int noOfLives, (int, int) position, IMoveStrategy moveStrategy)
        {
            _username = username;
            _noOfBombs = noOfBombs;
            _noOfLives = noOfLives;
            _position = position;
            _moveStrategy = moveStrategy;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Entity otherPlayer = (Entity)obj;

            
            return this._username == otherPlayer.Username && this._noOfBombs == otherPlayer.NoOfBombs && this._noOfLives == otherPlayer.NoOfLives
                && this._position == otherPlayer.Position;

        }

        public void RemoveBomb()
        {
            if( _noOfBombs > 0 )
            {
                _noOfBombs--;
            }
        }

        public void AddBomb()
        {
            _noOfBombs++;
        }

        public void RemoveLife()
        {
            _noOfLives--;
        }

        public void Move(ITileMap tileMap, int x = 0, int y = 0)
        {
            _position = _moveStrategy.Move(tileMap, Position, x, y);
            // Logger.Log("Next pos for " + Username + ": " + Position.ToString());
        }
    }
}
