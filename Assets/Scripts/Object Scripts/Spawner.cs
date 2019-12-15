using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    #region Inspector Fields
    public float spawnHeightPickUp = 6f;
    public float spawnHeightObstacle = 4f;
    public List<PickUp> pickUpsToSpawn = new List<PickUp>();
    public List<Obstacle> obstaclesToSpawn = new List<Obstacle>();
    [Range(0, 100)]
    public float spawnChance = 10;
    [Range(0, 60)]
    public float secondsUntilNextSpawnTry = 30f;
    #endregion

    private float nextSpawnTime = 0;


    private void Start()
    {
   
    }

    private void Update()
    {

        if (Time.timeSinceLevelLoad > 4)

        if (Time.timeSinceLevelLoad > nextSpawnTime)
        {
            int decider = Random.Range(0, 1);

            if (decider == 1)
            {
                SpawnPickUp();

            }
            else
            {
                SpawnObstacle();
            }

            nextSpawnTime = Time.timeSinceLevelLoad + secondsUntilNextSpawnTry;
        }
    }

    public void SpawnPickUp()
    {
        if (spawnChance < Random.Range(0, 100))
        {
            int indexChosen = Random.Range(0, pickUpsToSpawn.Count - 1);
            if (pickUpsToSpawn[indexChosen] == null)
            {
                return;
            }

            GameObject pickUp = Instantiate(pickUpsToSpawn[indexChosen].gameObject, transform);
            pickUp.transform.position = transform.position + (Vector3.up * spawnHeightPickUp);
        }
    }

    public void SpawnObstacle()
    {
        if (spawnChance < Random.Range(0, 100))
        {
            int indexChosen = Random.Range(0, obstaclesToSpawn.Count - 1);
            if (obstaclesToSpawn[indexChosen] == null)
            {
                return;
            }

            GameObject obstacle = Instantiate(obstaclesToSpawn[indexChosen].gameObject, transform);
            obstacle.transform.position = transform.position + (Vector3.up * spawnHeightObstacle);
        }
    }
}