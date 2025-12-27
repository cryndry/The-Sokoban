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
        Vector2 movement = inputSystemActions.Player.Move.ReadValue<Vector2>();
        EventManager.Instance.InvokeOnMove(movement);
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
