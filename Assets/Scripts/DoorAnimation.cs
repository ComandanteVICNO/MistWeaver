using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    public Transform originalTransform;
    public Transform targetTransform;

    public float closeAnimationSpeed;
    public float openAnimationSpeed;

    public Ease closeEase;
    public Ease openEase;

    [Header("Audio")]
    public AudioSource doorAudioSource;
    public AudioClip doorAudioClip;

    bool wasDoorClose = false;
    bool wasDoorOpen = false;
    public void CloseDoors()
    {
        if (wasDoorClose) return;
        transform.DOMove(targetTransform.position, closeAnimationSpeed) .SetEase(closeEase);
        PlayAudio();
        wasDoorClose = true;
    }

    public void OpenDoors()
    {
        if(wasDoorOpen) return;
        transform.DOMove(originalTransform.position, openAnimationSpeed).SetEase(openEase);
        PlayAudio();
        wasDoorOpen = true;
    }

    public void PlayAudio()
    {
        float randomPitch = UnityEngine.Random.Range(0.8f, 1.2f);
        doorAudioSource.pitch = randomPitch;
        doorAudioSource.PlayOneShot(doorAudioClip);
    }

}
