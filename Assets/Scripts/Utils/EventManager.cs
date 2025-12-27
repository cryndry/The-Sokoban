using System;
using UnityEngine;

public class EventManager : LazySingleton<EventManager>
{
    public event Action<Vector2Int> OnMove;
    public void InvokeOnMove(Vector2Int movement)
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
