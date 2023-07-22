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

    public float verticalSpeed = 5f;
    public GameObject playerBullet;
    public int life = 3;
    public GameObject UIlifeText;
    private TextMeshProUGUI lifeText;
    

    private void Start()
    {
        lifeText = UIlifeText.GetComponent<TextMeshProUGUI>();
        rb = GetComponent<Rigidbody2D>();
        transform.SetPositionAndRotation(new Vector3(-8.4f, 0, 0), new Quaternion(0f, 0f, 0f, 0f));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !reloading)
        {
            spawnCurBullet();
            reloading = true;
            Invoke(nameof(reload), 2f);
        }
    }

    private void FixedUpdate()
    {
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 movement = Time.fixedDeltaTime * verticalSpeed * new Vector2(0f, vertical).normalized;

        rb.MovePosition(rb.position + movement);

    }

    private void spawnCurBullet()
    {
        if(playerBullet == null)
        {
            Debug.Log("Player: no bullet selected");
            return;
        }
        Instantiate(playerBullet, transform.position, Quaternion.identity);
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
            life = Mathf.Min(3, life + gain);
            lifeText.text = ("Life: " + life);
        }
    }
}
