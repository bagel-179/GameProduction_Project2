using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsSpotted", story: "If Player [sees] [Enemy]", category: "Conditions", id: "8b5f00c9de1a314191640062bd5604b7")]
public partial class IsSpottedCondition : Condition
{
    [SerializeReference] public BlackboardVariable<DetectionScript> Sees;
    [SerializeReference] public BlackboardVariable<GameObject> Enemy;

    public override bool IsTrue()
    {
        Sees.Value.isSpotted = true;
        return true;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
