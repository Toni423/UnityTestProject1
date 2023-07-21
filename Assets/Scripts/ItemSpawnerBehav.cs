using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnerBehav : MonoBehaviour
{
    private int direct = 1;
    public float moveSpeed = 1f;
    private bool cooldown = true;
    public float spawnCooldownMin = 20f;
    public float spawnCooldownMax = 25f;
    public int count = 0;
    public GameObject heart;
    private GameObject square;
    private const int itemMaxCount = 1;

    // Start is called before the first frame update
    void Start()
    {
        transform.SetPositionAndRotation(new Vector3(-8.4f, 0, 0), new Quaternion(0f, 0f, 0f, 0f));
        square = GameObject.FindGameObjectWithTag("Player");

        Invoke(nameof(restoreCooldown), Random.Range(spawnCooldownMin, spawnCooldownMax));
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



        
        if (!cooldown && count < itemMaxCount && Mathf.Abs( square.transform.position.y - gameObject.transform.position.y) > 2f )
        {
            cooldown = true;
            count++;

            spawn(heart);

        }

    }

    private void restoreCooldown()
    {
        cooldown = false;
    }

    private void spawn(GameObject item)
    {
        if (item == null)
        {
            Debug.Log("ItemSpawner: Item not selected");
            return;
        }
        Instantiate(item, transform.position, Quaternion.identity);

        
    }


    private object locker = new();
    public void itemPickedUp()
    {
        lock (locker)
        {
            count--;
            Invoke(nameof(restoreCooldown), Random.Range(spawnCooldownMin, spawnCooldownMax));
        }
    }
}
