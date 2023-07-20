using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderBehav : MonoBehaviour
{

    public GameObject player;
    public GameObject enemySpawner;

    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
        {
            Debug.Log("Border: Player not selected");
        }
        Instantiate(player);
        if (enemySpawner == null)
        {
            Debug.Log("Border: EnemySpawner not selected");
        }
        Instantiate(enemySpawner);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
