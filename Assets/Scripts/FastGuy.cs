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
        Invoke(nameof(spawnBullet), 0.2f);
        Invoke(nameof(secondShoot), 0.3f);
        Invoke(nameof(reload), Random.Range(reloadTimeMin, reloadTimeMax));
    }

    private void secondShoot()
    {
        shootSound.Play();
        Invoke(nameof(spawnBullet), 0.2f);
    }

}
