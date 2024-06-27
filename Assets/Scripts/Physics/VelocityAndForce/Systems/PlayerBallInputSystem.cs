using Unity.Burst;
using Unity.Entities;
using UnityEngine;

/// <summary>
/// Class used to reand and Write player ball input.
///
/// We can't reuse the CharacterInputSystem from the CharacterMovement example  because the output will be different.
/// Since Systems are active across the whole scene, we need to create a new one or the CharacterMovementSystem will
/// going to catch the player ball input if we reuse the CharacterInputSystem.
/// 
/// CharacterMovementSystem updates the Position of the character, while PlayerBallSystem updates the player
/// ball Velocity.
/// </summary>
[BurstCompile]
public partial class PlayerBallInputSystem : SystemBase
{
    private Controls _controls;

    /// <summary>
    /// On create is accessible due to the SystemBase inheritance.
    /// </summary>
    protected override void OnCreate()
    {
        _controls = new Controls();
        _controls.Enable();
    }

    [BurstCompile]
    protected override void OnUpdate()
    {
        // This part just reads the input, it does not apply any movement to the player ball.
        foreach (var playerBallMovementComponent in SystemAPI.Query<RefRW<PlayerBallMovementComponent>>())
        {
            playerBallMovementComponent.ValueRW.Movement = _controls.Character.Move.ReadValue<Vector2>();
        }
    }
}
