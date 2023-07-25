using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenButtons : MonoBehaviour
{
    
    public void toMainMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void tryAgain() {
        Time.timeScale = 1f;
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
