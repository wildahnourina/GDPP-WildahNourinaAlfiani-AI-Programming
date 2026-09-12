using System.Collections;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class GhostAIController : MonoBehaviour
{
    [SerializeField] private PlayerCharacter target;

    private BehaviorGraphAgent behaviorGraphAgent;
    private NavMeshAgent navMeshAgent;
    private SightPerception sightPerception;

    public UnityEvent OnDespawn;
    public BehaviorGraphAgent BehaviorGraphAgent => behaviorGraphAgent;
    public NavMeshAgent NavMeshAgent => navMeshAgent;
    public PlayerCharacter Target => target;
    public SightPerception SightPerception => sightPerception;

    private void Awake()
    {
        behaviorGraphAgent = GetComponent<BehaviorGraphAgent>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        sightPerception = GetComponent<SightPerception>();
    }

    public void Despawn()
    {
        StartCoroutine(DespawnAfterEndOfFrame());
    }

    private IEnumerator DespawnAfterEndOfFrame()
    {
        if (behaviorGraphAgent != null)
        {
            // melakukan reset variable blackboard CanSeeTarget
            // dari blackboard behavior graph BT_ChasingGhost
            // supaya ketika respawn, status target kembali
            // menjadi tidak terlihat
            behaviorGraphAgent.SetVariableValue("CanSeeTarget", false);
            // menonaktifkan behavior graph, 
            // supaya alur perilaku AI berhenti ketika
            // AI nonaktif
            behaviorGraphAgent.enabled = false;

            if (navMeshAgent != null && navMeshAgent.isOnNavMesh == true)
            {
                // Mengosongkan jalur AI, untuk AI tidak bergerak
                // ketika akan di-nonaktifkan
                navMeshAgent.ResetPath();
                // Menonaktifkan navmesh agent, supaya AI tidak
                // bergerak ketika AI nonaktif
                navMeshAgent.enabled = false;
            }

            // Memanggil event OnDespawn untuk module lain
            // jika ingin melakukan sesuatu ketika AI di-despawn
            OnDespawn?.Invoke();
            // Menunggu sampai akhir frame, 
            // memastikan node action di dalam behavior graph
            // telah mengembalikan status success/failed
            yield return new WaitForEndOfFrame();
            gameObject.SetActive(false);
        }
    }
}
