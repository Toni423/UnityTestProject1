using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletBehav : MonoBehaviour
{
    public float moveSpeed = 8f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the movement vector for moving right
        Vector3 movement = new Vector3(- moveSpeed * Time.deltaTime, 0f, 0f);

        // Update the object's position
        transform.Translate(movement);
    }
}
