using System.Collections.Generic;
using UnityEngine;

public class Subject<T> : MonoBehaviour
{
    List<IObserver> _observers = new List<IObserver>();

    protected void NotifyObservers(T data)
    {
        foreach (var observer in _observers)
        {
            observer.Notify(data);
        }
    }

    public void AddObserver(IObserver obs)
    {
        _observers.Add(obs);
    }

    public void RemoveObserver(IObserver obs)
    {
        Debug.Log(_observers.Count);
        _observers.Remove(obs);
    }
}
