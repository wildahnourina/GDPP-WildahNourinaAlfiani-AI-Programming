using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Set LastSeenPosition", story: "Set [LastSeenPosition] from [AI]", category: "Action", id: "22c5803d724ef60497780947f9d75c7f")]
public partial class SetLastSeenPositionAction : Action
{
    [SerializeReference] public BlackboardVariable<Vector3> LastSeenPosition;
    [SerializeReference] public BlackboardVariable<GhostAIController> AI;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (AI.Value == null && AI.Value.Target == null)
        {
            return Status.Failure;
        }
        LastSeenPosition.Value = AI.Value.SightPerception.lastSeenPosition;
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

