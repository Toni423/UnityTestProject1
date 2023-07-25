using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehav : MonoBehaviour
{
    public float moveSpeed = 8f;
    public int direct;
    private string hittable;

    // Start is called before the first frame update
    void Start()
    {
        hittable = direct == 1 ? "Enemy" : "Player";
        moveSpeed *= direct;
        Destroy(gameObject, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the movement vector for moving right
        Vector3 movement = new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

        // Update the object's position
        transform.Translate(movement);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the other collider belongs to the "ObjectB."
        if (other.gameObject.CompareTag(hittable))
        {
            Destroy(gameObject, 0.05f);
        }
        else if (other.gameObject.CompareTag("Border"))
        {
            Destroy(gameObject);
        }
    }
}
