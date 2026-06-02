using System.Collections;
using UnityEngine;

public class ControladorDianas : MonoBehaviour
{
    [Header("Spawn Points")]
    public Transform leftSpawnPoint;
    public Transform rightSpawnPoint;

    [Header("Prefab")]
    public GameObject targetPrefab;

    [Header("Timers")]
    public float targetLifetime = 3f;
    public float respawnDelay = 0.5f;

    private GameObject currentTarget;

    public void StartTargets()
    {
        StopAllCoroutines();

        if (currentTarget != null)
        {
            Destroy(currentTarget);
        }

        SpawnNewTarget();
    }

    public void StopTargets()
    {
        StopAllCoroutines();

        if (currentTarget != null)
        {
            Destroy(currentTarget);
        }
    }

    void SpawnNewTarget()
    {
        Transform selectedSpawnPoint;

        if (Random.Range(0, 2) == 0)
        {
            selectedSpawnPoint = leftSpawnPoint;
        }
        else
        {
            selectedSpawnPoint = rightSpawnPoint;
        }

        currentTarget = Instantiate(
            targetPrefab,
            selectedSpawnPoint.position,
            selectedSpawnPoint.rotation
        );

        StartCoroutine(TargetTimer());
    }

    IEnumerator TargetTimer()
    {
        yield return new WaitForSeconds(targetLifetime);

        if (currentTarget != null)
        {
            Destroy(currentTarget);
        }

        yield return new WaitForSeconds(respawnDelay);

        SpawnNewTarget();
    }

    public void TargetHit()
    {
        StopAllCoroutines();

        if (currentTarget != null)
        {
            Destroy(currentTarget);
        }

        StartCoroutine(RespawnTarget());
    }

    IEnumerator RespawnTarget()
    {
        yield return new WaitForSeconds(respawnDelay);

        SpawnNewTarget();
    }
}