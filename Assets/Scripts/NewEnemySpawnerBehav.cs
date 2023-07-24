using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewEnemySpawnerBehav : SpawnerBehav
{

    public GameObject zombie;
    public GameObject skeleton;
    private TextMeshProUGUI scoreBoard;
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        scoreBoard = GameObject.Find("ScoreBoard").GetComponent<TextMeshProUGUI>();
        spawnCooldownMin = 7f;
        spawnCooldownMax = 11f;
        itemMaxCount = 4; ;


        Camera mainCamera = Camera.main;
        transform.SetPositionAndRotation(new Vector3(mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane)).x - 1f, 0f, 0f), new Quaternion(0f, 0f, 0f, 0f));
    }


    protected override GameObject getObjectToSpawn()
    {
        if(zombie == null)
        {
            Debug.Log("EnemySpawner: Zombie not set");
        }
        if (skeleton == null)
        {
            Debug.Log("EnemySpawner: Skeleton not set");
        }

        return Random.Range(0, 3) < 2 ? zombie : skeleton;
    }

    private object locker2 = new();
    public void increaseScore(int points)
    {
        lock (locker2)
        {
            score += points;
            scoreBoard.SetText("" + score);
        }
    }

    private void OnDestroy()
    {
        if(score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }
}
