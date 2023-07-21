using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBehav : MonoBehaviour
{
    private SquareBehav player;
    private NewItemSpawnerBehav itemSpawner;
    private bool pickedUp = false;


    // Start is called before the first frame update
    void Start()
    {
        GameObject square = GameObject.FindGameObjectWithTag("Player");
        player = square.GetComponent<SquareBehav>();

        GameObject spawner = GameObject.FindGameObjectWithTag("ItemSpawner");
        itemSpawner = spawner.GetComponent<NewItemSpawnerBehav>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the other collider belongs to the "ObjectB."
        if (other.gameObject.CompareTag("Player") && !pickedUp)
        {
            pickedUp = true;
            player.lifegain(1);
            itemSpawner.decreaseCount();
            Destroy(gameObject, 0.05f);
        }
    }

}
