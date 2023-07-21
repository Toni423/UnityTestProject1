using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnerBehav : MonoBehaviour
{
    private int direct = 1;
    public float moveSpeed = 1f;
    protected bool cooldown = false;
    public float spawnCooldownMin = 7f;
    public float spawnCooldownMax = 11f;
    public int count = 0;
    protected int itemMaxCount = 1;


    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y > 4.156 || gameObject.transform.position.y < -4.526)
        {
            direct *= -1;
        }

        // Calculate the movement vector for moving right
        Vector3 movement = new Vector3(0f, moveSpeed * Time.deltaTime * direct, 0f);

        // Update the object's position
        transform.Translate(movement);

        spawn();
    }

    protected virtual void spawn()
    {
        if (count < itemMaxCount && !cooldown)
        {
            cooldown = true;
            count++;

            GameObject tospawn = getObjectToSpawn();

            if (tospawn == null)
            {
                Debug.Log("Spawner: Spawnable not selected");
                return;
            }
            Instantiate(tospawn, transform.position, Quaternion.identity);

            Invoke(nameof(restoreCooldown), Random.Range(spawnCooldownMin, spawnCooldownMax));

        }
    }

    protected abstract GameObject getObjectToSpawn();

    protected void restoreCooldown()
    {
        cooldown = false;
    }


    protected object locker = new();

    
    public virtual void decreaseCount()
    {
        lock (locker)
        {
            count--;
        }
    }


}
