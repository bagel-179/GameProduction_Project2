using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "InFOV", story: "If the [Player] does not see the Enemy", category: "Action", id: "98dc616455769c287a7934264a37f63a")]
public partial class InFovAction : Action
{
    [SerializeReference] public BlackboardVariable<FOV> Player;
    protected override Status OnStart()
    {
        
        return Status.Running;
    }

    
    protected override Status OnUpdate()
    {
        Debug.Log($"InFovAction: isSpotted = {Player.Value.isSpotted}");
        
        Debug.Log($"Checking FOV Instance: {Player.Value.gameObject.name}");
        
        Debug.Log($"[InFovAction] Checking isSpotted = {Player.Value.isSpotted}");
        
        
        return Player.Value.isSpotted? Status.Failure : Status.Success;
    }

    protected override void OnEnd()
    {
    }
   
}

