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
    public TMP_Text[] stats;
    public TMP_Text timerValue;
    public TMP_Text conesAmount;
    public AudioSource audioSource;

    void Start()
    {
        timer = video.length + 2f;
        StartCoroutine(Return());
        GrabAndSetStats();
    }

    IEnumerator Return()
    {
        audioSource.DOFade(1f, textFadeTime);
        creditsText.DOFade(0f, 0f);
        foreach (TMP_Text text in stats)
        {
            text.DOFade(0f, 0f);
        }
        yield return new WaitForSecondsRealtime((float)timer);

        videoUI.SetActive(false);
        
        foreach(TMP_Text text in stats)
        {
            text.DOFade(1f, textFadeTime).SetEase(textEase);
        }
        yield return new WaitForSecondsRealtime(textFadeTime);

        yield return new WaitForSecondsRealtime(creditsTimer);

        foreach (TMP_Text text in stats)
        {
            text.DOFade(0f, textFadeTime).SetEase(textEase);
        }

        yield return new WaitForSecondsRealtime(textFadeTime);

        creditsText.DOFade(1f, textFadeTime).SetEase(textEase);
        yield return new WaitForSecondsRealtime(textFadeTime);

        yield return new WaitForSecondsRealtime(creditsTimer);

        creditsText.DOFade(0f, textFadeTime).SetEase(textEase);
        audioSource.DOFade(0f, textFadeTime +1f);
        yield return new WaitForSecondsRealtime(textFadeTime + 2f);

        SceneManager.LoadScene("MainMenu");
    }


    void GrabAndSetStats()
    {
        int coneCount = PlayerPrefs.GetInt("ConeCount");
        conesAmount.text = coneCount + " / 12";

        float timer = PlayerPrefs.GetFloat("Timer");
        string timeFormated = FormatTime(timer);
        timerValue.text = timeFormated;

    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time * 1000) % 1000);

        // Use string.Format to create a formatted string
        return string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milliseconds);
    }
}
