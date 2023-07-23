using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    

    private void Start()
    {
        TextMeshProUGUI highscore = GameObject.Find("HighScoreText").GetComponent<TextMeshProUGUI>();
        highscore.SetText("HighScore: " + PlayerPrefs.GetInt("HighScore", 0));
    }

    public void startGame()
    {
        Invoke(nameof(starting), 1f);
    }

    private void starting()
    {
        SceneManager.LoadScene("PlayScene");
    }
}
