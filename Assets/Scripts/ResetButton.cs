using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResetButton : MonoBehaviour
{
    public GameObject highscoreText;
    
    public void reset() {
        PlayerPrefs.DeleteAll();

        highscoreText.GetComponent<TextMeshProUGUI>().SetText("HighScore: 0");
    }

}
