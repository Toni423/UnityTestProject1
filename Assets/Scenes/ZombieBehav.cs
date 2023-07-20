using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehav : MonoBehaviour
{
    private int direct = 1;
    public float moveSpeed = 3f;
    private bool reloading = false;
    public float reloadTime = 3f;
    public GameObject bullet;
    public int life = 3;

    // Start is called before the first frame update
    void Start()
    {
        
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

        shoot();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the other collider belongs to the "ObjectB."
        if (other.gameObject.CompareTag("Bullet"))
        {
            life--;
            if(life == 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void shoot()
    {
        if (reloading)
        {
            return;
        }
        if(bullet == null)
        {
            Debug.Log("Zombie: No bullet selected");
            return;
        }
        reloading = true;
        Instantiate(bullet, transform.position, Quaternion.identity);
        Invoke(nameof(reload), reloadTime);
    }


    private void reload()
    {
        reloading = false;
    }
}
