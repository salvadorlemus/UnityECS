using Unity.Burst;
using Unity.Entities;
using UnityEngine;

/// <summary>
/// System used to update the input of the character Input System
/// </summary>
[BurstCompile]
public partial class CharacterInputSystem : SystemBase
{
    // Reference to the Controls in the Input System
    private Controls _controls;

    protected override void OnCreate()
    {
        _controls = new Controls();
        _controls.Enable();
    }

    [BurstCompile]
    protected override void OnUpdate()
    {
        // Query all the InputComponent component in scene to update the Movement value;
        // Right now this component is yielded by the player entity.
        foreach (var inputComponent in SystemAPI.Query<RefRW<InputComponent>>())
        {
            // Read the value of the Movement input from the controls as RW (Read / Write) so we can update it. 
            // This does not apply any movement to the character, it just updates the input.
            inputComponent.ValueRW.Movement = _controls.Character.Move.ReadValue<Vector2>();
        }
    }
}
