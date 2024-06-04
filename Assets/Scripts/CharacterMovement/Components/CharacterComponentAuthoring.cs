using Unity.Entities;

/// <summary>
/// Component used to hold the character speed data.
/// </summary>
public struct CharacterComponentAuthoring : IComponentData
{
    public float characterSpeed;
}

/// <summary>
/// Class used to bake the character into the ECS world.
/// Remember that we can only create entities at runtime
/// </summary>
public class CharacterBaker : Baker<Character>
{
    // Overrride the Bake method to create the entity and add the components
    public override void Bake(Character authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        
        // Adds a CharacterComponent`component to the created entity.
        AddComponent(entity, new CharacterComponentAuthoring
        {
            characterSpeed = authoring.characterSpeed
        });

        // Adds an InputComponent component to the created entity.
        AddComponent(entity, new InputComponent());
    }
}