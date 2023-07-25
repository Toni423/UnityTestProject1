using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{

    public void exit() {
        StartCoroutine(exiting());
    }

    private IEnumerator exiting()
    {
        yield return new WaitForSecondsRealtime(0.75f);

        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
