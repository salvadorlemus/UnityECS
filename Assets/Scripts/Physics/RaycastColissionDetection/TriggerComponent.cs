using Unity.Entities;

/// <summary>
/// Component that holds the sphere cast radius for the trigger
/// </summary>
public struct TriggerComponent : IComponentData
{
    public float SpherecastSize; 
}
