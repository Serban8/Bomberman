using BombermanMONO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BombermanBase
{
    public class Player
    {
        private string _username;
        public string Username
        {
            get
            {
                return _username;
            } 
            set
            {
                _username = value;
            }
        }
        
        private int _noOfBombs;
        public int NoOfBombs
        { 
            get
            {
                return _noOfBombs;
            } 
            set
            {
                _noOfBombs = value;
            }
        }
        
        private int _noOfLives;
        public int NoOfLives
        {
            get
            {
                return _noOfLives;
            } 
            set
            {
                _noOfLives = value;
            }
        }
        
        private (int, int) _position;
        public (int X, int Y) Position
        {
            get
            {
                return _position;
            } 
            set
            {
                _position = value;
            }
        }

        public IMoveStrategy MoveStrategy;

        public Player(string username, int noOfBombs, int noOfLives, (int, int) position, IMoveStrategy moveStrategy)
        {
            Username = username;
            NoOfBombs = noOfBombs;
            NoOfLives = noOfLives;
            Position = position;
            MoveStrategy = moveStrategy;
        }
        public void RemoveBomb()
        {
            NoOfBombs--;
        }
        public void AddBomb()
        {
            NoOfBombs++;
        }

        public void SetMoveStrategy(IMoveStrategy moveStrategy)
        {
            MoveStrategy = moveStrategy;
        }

        public void Move(TileMap tileMap, int x = 0, int y = 0)
        {
            Position = MoveStrategy.Move(tileMap, Position, x, y);
           // Logger.Log("Next pos for " + Username + ": " + Position.ToString());
        }
    }
}
