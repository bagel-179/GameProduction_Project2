using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Sighted", story: "If the [enemy] is in line of [sight]", category: "Action", id: "eb8d81ff0fc09c79ac12fd60ca6d367e")]
public partial class SightedAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Enemy;
    [SerializeReference] public BlackboardVariable<FOV> Sight;
    protected override Status OnStart()
    {
        return Sight.Value.isSpotted == false ? Status.Success : Status.Failure;
    }
    
}

