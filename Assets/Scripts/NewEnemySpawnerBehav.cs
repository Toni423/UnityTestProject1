using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemySpawnerBehav : SpawnerBehav
{

    public GameObject zombie;
    public GameObject skeleton;


    // Start is called before the first frame update
    void Start()
    {
        spawnCooldownMin = 7f;
        spawnCooldownMax = 11f;
        itemMaxCount = 4;
        transform.SetPositionAndRotation(new Vector3(8.4f, 0f, 0f), new Quaternion(0f, 0f, 0f, 0f));
    }

    

    protected override GameObject getObjectToSpawn()
    {
        if(zombie == null)
        {
            Debug.Log("EnemySpawner: Zombie not set");
        }
        if (skeleton == null)
        {
            Debug.Log("EnemySpawner: Skeleton not set");
        }

        return Random.Range(0, 3) < 2 ? zombie : skeleton;
    }
}
