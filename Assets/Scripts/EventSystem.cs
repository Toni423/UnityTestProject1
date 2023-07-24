using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public Animator animator;
    public string clipName;

    void Start()
    {
        // Play the animation.
        animator.Play(clipName, 0);
    }

    public void quitGame()
    {
        Application.Quit();
    }
 
}
