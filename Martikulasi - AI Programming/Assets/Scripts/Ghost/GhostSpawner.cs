using System.Collections;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    [SerializeField] private GhostAIController aiController;
    [SerializeField] private float minSpawnDelay = 5f;
    [SerializeField] private float maxSpawnDelay = 8f;
    [SerializeField] private float minSpawnDistance = 3f;
    [SerializeField] private float maxSpawnDistance = 5f;

    private Coroutine spawnCo;

    public void RestartSpawn()
    {
        if (spawnCo != null)
            StopCoroutine(spawnCo);

        spawnCo = StartCoroutine(StartSpawn());
    }

    public IEnumerator StartSpawn()
    {
        float spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);

        yield return new WaitForSeconds(spawnDelay);

        if (aiController.Target == null || aiController.Target.IsHiding == true)
        {
            RestartSpawn();
            yield break;
        }

        SpawnGhost();
    }

    public void SpawnGhost()
    {
        float spawnDistance = Random.Range(minSpawnDistance, maxSpawnDistance);
        Vector3 spawnPos = aiController.Target.transform.position - aiController.Target.transform.forward * spawnDistance;
        spawnPos.y = aiController.transform.position.y;

        // Mengaktifkan kembali NavMeshAgent
        aiController.NavMeshAgent.enabled = true;
        // Memindahkan posisi ghost AI ke spawn pos yang sudah dihitung
        // Function warp akan memastikan AI berada tetap di area nav mesh
        aiController.NavMeshAgent.Warp(spawnPos);
        // Memutar AI untuk menghadap ke arah target (player)
        aiController.transform.LookAt(aiController.Target.transform);

        aiController.gameObject.SetActive(true);

        // Reset variable blackboard last seen position
        // ke posisi target (player)
        aiController.BehaviorGraphAgent.SetVariableValue("LastSeenPosition", aiController.Target.transform.position);
        // Mengaktifkan kembali behavior graph agent
        aiController.BehaviorGraphAgent.enabled = true;
    }
}
