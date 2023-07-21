using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public string gameplaySceneName = "SampleScene";

    public void startGame()
    {
        SceneManager.LoadScene(gameplaySceneName);
    }

}
