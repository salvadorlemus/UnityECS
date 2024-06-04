using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;


/// <summary>
/// Since this is a system we need to implement a ISystem interface and instead of a class we use a partial struct.
/// </summary>
[BurstCompile]
public partial struct CuveMovementSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        EntityManager entityManager = state.EntityManager;

        // Get all entities in the scene
        NativeArray<Entity> entities = entityManager.GetAllEntities(Allocator.Temp);

        // Filtrate entities based on their components
        foreach (var entity in entities)
        {
            // Ask the entity manager if this particular entity has the component CubeMovementComponent
            if (entityManager.HasComponent<CubeMovementComponent>(entity))
            {
                // Gets the component data from the entity
                CubeMovementComponent cubeMovementComponent = entityManager.GetComponentData<CubeMovementComponent>(entity);

                // Gets the local transform from the entity
                LocalTransform cubeLocalTransform = entityManager.GetComponentData<LocalTransform>(entity);

                // Calculates the cube movement direction and animate them using the deltatime 
                float3 cubeMoveDirection = cubeMovementComponent.moveDiraction * SystemAPI.Time.DeltaTime * cubeMovementComponent.moveSpeed;
                
                // Set the new transform position to the entity
                cubeLocalTransform.Position = cubeLocalTransform.Position + cubeMoveDirection;

                // Tells the entity manager to set the new component data to the entity
                entityManager.SetComponentData<LocalTransform>(entity, cubeLocalTransform);

                // Updates the moving speed to slow down
                if(cubeMovementComponent.moveSpeed > 0.0f)
                {
                    cubeMovementComponent.moveSpeed -= 1f * SystemAPI.Time.DeltaTime;
                }
                else
                {
                    cubeMovementComponent.moveSpeed = 0.0f;
                }

                // Tells the entity manager to set the new component data to the entity
                entityManager.SetComponentData(entity, cubeMovementComponent);
            }
        }
    }
}
