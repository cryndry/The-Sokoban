using System;
using UnityEngine;

public class EventManager : LazySingleton<EventManager>
{
    public event Action<Vector2> OnMove;
    public void InvokeOnMove(Vector2 movement)
    {
        OnMove?.Invoke(movement);
    }

    public event Action OnUndo;
    public void InvokeOnUndo()
    {
        OnUndo?.Invoke();
    }

    public event Action OnRedo;
    public void InvokeOnRedo()
    {
        OnRedo?.Invoke();
    }
}
