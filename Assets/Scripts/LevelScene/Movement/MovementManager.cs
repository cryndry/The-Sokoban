using System.Collections.Generic;

public class MovementManager : LazySingleton<MovementManager>
{
    private Stack<List<Movement>> undoStack;
    private Stack<List<Movement>> redoStack;

    private void OnEnable()
    {
        undoStack = new Stack<List<Movement>>();
        redoStack = new Stack<List<Movement>>();

        EventManager.Instance.OnUndo += OnUndo;
        EventManager.Instance.OnRedo += OnRedo;
    }

    private void OnDisable()
    {
        undoStack = null;
        redoStack = null;

        EventManager.Instance.OnUndo -= OnUndo;
        EventManager.Instance.OnRedo -= OnRedo;
    }

    public void RegisterMovement(List<Movement> movements)
    {
        if (movements == null || movements.Count == 0) return;

        redoStack.Clear();
        undoStack.Push(movements);
    }

    public void OnUndo()
    {
        if (undoStack.Count == 0) return;

        if (!CanMove(undoStack.Peek())) return;

        List<Movement> movements = undoStack.Pop();

        redoStack.Push(movements);

        foreach (Movement movement in movements)
        {
            movement.Mover.MoveTo(movement.From);
        }
    }


    public void OnRedo()
    {
        if (redoStack.Count == 0) return;

        if (!CanMove(redoStack.Peek())) return;

        List<Movement> movements = redoStack.Pop();

        undoStack.Push(movements);

        foreach (Movement movement in movements)
        {
            movement.Mover.MoveTo(movement.To);
        }
    }

    private bool CanMove(List<Movement> movements)
    {
        foreach (Movement movement in movements)
        {
            if (movement.Mover.IsMoving)
                return false;
        }

        return true;
    }
}