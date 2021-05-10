using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public Animator transitionAnimator;
    public AnimationClip toBlackAnimation;
    public AnimationClip fromBlackAnimation;
    public float FadeOutDelay = 1f;
    private float toBlackTime;
    private float fromBlackTime;
    private void Start()
    {
        fromBlackTime = fromBlackAnimation.length;
        toBlackTime = toBlackAnimation.length;
        transitionAnimator.SetTrigger("FromBlack");
    }
    public void LoadScene(string sceneName)
    {
        StartCoroutine(TransitionTo(sceneName));
    }

    IEnumerator TransitionTo(string sceneName)
    {
        yield return new WaitForSeconds(FadeOutDelay);
        transitionAnimator.SetTrigger("ToBlack");
        yield return new WaitForSeconds(toBlackTime);
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        StartCoroutine(TransitionQuit());
    }

    IEnumerator TransitionQuit()
    {
        yield return new WaitForSeconds(FadeOutDelay * 2);
        transitionAnimator.SetTrigger("ToBlack");
        yield return new WaitForSeconds(toBlackTime);

#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
		    Application.Quit();
#endif
        
    }
}
