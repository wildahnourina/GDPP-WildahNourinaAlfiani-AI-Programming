using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerInputSystem;

public class InputManager : MonoBehaviour, IPlayerActions
{
    private PlayerInputSystem input;
    private void Awake()
    {
        input = new PlayerInputSystem();

        input.Enable();
        input.Player.Enable();

        input.Player.SetCallbacks(this);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Interact");
        }
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<Vector2>());
    }

}
