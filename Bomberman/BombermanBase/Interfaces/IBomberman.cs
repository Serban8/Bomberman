﻿using BombermanBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombermanBase
{
    //public class Unsubscriber : IDisposable
    //{
    //    private List<IObserver<Bomberman>> _observers;
    //    private IObserver<Bomberman> _observer;

    //    public Unsubscriber(List<IObserver<Bomberman>> observers, IObserver<Bomberman> observer)
    //    {
    //        _observers = observers;
    //        _observer = observer;
    //    }

    //    public void Dispose()
    //    {
    //        if (_observers.Contains(_observer))
    //        {
    //            _observers.Remove(_observer);
    //        }
    //    }
    //}

    public interface IBomberman
    {
        IEntity Player { get; }
        List<IEntity> Enemies { get; }
        ITileMap CrtLevel { get; }

        void AddLevel(ITileMap tileMap);
        void LoadNextLevel();
        void MovePlayer(int x, int y);
        void CheckLevelOver();
        void PlaceBomb();
        void PauseGame();
        void PauseEnemies(); //remove
        void ResumeGame();
        void AddObserver(IBombermanObserver observer);
        void RemoveObserver(IBombermanObserver observer);
    }
}
