using Unity.Entities;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Class used to attach components to the trigger entity
/// </summary>
public class TriggerAuthoring : MonoBehaviour
{
   [FormerlySerializedAs("SpherecastRadius")] public float SpherecastSize;

   /// <summary>
   /// Class used to bake the character behaviour into the ECS world.
   /// Remember that we can only create entities at runtime
   /// </summary>
   public class TriggerBaker : Baker<TriggerAuthoring>
   {
      // Override the Bake method to create the entity and add the components
      public override void Bake(TriggerAuthoring authoring)
      {
         // Returns the primary Entity
         Entity triggerAuthoring = GetEntity(TransformUsageFlags.None);

         // Adds a TriggerComponent component to the created entity.
         AddComponent(triggerAuthoring, new TriggerComponent{SpherecastSize = authoring.SpherecastSize});
      }
   }
}
