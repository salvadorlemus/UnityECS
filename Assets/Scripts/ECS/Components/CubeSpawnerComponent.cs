using Unity.Entities;
using Unity.Mathematics;

/// <summary>
/// Since we are using ECS, we need to create a component to attach to the entity that will spawn the cubes.
/// This component will be used to identify the entity that will spawn the cubes.
/// This is not a class but a struct because it is a component and we are using ECS.
/// </summary>
public struct CubeSpawnerComponent : IComponentData
{
    public Entity prefab;
    public float3 spawnPosition;
    public float nextSpawnTime;
    public float spawnRate;
}
