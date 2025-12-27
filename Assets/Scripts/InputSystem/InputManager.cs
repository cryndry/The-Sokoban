using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : LazySingleton<InputManager>
{
    private InputSystemActions inputSystemActions;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        inputSystemActions = new InputSystemActions();
    }

    private void OnEnable()
    {
        inputSystemActions.Player.Enable();
        inputSystemActions.Player.Move.performed += OnMoveInput;
        inputSystemActions.Player.Undo.performed += OnUndo;
        inputSystemActions.Player.Redo.performed += OnRedo;
    }

    private void OnDisable()
    {
        inputSystemActions.Player.Move.performed -= OnMoveInput;
        inputSystemActions.Player.Undo.performed -= OnUndo;
        inputSystemActions.Player.Redo.performed -= OnRedo;
        inputSystemActions.Player.Disable();
    }

    private void OnMoveInput(InputAction.CallbackContext context)
    {
        Vector2 rawInput = inputSystemActions.Player.Move.ReadValue<Vector2>();

        // "Deadzone" check
        if (rawInput.magnitude < 0.5f) return;

        Vector2Int direction;
        if (Mathf.Abs(rawInput.x) > Mathf.Abs(rawInput.y))
        {
            direction = rawInput.x > 0 ? Vector2Int.right : Vector2Int.left;
        }
        else
        {
            direction = rawInput.y > 0 ? Vector2Int.up : Vector2Int.down;
        }

        EventManager.Instance.InvokeOnMove(direction);
    }

    private void OnUndo(InputAction.CallbackContext context)
    {
        EventManager.Instance.InvokeOnUndo();
    }

    private void OnRedo(InputAction.CallbackContext context)
    {
        EventManager.Instance.InvokeOnRedo();
    }
}
