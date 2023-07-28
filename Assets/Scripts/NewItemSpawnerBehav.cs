using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewItemSpawnerBehav : SpawnerBehav
{
    public GameObject heart;
    private GameObject square;
    public GameObject feather;
    public GameObject apple;
    public GameObject seed;

    private List<GameObject> filteredItems = new();

    // Start is called before the first frame update
    void Start()
    {
        filteredItems.Add(heart);

        GameObject[] unfilteredItems = { feather, apple, seed };
        bool[] itemBools = { PlayerPrefs.GetInt("featherIsActive", 0) == 1 , PlayerPrefs.GetInt("appleIsActive", 0) == 1 , PlayerPrefs.GetInt("seedIsActive", 0) == 1 };

        if( unfilteredItems.Length != itemBools.Length) {
            Debug.Log("ItemSpawner: Length of unfiltered items and bools not equal");
        }

        for(int i = 0; i < unfilteredItems.Length; i++) {
            if(itemBools[i]) {
                filteredItems.Add(unfilteredItems[i]);
            }
        }


        cooldown = true;
        spawnCooldownMin = 20f;
        spawnCooldownMax = 25f;
        itemMaxCount = 1;


        Camera mainCamera = Camera.main;
        transform.SetPositionAndRotation(new Vector3(Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).x + 1f, 0, 0), new Quaternion(0f, 0f, 0f, 0f));
        square = GameObject.FindGameObjectWithTag("Player");

        Invoke(nameof(restoreCooldown), Random.Range(spawnCooldownMin, spawnCooldownMax));
    }

    protected override GameObject getObjectToSpawn()
    {

        return filteredItems[Random.Range(0, filteredItems.Count)];

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


        }
    }

}
