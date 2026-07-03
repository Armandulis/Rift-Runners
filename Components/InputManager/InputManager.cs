using Godot;
using System;

public partial class InputManager : Node2D
{
    // Define delegates for different input types
    public delegate void SpellInputEvent(string actionName);
    public event SpellInputEvent OnSpellInput;

    public delegate void MovementInputEvent(Vector2 direction);
    public event MovementInputEvent OnMovementInput;

    public delegate void ActionInputEvent(string actionName);
    public event ActionInputEvent OnActionInput;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        // Handle spell input
        // if (Input.IsActionJustPressed("Keybind_4"))
        // {
        // 	GD.Print("hello everybody");
        //     // OnSpellInput?.Invoke("cast_fireball");
        // }
        // else if (Input.IsActionJustPressed("cast_icebolt"))
        // {
        // OnSpellInput?.Invoke("cast_icebolt");
        // }

        // Handle movement input
        // Vector2 movementDirection = Vector2.Zero;
        // if (Input.IsActionPressed("move_up"))
        //     movementDirection.y -= 1;
        // if (Input.IsActionPressed("move_down"))
        //     movementDirection.y += 1;
        // if (Input.IsActionPressed("move_left"))
        //     movementDirection.x -= 1;
        // if (Input.IsActionPressed("move_right"))
        //     movementDirection.x += 1;

        // if (movementDirection != Vector2.Zero)
        // {
        //     OnMovementInput?.Invoke(movementDirection.Normalized());
        // }

        // Handle other types of input (e.g., actions)
        // if (Input.IsActionJustPressed("jump"))
        // {
        //     OnActionInput?.Invoke("jump");
        // }
    }
}
