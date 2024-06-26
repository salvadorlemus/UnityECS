using Unity.Entities;
using Unity.Mathematics;

public struct PlayerBallMovementComponent : IComponentData
{
    public float2 movement;
    public float2 mousePos;
    public bool pressingLMB;
}
