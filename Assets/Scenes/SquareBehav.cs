using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareBehav : MonoBehaviour

{
    private Rigidbody2D rb;
    private bool reloading = false;

    public float verticalSpeed = 5f;
    public GameObject playerBullet;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
}
