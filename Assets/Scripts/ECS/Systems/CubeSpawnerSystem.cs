using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;


/// <summary>
/// Sinsce this a system we need to implement a ISystem interface.
/// Also, since this is not a Unity component, we don't need to inherit from MonoBehaviour and instead of a class
/// we use a pertial struct
/// </summary>
[BurstCompile]
public partial struct CubeSpawnerSystem : ISystem
{
    // Normal update method in ECS
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        // If we don't have the CubeSpawnerComponent in the scene, we don't need to do anything
        // Remember, we can only create entities at runtime and since CubeSpawnerComponent is a component we can't just
        // drag and drop it in the scene. We need to use the CubeSpawningAuthoring class to create the entity at runtime
        // Usong the CubeSpawnerBaker class.
        if (!SystemAPI.TryGetSingletonEntity<CubeSpawnerComponent>(out Entity spawnerEntity))
        {
            return;
        }

        // Gets the CubeSpawnerComponent from the entity. Since we need to read and write data from the component
        // We use the SystemAPI.GetComponentRW method and RefRW to get a reference to the component.
        RefRW<CubeSpawnerComponent> spawner = SystemAPI.GetComponentRW<CubeSpawnerComponent>(spawnerEntity);

        // We use an entity command buffer to create the entities at runtime. This command buffer will be executed
        // using burst compatible tech
        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);

        if (spawner.ValueRO.nextSpawnTime < SystemAPI.Time.ElapsedTime)
        {
            // Since we need to READ the data from the component, we use the SystemAPI.GetComponentRO method
            Entity newEntity = ecb.Instantiate(spawner.ValueRO.prefab);

            // Tells the command buffer to add the CubeMovementComponent to the new entity
            // This component will be used to move the cube in the CubeMovementSystem
            ecb.AddComponent(newEntity, new CubeMovementComponent
            {
                // This is how we get a random in ECS
                moveDiraction = Random.CreateFromIndex((uint)(SystemAPI.Time.ElapsedTime / SystemAPI.Time.DeltaTime)).NextFloat3(),
                moveSpeed = 10.0f
            });

            // Now, we need to set the next spawn time, so this time we need to WRITE to the component
            spawner.ValueRW.nextSpawnTime = (float) SystemAPI.Time.ElapsedTime + spawner.ValueRO.spawnRate;

            // Executes all comands in the command buffer
            ecb.Playback(state.EntityManager);
        }
    }
}
