using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemy : MonoBehaviour
{
    private int direct;
    public float moveSpeedMin = 1.5f;
    public float moveSpeedMax = 3f;
    private float moveSpeed;
    private bool reloading = false;
    public float reloadTimeMin = 3f;
    public float reloadTimeMax = 5f;
    public GameObject bullet;
    public int life = 3;
    private NewEnemySpawnerBehav enemySpawner;
    public AudioSource shootSound;
    public AudioSource deathSound;



    // Start is called before the first frame update
    void Start()
    {
        GameObject spawner = GameObject.FindGameObjectWithTag("EnemySpawner");
        enemySpawner = spawner.GetComponent<NewEnemySpawnerBehav>();

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

    private void move()
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

    private void shoot()
    {
        if (reloading)
        {
            return;
        }
        if (bullet == null)
        {
            Debug.Log("Enemy: No bullet selected");
            return;
        }
        reloading = true;
        shootSound.Play();
        Invoke(nameof(spawnBullet), 0.2f);
        Invoke(nameof(reload), Random.Range(reloadTimeMin, reloadTimeMax));
    }
    private void spawnBullet()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }
    private void reload()
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
                deathSound.Play();
                enemySpawner.increaseScore(20);
                Invoke(nameof(destroyThis), 0.2f);
                enemySpawner.decreaseCount();
            }
        }
    }
    private void destroyThis()
    {
        Destroy(gameObject);
    }
}
