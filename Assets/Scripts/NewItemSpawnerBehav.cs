using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewItemSpawnerBehav : SpawnerBehav
{
    public GameObject heart;
    private GameObject square;

    // Start is called before the first frame update
    void Start()
    {
        cooldown = true;
        spawnCooldownMin = 20f;
        spawnCooldownMax = 25f;
        itemMaxCount = 1;

        transform.SetPositionAndRotation(new Vector3(-8.4f, 0, 0), new Quaternion(0f, 0f, 0f, 0f));
        square = GameObject.FindGameObjectWithTag("Player");

        Invoke(nameof(restoreCooldown), Random.Range(spawnCooldownMin, spawnCooldownMax));
    }

    protected override GameObject getObjectToSpawn()
    {
        if(heart == null)
        {
            Debug.Log("ItemSpawner: Heart not selected");
            return null;
        }
        return heart;
    }

    public override void decreaseCount()
    {
        lock (locker)
        {
            count--;
            Invoke(nameof(restoreCooldown), Random.Range(spawnCooldownMin, spawnCooldownMax));
        }
    }


    protected override void spawn()
    {
        if (count < itemMaxCount && !cooldown && Mathf.Abs(square.transform.position.y - gameObject.transform.position.y) > 2f)
        {
            cooldown = true;
            count++;

            GameObject tospawn = getObjectToSpawn();

            if (tospawn == null)
            {
                Debug.Log("ItemSpawner: Item not selected");
                return;
            }
            Instantiate(tospawn, transform.position, Quaternion.identity);

            Invoke(nameof(restoreCooldown), Random.Range(spawnCooldownMin, spawnCooldownMax));

        }
    }

}
