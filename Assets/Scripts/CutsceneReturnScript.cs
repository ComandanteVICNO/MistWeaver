using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutsceneReturnScript : MonoBehaviour
{
    public VideoClip video;
    public double timer;
    void Start()
    {
        timer = video.length + 2f;
        StartCoroutine(Return());
    }

    IEnumerator Return()
    {
        yield return new WaitForSecondsRealtime((float)timer);

        SceneManager.LoadScene("MainMenu");
    }
    
}
