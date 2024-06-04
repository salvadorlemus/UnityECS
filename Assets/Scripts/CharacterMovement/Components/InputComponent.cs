using Unity.Entities;
using Unity.Mathematics;

/// <summary>
/// Component used to hold the input data for the input system.
/// </summary>
public struct InputComponent : IComponentData
{
    public float2 Movement;
}
