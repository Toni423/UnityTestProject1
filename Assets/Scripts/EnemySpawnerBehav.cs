using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerBehav : MonoBehaviour
{
    private int direct = 1;
    public float moveSpeed = 1f;
    public GameObject zombie;
    public GameObject skeleton;
    private bool cooldown = false;
    public float spawnCooldownMin = 7f;
    public float spawnCooldownMax = 11f;
    public int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.SetPositionAndRotation(new Vector3(8.4f, 0f, 0f), new Quaternion(0f, 0f, 0f, 0f));

    }

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

        
        if(count < 4 && !cooldown)
        {
            cooldown = true;
            count++;

            spawn(Random.Range(0, 3) < 2 ? zombie : skeleton);
            
        }
    }

    

    private void restoreCooldown()
    {
        cooldown = false;
    }

    private void spawn(GameObject enemy)
    {
        if(enemy == null)
        {
            Debug.Log("EnemySpawner: Enemy not selected");
            return;
        }
        Instantiate(enemy, transform.position, Quaternion.identity);

        Invoke(nameof(restoreCooldown), Random.Range(spawnCooldownMin, spawnCooldownMax));
    }


    private object locker = new();
    public void enemyDied()
    {
        lock (locker)
        {
            count--;
        }
    }
}
