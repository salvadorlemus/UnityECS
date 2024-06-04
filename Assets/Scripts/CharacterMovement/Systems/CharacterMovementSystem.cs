using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

/// <summary>
/// System in charge of update the player position on screen.
/// </summary>
[BurstCompile]
public partial struct CharacterMovementSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        // Queries the character component (Read Only), the input component (Read Only) and the
        // local transform component (Read / Write) so the entity that has those 3 components can be updated.
        foreach (var (data, inputs, transform) in SystemAPI.Query<RefRO<CharacterComponentAuthoring>, RefRO<InputComponent>, RefRW<LocalTransform>>())
        {
            // Gets entity position
            float3 position = transform.ValueRO.Position;

            // Updates position based on input
            position.x += inputs.ValueRO.Movement.x * data.ValueRO.characterSpeed * SystemAPI.Time.DeltaTime;
            position.z += inputs.ValueRO.Movement.y * data.ValueRO.characterSpeed * SystemAPI.Time.DeltaTime;

            // Sets the updated position into the character
            transform.ValueRW.Position = position;
        }
    }
}
