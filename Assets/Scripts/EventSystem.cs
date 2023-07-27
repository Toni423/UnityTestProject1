using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public Animator animator;
    public string clipName;

    public AudioSource backgroundMusic;

    void Start()
    {
        // Play the animation.
        animator.Play(clipName, 0);
    }

    public void quitGame()
    {
        backgroundMusic.Stop();
        StartCoroutine(DelayedCoroutine.delayedCoroutine(1f, () => Application.Quit()));
    }
   
}
