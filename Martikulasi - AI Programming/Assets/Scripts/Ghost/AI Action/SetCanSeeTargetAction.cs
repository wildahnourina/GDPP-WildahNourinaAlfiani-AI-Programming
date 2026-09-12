using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Set CanSeeTarget", story: "Set [CanSeeTarget] from [AI]", category: "Action", id: "65f2fec476ecb0ea7716c517a9d3b944")]
public partial class SetCanSeeTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<bool> CanSeeTarget;
    [SerializeReference] public BlackboardVariable<GhostAIController> AI;
    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (AI.Value == null && AI.Value.SightPerception == null)
        {
            return Status.Failure;
        }
        CanSeeTarget.Value = AI.Value.SightPerception.canSeePlayer;
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

