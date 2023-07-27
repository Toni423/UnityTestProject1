using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastGuy : NewEnemy
{

    protected override void shoot()
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
        StartCoroutine(DelayedCoroutine.delayedCoroutine(0.5f, () => shootSound.Play()));
        StartCoroutine(DelayedCoroutine.delayedCoroutine(0.7f, () => Instantiate(bullet, transform.position, Quaternion.identity)));
        StartCoroutine(DelayedCoroutine.delayedCoroutine(Random.Range(reloadTimeMin, reloadTimeMax) + 1.4f, () => reloading = false));

        
    }

    
}
