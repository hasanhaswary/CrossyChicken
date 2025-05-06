using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldSpawner : MonoBehaviour
{
    [Header("Row Ground Prefabs")]
    [SerializeField] private GameObject grassRowPrefab;
    [SerializeField] private GameObject roadRowPrefab;
    [SerializeField] private GameObject riverRowPrefab;
    [SerializeField] private GameObject railwayRowPrefab;

    [Header("Car Prefabs (Facing)")]
    [SerializeField] private GameObject[] carsFacingRight;
    [SerializeField] private GameObject[] carsFacingLeft;

    [Header("Log Prefabs (Facing)")]
    [SerializeField] private GameObject[] logsFacingRight;
    [SerializeField] private GameObject[] logsFacingLeft;

    [Header("Train Prefabs (Facing)")]
    [SerializeField] private GameObject[] trainsFacingRight;
    [SerializeField] private GameObject[] trainsFacingLeft;

    //[Header("Spawn Settings")]
    //[Tooltip("Half-width of each row in world units (e.g. 10 for a 20-unit wide row)")]
    //[SerializeField] private float rowHalfWidth = 10f;

    [Tooltip("How far off the row edge obstacles first appear")]
    [SerializeField] private float carSpawnOffsetX = 1f;
    [SerializeField] private float logSpawnOffsetX = 1f;
    [SerializeField] private float trainSpawnOffsetX = 1f;

    [Header("Continuous Spawn Intervals (secs)")]
    [SerializeField] private float carSpawnInterval = 2f;
    [SerializeField] private float logSpawnInterval = 3f;
    [SerializeField] private float trainSpawnInterval = 5f;

    [Header("General Settings")]
    [SerializeField] private int initialRows = 10;
    [SerializeField] private float rowSpacing = 1f;
    [SerializeField] private Transform playerTransform;

    private float nextSpawnZ = 0f;
    private bool lastDirectionRight = false;
    private Queue<GameObject> spawnedRows = new Queue<GameObject>();

    private void Start()
    {
        for (int i = 0; i < initialRows; i++)
            SpawnNextRow();
    }

    private void Update()
    {
        if (nextSpawnZ < playerTransform.position.z + initialRows * rowSpacing)
            SpawnNextRow();
    }

    private void SpawnNextRow()
    {
        int type = Random.Range(0, 4); // 0=Grass,1=Road,2=River,3=Railway
        GameObject rowPrefab = type switch
        {
            1 => roadRowPrefab,
            2 => riverRowPrefab,
            3 => railwayRowPrefab,
            _ => grassRowPrefab
        };

        Vector3 rowPos = new Vector3(0, 0, nextSpawnZ);
        GameObject rowGO = Instantiate(rowPrefab, rowPos, Quaternion.identity, transform);
        spawnedRows.Enqueue(rowGO);

        // start continuous spawning for this row
        StartCoroutine(ContinuousSpawn(rowGO, type, lastDirectionRight));
        lastDirectionRight = !lastDirectionRight;

        nextSpawnZ += rowSpacing;

        if (spawnedRows.Count > initialRows + 2)
            Destroy(spawnedRows.Dequeue());
    }

    private IEnumerator ContinuousSpawn(GameObject parentRow, int type, bool directionRight)
    {
        Vector3 dir = directionRight ? Vector3.right : Vector3.left;
        float offsetX = (type == 1 ? carSpawnOffsetX : type == 2 ? logSpawnOffsetX : trainSpawnOffsetX) * (directionRight ? -1 : 1);
        float interval = (type == 1 ? carSpawnInterval : type == 2 ? logSpawnInterval : trainSpawnInterval);
        GameObject[] pool = type switch
        {
            1 => (directionRight ? carsFacingRight : carsFacingLeft),
            2 => (directionRight ? logsFacingRight : logsFacingLeft),
            3 => (directionRight ? trainsFacingRight : trainsFacingLeft),
            _ => null
        };

        if (pool == null)
            yield break;

        while (parentRow != null)
        {
            // spawn a single obstacle each interval
            var prefab = pool[Random.Range(0, pool.Length)];
            Vector3 spawnPos = new Vector3(offsetX, 0.5f, parentRow.transform.position.z);
            var obj = Instantiate(prefab, spawnPos, Quaternion.identity, parentRow.transform);

            // set movement direction
            if (type == 1) obj.GetComponent<CarMovement>().moveDirection = dir;
            if (type == 2) obj.GetComponent<LogMovement>().moveDirection = dir;
            if (type == 3) obj.GetComponent<TrainMovement>().moveDirection = dir;

            yield return new WaitForSeconds(interval);
        }
    }
}
