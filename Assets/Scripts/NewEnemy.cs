using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemy : MonoBehaviour
{
    private int direct;
    public float moveSpeedMin = 1.5f;
    public float moveSpeedMax = 3f;
    private float moveSpeed;
    protected bool reloading = false;
    public float reloadTimeMin = 3f;
    public float reloadTimeMax = 5f;
    public GameObject bullet;
    public int life = 3;
    private NewEnemySpawnerBehav enemySpawner;
    public AudioSource shootSound;
    public AudioSource deathSound;
    public AudioSource hitSound;
    public int scorePoints;

    private float minYvalue;
    private float maxYvalue;



    // Start is called before the first frame update
    void Start()
    {
        minYvalue = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).y + 0.9f;
        maxYvalue = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane)).y - 0.9f;


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
        if (gameObject.transform.position.y > maxYvalue || gameObject.transform.position.y < minYvalue)
        {
            direct *= -1;
        }

        // Calculate the movement vector for moving right
        Vector3 movement = new(0f, moveSpeed * Time.deltaTime * direct, 0f);

        // Update the object's position
        transform.Translate(movement);
    }

    protected virtual void shoot()
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
        StartCoroutine(DelayedCoroutine.delayedCoroutine(0.2f, () => Instantiate(bullet, transform.position, Quaternion.identity)));
        StartCoroutine(DelayedCoroutine.delayedCoroutine(Random.Range(reloadTimeMin, reloadTimeMax) + 0.2f, () => reloading = false));
        
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the other collider belongs to the "ObjectB."
        if (other.gameObject.CompareTag("Bullet"))
        {
            hitSound.Play();
            life--;
            if (life == 0)
            {
                deathSound.Play();
                enemySpawner.increaseScore(scorePoints);
                StartCoroutine(DelayedCoroutine.delayedCoroutine(0.2f, () => Destroy(gameObject)));
                enemySpawner.decreaseCount();
            }
        }
    }
    
}
