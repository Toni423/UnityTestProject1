using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBehav : MonoBehaviour 
{
    private int direct;
    public float moveSpeedMin = 1.5f;
    public float moveSpeedMax = 3f;
    private float moveSpeed;
    private bool reloading = false;
    public float reloadTimeMin = 3f;
    public float reloadTimeMax = 5f;
    public GameObject bullet;
    public int life = 5;
    public EnemySpawnerBehav enemySpawner;


    // Start is called before the first frame update
    void Start()
    {
        GameObject spawner = GameObject.FindGameObjectWithTag("EnemySpawner");
        enemySpawner = spawner.GetComponent<EnemySpawnerBehav>();


        moveSpeed = Random.Range(moveSpeedMin, moveSpeedMax);
        direct = Random.Range(-1, 1);
        if (direct == 0)
        {
            direct = 1;
        }
    }


    // Update is called once per frame
    void Update()
    {
        move();

        shoot();
    }

    protected void move()
    {
        if (gameObject.transform.position.y > 4.156 || gameObject.transform.position.y < -4.526)
        {
            direct *= -1;
        }

        // Calculate the movement vector for moving right
        Vector3 movement = new(0f, moveSpeed * Time.deltaTime * direct, 0f);

        // Update the object's position
        transform.Translate(movement);
    }


    protected void shoot()
    {
        if (reloading)
        {
            return;
        }
        if (bullet == null)
        {
            Debug.Log("Skeleton: No bullet selected");
            return;
        }
        reloading = true;
        Instantiate(bullet, transform.position, Quaternion.identity);
        Invoke(nameof(reload), Random.Range(reloadTimeMin, reloadTimeMax));
    }


    protected void reload()
    {
        reloading = false;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the other collider belongs to the "ObjectB."
        if (other.gameObject.CompareTag("Bullet"))
        {
            life--;
            if (life == 0)
            {
                Destroy(gameObject);
                enemySpawner.enemyDied();
            }
        }
    }
}
