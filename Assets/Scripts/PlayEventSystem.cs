using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEventSystem : MonoBehaviour
{

    public Animator animator;
    public string clipName;

    public GameObject pauseCanvas;
    private bool isPaused = false;

    void Start()
    {
        // Play the animation.
        animator.Play(clipName, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }


    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f; // Freeze or unfreeze time to pause/unpause the game.

        // Show/hide the pause canvas based on the pause state.
        pauseCanvas.SetActive(isPaused);
    }
}
