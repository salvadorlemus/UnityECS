using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

[BurstCompile]
public partial struct TriggerSystem : ISystem
{
    [BurstCompile]
    private void OnUpdate(ref SystemState state)
    {
        EntityManager entityManager = state.EntityManager;

        NativeArray<Entity> entities = entityManager.GetAllEntities(Allocator.Temp);

        foreach (Entity entity in entities)
        {
            // Ask the entity manager if the entity has the TriggerComponent, so we change its size
            if (entityManager.HasComponent<TriggerComponent>(entity))
            {
                // Get the Entity Transform
                RefRW<LocalToWorld> triggerTransform = SystemAPI.GetComponentRW<LocalToWorld>(entity);
                // Get the Entity TriggerComponent
                RefRO<TriggerComponent> triggerComponent = SystemAPI.GetComponentRO<TriggerComponent>(entity);

                // Gets the Sphere Radious we set in the TriggerComponent at runtime
                float size = triggerComponent.ValueRO.SpherecastSize;

                // Changes the Scale of the Sphere alongside with the Sphere collision Size
                triggerTransform.ValueRW.Value.c0 = new float4(size, 0, 0, 0);
                triggerTransform.ValueRW.Value.c1 = new float4(0, size, 0, 0);
                triggerTransform.ValueRW.Value.c2 = new float4(0, 0, size, 0);

                // To cast any raycast we need a reference to the PhysicsWorld Singleton
                PhysicsWorldSingleton physicsWorld = SystemAPI.GetSingleton<PhysicsWorldSingleton>();

                // List to hold all the collisions from the sphere cast
                NativeList<ColliderCastHit> hits = new NativeList<ColliderCastHit>(Allocator.Temp);

                // Cast an Sphere cast from the trigger position
                physicsWorld.SphereCastAll(triggerTransform.ValueRO.Position, triggerComponent.ValueRO.SpherecastSize / 2,float3.zero, 1, ref hits, CollisionFilter.Default);

                // Here we can filter collision based on components
                foreach (ColliderCastHit hit in hits)
                {
                    if (!entityManager.HasComponent<TriggerComponent>(hit.Entity))
                    {
                        entityManager.DestroyEntity(hit.Entity);
                    }
                }
            }
        }

        entities.Dispose();
    }
}
