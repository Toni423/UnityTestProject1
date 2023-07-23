using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{
    public void toMainScreen()
    {
        Invoke(nameof(exiting), 0.75f);
    }
    private void exiting()
    {
        SceneManager.LoadScene("MainMenu");

    }
}
