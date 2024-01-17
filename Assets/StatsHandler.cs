using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsHandler : MonoBehaviour
{
    [Header("Cone")]
    public GameObject playerCone;
    int coneCount;
    public AudioSource coneAudioSource;
    public AudioClip coneAudioClip;

    [Header("Time")]
    float timer;
    bool gotTimer = false;
    private void Awake()
    {
        playerCone.SetActive(false);
    }

    private void Update()
    {
        GetConeCount();
        Timer();
    }

    public void GetConeCount()
    {
        coneCount = PlayerPrefs.GetInt("ConeCount");

        if(coneCount == 12)
        {
            playerCone.SetActive(true);
        }
    }

    public void Timer()
    {
        if(!gotTimer)
        {
            timer = PlayerPrefs.GetFloat("Timer");
            gotTimer = true;
        }

        timer += Time.deltaTime;
        PlayerPrefs.SetFloat("Timer", timer);
        PlayerPrefs.Save();
        Debug.Log(timer);
    }



}
