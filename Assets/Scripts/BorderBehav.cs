using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderBehav : MonoBehaviour
{

    public GameObject enemySpawner;
    public GameObject itemSpawner;

    // Start is called before the first frame update
    void Start()
    {
        if (enemySpawner == null)
        {
            Debug.Log("Border: EnemySpawner not selected");
        }
        Instantiate(enemySpawner);
        if (itemSpawner == null)
        {
            Debug.Log("Border: ItemSpawner not selected");
        }
        Instantiate(itemSpawner);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
