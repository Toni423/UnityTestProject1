using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButtons : MonoBehaviour
{

    public string scene;
    
    public void loadScene() {
        
        StartCoroutine(loadingScene());
    }


    private IEnumerator loadingScene() {
        yield return new WaitForSecondsRealtime(0.7f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene);
    }
}
