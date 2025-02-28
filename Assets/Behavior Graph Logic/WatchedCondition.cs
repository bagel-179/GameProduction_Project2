using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Watched", story: "If [self] is [watching] the [target]", category: "Conditions", id: "ccd726f62ca3cebf945351401b996bc0")]
public partial class WatchedCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<bool> Watching;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    public override bool IsTrue()
    {
        
        return true;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
