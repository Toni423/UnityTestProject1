using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SquareBehav : MonoBehaviour

{
    private Rigidbody2D rb;
    private bool reloading = false;
    private float reloadTime = 2f;

    public float verticalSpeed = 5f;
    public GameObject playerBullet;
    public GameObject superiorPlayerBullet;
    public int life = 3;
    public GameObject UIlifeText;
    private TextMeshProUGUI lifeText;
    public AudioSource healAudio;
    public AudioSource shootSound;
    public AudioSource superiorShootSound;
    private bool isWpressed = false;
    private float shootStartTime;



    private void Start()
    {
        lifeText = UIlifeText.GetComponent<TextMeshProUGUI>();
        rb = GetComponent<Rigidbody2D>();
        transform.SetPositionAndRotation(new Vector3(-8.4f, 0, 0), new Quaternion(0f, 0f, 0f, 0f));
    }

    private void Update()
    {

        if (Input.GetKey(KeyCode.Space) && !reloading && !isWpressed)
        {
            reloading = true;
            isWpressed = true;
            shootStartTime = Time.time;
        }
        else if (!Input.GetKey(KeyCode.Space) && isWpressed)
        {
            StartCoroutine(shoot(Time.time - shootStartTime));
            isWpressed = false;
            Invoke(nameof(reload), reloadTime);
            
        }
    }

    private void FixedUpdate()
    {
        if(!isWpressed)
        {
            float vertical = Input.GetAxisRaw("Vertical");

            Vector2 movement = Time.fixedDeltaTime * verticalSpeed * new Vector2(0f, vertical).normalized;

            rb.MovePosition(rb.position + movement);
        }

    }

    private IEnumerator shoot(float loadTime)
    {
        if(loadTime < 2f)
        {
            shootSound.Play();
        }
        else
        {
            superiorShootSound.Play();
        }

        yield return new WaitForSeconds(0.2f);


        if (loadTime < 2f)
        {
            if (playerBullet == null)
            {
                Debug.Log("Player: No bullet selected");
                yield break;
            }
            Instantiate(playerBullet, transform.position, Quaternion.identity);
            yield break;
        }
        else if (playerBullet == null)
        {
            Debug.Log("Player: No superior bullet selected");
            yield break;
        }
        BulletBehav temp = Instantiate(playerBullet, transform.position, Quaternion.identity).GetComponent<BulletBehav>();
        temp.moveSpeed = 14;
    }

    

    private void reload()
    {
        reloading = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the other collider belongs to the "ObjectB."
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            life--;
            lifeText.SetText("Life: " + life);
        }

        if (other.gameObject.CompareTag("2DmgEnemybullet"))
        {
            life -= 2;
            lifeText.text = ("Life: " + life);
        }

        if (life <= 0)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    private object locker = new();
    public void lifegain(int gain)
    {
        lock (locker)
        {
            healAudio.Play();
            life = Mathf.Min(3, life + gain);
            lifeText.text = ("Life: " + life);
        }
    }
}