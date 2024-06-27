using Unity.Entities;
using Unity.Mathematics;

public struct PlayerBallMovementComponent : IComponentData
{
    public float2 Movement;
    public float2 MousePos;
    public bool PressingLMB;
}
