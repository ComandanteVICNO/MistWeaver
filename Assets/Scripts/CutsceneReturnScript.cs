using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class CutsceneReturnScript : MonoBehaviour
{
    public VideoClip video;
    public double timer;
    public GameObject videoUI;
    public GameObject creditsUI;
    public TMP_Text creditsText;
    public float creditsTimer;
    public float textFadeTime;
    public Ease textEase;
    void Start()
    {
        timer = video.length + 2f;
        StartCoroutine(Return());
    }

    IEnumerator Return()
    {
        creditsText.DOFade(0f, 0f);
        yield return new WaitForSecondsRealtime((float)timer);

        videoUI.SetActive(false);
        


        creditsText.DOFade(1f, textFadeTime).SetEase(textEase);
        yield return new WaitForSecondsRealtime(textFadeTime);

        yield return new WaitForSecondsRealtime(creditsTimer);

        creditsText.DOFade(0f, textFadeTime).SetEase(textEase);

        yield return new WaitForSecondsRealtime(textFadeTime + 2f);

        SceneManager.LoadScene("MainMenu");
    }
    
    

}
