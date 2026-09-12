using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using UnityEngine.AI;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Wait until reach destination", story: "[AI] wait until reach destination", category: "Action", id: "2fd3a43dc1c828fcfa3cef831b9be2fa")]
public partial class WaitUntilReachDestinationAction : Action
{
    [SerializeReference] public BlackboardVariable<GhostAIController> AI;

    protected override Status OnStart()
    {
        return Status.Running;
    }
    protected override Status OnUpdate()
    {
        if (AI.Value == null)
        {
            return Status.Failure;
        }
        NavMeshAgent agent = AI.Value.NavMeshAgent;

        if (agent == null)
        {
            return Status.Failure;
        }

        if (agent.pathPending == true)
        {
            return Status.Running;
        }

        if (agent.remainingDistance > agent.stoppingDistance + 0.5)
        {
            return Status.Running;
        }
        return Status.Success;
    }
    protected override void OnEnd()
    {
    }
}

