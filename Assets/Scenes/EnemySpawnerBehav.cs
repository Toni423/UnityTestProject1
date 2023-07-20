using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerBehav : MonoBehaviour
{
    private int direct = 1;
    public float moveSpeed = 1f;
    public GameObject zombie;
    private bool cooldown = false;
    public float spawnCooldown = 10f;

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

        
        if(!cooldown)
        {
            cooldown = true;
            spawnZombie();
        }
    }

    

    private void restoreCooldown()
    {
        cooldown = false;
    }

    private void spawnZombie()
    {
        if(zombie == null)
        {
            Debug.Log("EnemySpawner: GameObject 'Zombie' not selected");
            return;
        }
        Instantiate(zombie, transform.position, Quaternion.identity);

        Invoke(nameof(restoreCooldown), spawnCooldown);
    }
}
