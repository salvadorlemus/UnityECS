using Unity.Entities;
using Unity.Mathematics;

/// <summary>
/// Component that hold the cube movement data
/// </summary>
public struct CubeMovementComponent : IComponentData
{
    public float3 moveDiraction;
    public float moveSpeed;
}
