using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextSceneScript : MonoBehaviour
{
    [Header("Text GameObject Refs")]
    public TMP_Text text1;
    public TMP_Text text2;
    public TMP_Text text3;
    public TMP_Text text4;
    public TMP_Text textFinal;

    [Header("Timers")]
    public float textFadeDuration;
    public float timeWaitingForText;

    [Header("Misc")]
    public string sceneToTransition;
    public Ease linearEase;

    [Header("Audio")]
    public AudioSource windSource;
    public float fadeDuration;

    public GameObject blackUI;
    public Image blackImage;

    void Start()
    {
        text1.DOFade(0f, 0f).SetEase(linearEase);
        text2.DOFade(0f, 0f).SetEase(linearEase);
        text3.DOFade(0f, 0f).SetEase(linearEase);
        text4.DOFade(0f, 0f).SetEase(linearEase);
        textFinal.DOFade(0f, 0f).SetEase(linearEase);
        StartCoroutine(TextAnimation());
        StartCoroutine(FadeIn());
    }

    IEnumerator TextAnimation()
    {
        yield return new WaitForSecondsRealtime(timeWaitingForText);

        text1.DOFade(1f, textFadeDuration).SetEase(linearEase);

        yield return new WaitForSecondsRealtime(textFadeDuration);

        yield return new WaitForSecondsRealtime(timeWaitingForText);

        text2.DOFade(1f, textFadeDuration).SetEase(linearEase);

        yield return new WaitForSecondsRealtime(textFadeDuration);

        yield return new WaitForSecondsRealtime(timeWaitingForText);

        text3.DOFade(1f, textFadeDuration).SetEase(linearEase);
        text4.DOFade(1f, textFadeDuration).SetEase(linearEase);

        yield return new WaitForSecondsRealtime(textFadeDuration);

        yield return new WaitForSecondsRealtime(timeWaitingForText + 1);

        text1.DOFade(0f, textFadeDuration).SetEase(linearEase);
        text2.DOFade(0f, textFadeDuration).SetEase(linearEase);
        text3.DOFade(0f, textFadeDuration).SetEase(linearEase);
        text4.DOFade(0f, textFadeDuration).SetEase(linearEase);

        yield return new WaitForSecondsRealtime(textFadeDuration);

        yield return new WaitForSecondsRealtime(timeWaitingForText);

        textFinal.DOFade(1f, textFadeDuration).SetEase(linearEase);

        yield return new WaitForSecondsRealtime(textFadeDuration);

        StartCoroutine(FadeOut());

        yield return new WaitForSecondsRealtime(timeWaitingForText);

        blackUI.SetActive(true);
        textFinal.DOFade(0f, textFadeDuration).SetEase(linearEase);
        blackImage.DOFade(1f, textFadeDuration).SetEase(linearEase);

        yield return new WaitForSecondsRealtime(textFadeDuration);

        SceneManager.LoadScene(sceneToTransition);

    }

    IEnumerator FadeIn()
    {
        float currentTime = 0f;
        windSource.volume = 0f;
        windSource.Play();

        while (currentTime < fadeDuration)
        {
            windSource.volume = Mathf.Lerp(0f, 1f, currentTime / fadeDuration);
            currentTime += Time.deltaTime;
            yield return null;
        }

        windSource.volume = 1f;
    }

    IEnumerator FadeOut()
    {
        float currentTime = 0f;

        while (currentTime < fadeDuration)
        {
            windSource.volume = Mathf.Lerp(1f, 0f, currentTime / fadeDuration);
            currentTime += Time.deltaTime;
            yield return null;
        }

        windSource.volume = 0f;
        windSource.Stop();
    }
}
