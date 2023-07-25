using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenButtons : MonoBehaviour
{
    
    public void toMainMenu() {
        
        StartCoroutine(loadScene("MainMenu"));
    }

    public void tryAgain() {
        
        StartCoroutine(loadScene(SceneManager.GetActiveScene().name));
    }

    private IEnumerator loadScene(string scene) {
        yield return new WaitForSecondsRealtime(0.7f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene);
    }
}
