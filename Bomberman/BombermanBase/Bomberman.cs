using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private List<IObserver<Bomberman>> _observers;

        public Bomberman()
        {
            _observers = new List<IObserver<Bomberman>>();
        }

        public void CreateGame()
        {
            throw new NotImplementedException();
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
