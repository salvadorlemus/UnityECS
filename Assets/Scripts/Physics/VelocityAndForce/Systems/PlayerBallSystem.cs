using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;

[BurstCompile]
public partial struct PlayerBallSystem : ISystem
{
    PlayerBallMovementComponent _playerBallMovementComponent;

    [BurstCompile]
    private void OnUpdate(ref SystemState state)
    {
        EntityManager entityManager = state.EntityManager;

        // Ask for the inputComponent
        if (!SystemAPI.TryGetSingleton(out _playerBallMovementComponent))
            return;

        // NativeArray of entities in scene
        NativeArray<Entity> entities = entityManager.GetAllEntities(Allocator.Temp);

        // Filter entities by components
        foreach (Entity entity in entities)
        {
            if (entityManager.HasComponent<PlayerBallComponent>(entity))
            {
                // Get the PlayerBallComponent from the entity
                PlayerBallComponent playerBallComponent = entityManager.GetComponentData<PlayerBallComponent>(entity);

                // Get the PhysicsVelocity component from the entity 
                RefRW<PhysicsVelocity> physicsVelocity = SystemAPI.GetComponentRW<PhysicsVelocity>(entity);

                // Apply the movement to the physics velocity
                physicsVelocity.ValueRW.Linear += new float3(_playerBallMovementComponent.Movement.x * playerBallComponent.MoveSpeed * SystemAPI.Time.DeltaTime, 0, _playerBallMovementComponent.Movement.y * playerBallComponent.MoveSpeed * SystemAPI.Time.DeltaTime);
            }
        }

        // dispose the NativeArray
        entities.Dispose();
    }
}
