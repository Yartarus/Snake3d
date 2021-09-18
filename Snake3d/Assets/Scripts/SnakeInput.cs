using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SnakeController))]
public class SnakeInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private InputAction movement;

    public bool directionPressed = false;

    SnakeController controller;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();

        controller = GetComponent<SnakeController>();
    }

    private void OnEnable()
    {
        movement = playerInputActions.Player.Movement;
        movement.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
    }

    private void Update()
    {
        if (movement.ReadValue<Vector2>().Equals(Vector2.zero))
        {
            directionPressed = false;
            return;
        }
        if (movement.ReadValue<Vector2>().Equals(Vector2.left) && directionPressed == false)
        {
            controller.RotateLeft();
            directionPressed = true;
            return;
        }
        if (movement.ReadValue<Vector2>().Equals(Vector2.right) && directionPressed == false)
        {
            controller.RotateRight();
            directionPressed = true;
            return;
        }
        if (movement.ReadValue<Vector2>().Equals(Vector2.up) && directionPressed == false)
        {
            controller.RotateUp();
            directionPressed = true;
            return;
        }
        if (movement.ReadValue<Vector2>().Equals(Vector2.down) && directionPressed == false)
        {
            controller.RotateDown();
            directionPressed = true;
            return;
        }
    }
}
