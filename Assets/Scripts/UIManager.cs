using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public string gameplaySceneName = "SampleScene";

    public void startGame()
    {
        Invoke(nameof(starting), 1f);
    }

    private void starting()
    {
        SceneManager.LoadScene(gameplaySceneName);
    }

}
