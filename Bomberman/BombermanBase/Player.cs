using System;
using System.Collections.Generic;
using System.Linq;
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

        public Player(string username, int noOfBombs, int noOfLives, (int, int) position)
        {
            Username = username;
            NoOfBombs = noOfBombs;
            NoOfLives = noOfLives;
            Position = position;
        }

        public void Move(int x, int y)
        {
            Position = (Position.X + x, Position.Y + y);
        }
    }
}
