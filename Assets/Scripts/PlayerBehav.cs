using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehav : MonoBehaviour

{
    private Rigidbody2D rb;
    private bool reloading = false;
    public float reloadTime = 1f;
    private float chargeTime = 1.5f;

    public float verticalSpeed = 5f;
    public GameObject playerBullet;
    public GameObject superiorPlayerBullet;
    private int life;
    private int maxLife;
    public AudioSource healAudio;
    public AudioSource shootSound;
    public AudioSource superiorShootSound;
    public AudioSource hitSound;
    private bool isWpressed = false;
    private float shootStartTime;
    

    public GameObject healthbar;
    private Image health;

    public GameObject reloadBarImage;
    private Image reloadBarImg;

    public GameObject chargeBarImage;
    private Image chargeBarImg;

    public GameObject eventSystem;

    public GameObject hat;
    public GameObject pet;

    public Animator animator;


    private void Awake() {
        animator.SetBool("IsRed", PlayerPrefs.GetInt("redBirdIsActive", 0) == 1);
    }

    private void Start()
    {
        maxLife = 3 + Mathf.Min(PlayerPrefs.GetInt("hearts", 0), 3);
        life = maxLife;

        verticalSpeed += PlayerPrefs.GetInt("speed", 0);

        reloadTime -= (float)PlayerPrefs.GetInt("cannon", 0) / 10f;
        chargeTime -= (float)PlayerPrefs.GetInt("cannon", 0) / 10f;

        hat.SetActive(PlayerPrefs.GetInt("topHatIsActive", 0) == 1);
        pet.SetActive(PlayerPrefs.GetInt("petBirdIsActive", 0) == 1);

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
            StartCoroutine(chargeBar());
        }
        
        else if (!Input.GetKey(KeyCode.Space) && isWpressed)
        {
            
            StartCoroutine(shoot(Time.time - shootStartTime));
            reloadBarImg.fillAmount = 0f;
            StartCoroutine(reloadBar());
            isWpressed = false;
            StartCoroutine(DelayedCoroutine.delayedCoroutine(reloadTime, () => reloading = false));
            StartCoroutine(DelayedCoroutine.delayedCoroutine(0.01f, () => chargeBarImg.fillAmount = 0f));
            
        }
    }

    private IEnumerator chargeBar() {
        while(Input.GetKey(KeyCode.Space)) {
            
            chargeBarImg.fillAmount += 0.01f;
            yield return new WaitForSeconds(0.01f * chargeTime);
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
            health.fillAmount = (float) life /  maxLife;
        }

        if (other.gameObject.CompareTag("2DmgEnemybullet"))
        {
            hitSound.Play();
            life -= 2;
            health.fillAmount = (float) life / maxLife;
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
                life = Mathf.Min(maxLife, life + 1);
                health.fillAmount = (float) life / maxLife;
                return;
            }

            if (itemName == "feather") {
                verticalSpeed *= 1.5f;
                StartCoroutine(DelayedCoroutine.delayedCoroutine(10f, () => verticalSpeed /= 1.5f));
                return;
            }

            if (itemName == "apple") {
                healAudio.Play();
                life = Mathf.Min(maxLife, life + 2);
                health.fillAmount = (float) life / maxLife;
                return;
            }

            if (itemName == "seed") {
                reloadTime /= 2f;
                chargeTime /= 2f;
                StartCoroutine(DelayedCoroutine.delayedCoroutine(10f, () => reloadTime *= 2f));
                StartCoroutine(DelayedCoroutine.delayedCoroutine(10f, () => chargeTime *= 2f));
                return;
            }

        }
    }

    
    
}
