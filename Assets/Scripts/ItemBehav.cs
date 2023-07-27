using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehav : MonoBehaviour
{
    private PlayerBehav player;
    private NewItemSpawnerBehav itemSpawner;
    private bool pickedUp = false;
    public string itemName;


    // Start is called before the first frame update
    void Start()
    {
        GameObject square = GameObject.FindGameObjectWithTag("Player");
        player = square.GetComponent<PlayerBehav>();

        GameObject spawner = GameObject.FindGameObjectWithTag("ItemSpawner");
        itemSpawner = spawner.GetComponent<NewItemSpawnerBehav>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the other collider belongs to the "ObjectB."
        if (other.gameObject.CompareTag("Player") && !pickedUp)
        {
            pickedUp = true;
            player.itemPickedUp(itemName);
            itemSpawner.decreaseCount();
            Destroy(gameObject, 0.05f);
        }
    }

}
