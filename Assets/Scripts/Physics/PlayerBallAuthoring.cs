using Unity.Entities;
using UnityEditor.Search;
using UnityEngine;

public class PlayerBallAuthoring : MonoBehaviour
{
    public float MoveSpeed = 10;

    /// <summary>
    /// Class used to bake the character into the ECS world.
    /// Remember that we can only create entities at runtime
    /// </summary>
    public class PlayerBallAuthoringBaker : Baker<PlayerBallAuthoring>
    {
        // Override the Bake method to create the entity and add the components
        public override void Bake(PlayerBallAuthoring authoring)
        {
            Entity ballAuthoring = GetEntity(TransformUsageFlags.None);

            // Adds a BallComponent component to the created entity.
            AddComponent(ballAuthoring, new PlayerBallComponent{MoveSpeed = authoring.MoveSpeed});
        }
    }
}
