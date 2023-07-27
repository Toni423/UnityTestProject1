using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    public GameObject moneyField;
    private TextMeshProUGUI highscore;


    private void Start()
    {
        highscore = GameObject.Find("HighScoreText").GetComponent<TextMeshProUGUI>();
        highscore.SetText("HighScore: " + PlayerPrefs.GetInt("HighScore", 0));

        moneyField.GetComponent<TextMeshProUGUI>().SetText("" + PlayerPrefs.GetInt("Money", 0));
    }

    public void reset() {
        PlayerPrefs.DeleteAll();

        highscore.GetComponent<TextMeshProUGUI>().SetText("HighScore: 0");
        moneyField.GetComponent<TextMeshProUGUI>().SetText("" + 0);
    }
}
