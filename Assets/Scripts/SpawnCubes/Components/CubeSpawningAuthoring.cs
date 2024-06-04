using Unity.Entities;
using UnityEngine;

/// <summary>
/// It's not a System nor a ComponentData, it's just a class to identify the GameObject that will spawn the cubes.
/// Remember that we can only create entities at runtime.
/// </summary>
public class CubeSpawningAuthoring : MonoBehaviour
{
    // Prefab to spawn; this will be passed to my CubeSpawnerComponent.
    public GameObject prefab;
    public float spawenRate;

}

/// <summary>
/// A baker creates the entity and ads the components to it.
/// Remember that we can only create entities at runtime.
/// </summary>
class CubeSpawnerBaker : Baker<CubeSpawningAuthoring>
{
    public override void Bake(CubeSpawningAuthoring authoring)
    {
        // Gets the entity at runtime
        Entity entity = GetEntity(TransformUsageFlags.None);

        // Adds the Entity component to this new entity and set the component data to it.
        AddComponent(entity, new CubeSpawnerComponent
        {
            prefab = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic),
            spawnPosition = authoring.transform.position,
            nextSpawnTime = 0.0f,
            spawnRate = authoring.spawenRate
        });
    }
}
