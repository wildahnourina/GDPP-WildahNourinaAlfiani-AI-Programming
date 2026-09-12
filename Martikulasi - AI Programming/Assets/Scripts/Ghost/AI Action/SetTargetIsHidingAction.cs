using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Set TargetIsHiding", story: "Set [TargetIsHiding] from [AI]", category: "Action", id: "92705a6f83583043da76c170757e56d3")]
public partial class SetTargetIsHidingAction : Action
{
    [SerializeReference] public BlackboardVariable<bool> TargetIsHiding;
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
        TargetIsHiding.Value = AI.Value.Target.IsHiding;
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

