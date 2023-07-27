using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SquareBehav : MonoBehaviour

{
    private Rigidbody2D rb;
    private bool reloading = false;
    private float reloadTime = 1f;
    private float chargeTime = 1.5f;

    public float verticalSpeed = 5f;
    public GameObject playerBullet;
    public GameObject superiorPlayerBullet;
    public int life = 3;
    public AudioSource healAudio;
    public AudioSource shootSound;
    public AudioSource superiorShootSound;
    public AudioSource hitSound;
    private bool isWpressed = false;
    private float shootStartTime;
    private bool charging = false;

    public GameObject healthbar;
    private Image health;

    public GameObject reloadBarImage;
    private Image reloadBarImg;

    public GameObject chargeBarImage;
    private Image chargeBarImg;

    public GameObject eventSystem;


    private void Start()
    {
        health = healthbar.GetComponent<Image>();
        reloadBarImg = reloadBarImage.GetComponent<Image>();
        chargeBarImg = chargeBarImage.GetComponent<Image>();

        rb = GetComponent<Rigidbody2D>();


        Camera mainCamera = Camera.main;
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        transform.SetPositionAndRotation(new Vector3(bottomLeft.x + 1f, 0, 0), new Quaternion(0f, 0f, 0f, 0f));
        
       
    }

    private void Update()
    {

        if (Input.GetKey(KeyCode.Space) && !reloading && !isWpressed)
        {
            reloading = true;
            isWpressed = true;
            shootStartTime = Time.time;
        }
        else if(Input.GetKey(KeyCode.Space) && isWpressed && !charging)
        {
            charging = true;
            chargeBarImg.fillAmount += 0.01f;
            StartCoroutine(DelayedCoroutine.delayedCoroutine(0.013f, () => charging = false));
            
        }
        else if (!Input.GetKey(KeyCode.Space) && isWpressed)
        {
            chargeBarImg.fillAmount = 0f;
            StartCoroutine(shoot(Time.time - shootStartTime));
            reloadBarImg.fillAmount = 0f;
            StartCoroutine(reloadBar());
            isWpressed = false;
            StartCoroutine(DelayedCoroutine.delayedCoroutine(reloadTime, () => reloading = false));
               
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
        if(loadTime < chargeTime)
        {
            shootSound.Play();
        }
        else
        {
            superiorShootSound.Play();
        }

        yield return new WaitForSeconds(0.2f);


        if (loadTime < chargeTime)
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

    private IEnumerator reloadBar()
    {
        while(reloadBarImg.fillAmount < 1f)
        {
            reloadBarImg.fillAmount += 0.01f;
            yield return new WaitForSeconds(0.01f * reloadTime);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the other collider belongs to the "ObjectB."
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            hitSound.Play();
            life--;
            health.fillAmount = life / 3f;
        }

        if (other.gameObject.CompareTag("2DmgEnemybullet"))
        {
            hitSound.Play();
            life -= 2;
            health.fillAmount = life / 3f;
        }

        if (life <= 0)
        {
            eventSystem.GetComponent<PlayEventSystem>().showDeathScreen();
        }
    }

    private object locker = new();
    public void itemPickedUp(string itemName) {
        lock(locker) {
            if (itemName == "heart") {
                healAudio.Play();
                life = Mathf.Min(3, life + 1);
                health.fillAmount = life / 3f;
                return;
            }

            if (itemName == "feather") {
                verticalSpeed *= 1.5f;
                StartCoroutine(DelayedCoroutine.delayedCoroutine(5f, () => verticalSpeed /= 1.5f));
            }
        }
    }

    
    
}
