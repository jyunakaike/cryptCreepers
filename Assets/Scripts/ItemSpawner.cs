using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject checkpointPrefab; 
    [SerializeField] int checkpointSpawnDelay = 10;
    [SerializeField] float spawnRadius = 6;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCheckpointRoutine());
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }

    IEnumerator SpawnCheckpointRoutine(){
        while (true)
        {
            yield return new WaitForSeconds(checkpointSpawnDelay);
            Vector2 randomPosition = Random.insideUnitCircle * spawnRadius;
            Instantiate(checkpointPrefab, randomPosition, Quaternion.identity);
        }
    }
}
