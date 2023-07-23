using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public string gameplaySceneName = "PlayScene";

    public void startGame()
    {
        Invoke(nameof(starting), 1f);
    }

    private void starting()
    {
        SceneManager.LoadScene(gameplaySceneName);
    }

}
