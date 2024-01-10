using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueInteractor : MonoBehaviour
{
    [Multiline]
    public string[] dialogue;
    public TMP_Text hintText;
    public TMP_Text dialogueText;
    private int currentIndex = 0;
    public DialogueWriter writer;

    public GameObject screenLight;


    [Header("Tween Animation")]
    public GameObject screen;
    public GameObject screenHide;

    public Transform originalTransform;
    public Transform hidingTransform;

    public float animationSpeed;
    public float outAnimationSpeed;

    public Ease InEase;
    public Ease OutEase;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip[] glitchClips;

    bool firstMessageRead;
    bool playerInZone;

    private void Awake()
    {
        writer = FindObjectOfType<DialogueWriter>();
        dialogueText = writer.dialogueText;
        writer.HideDialogue();
        
        firstMessageRead = false;
    }
    private void Start()
    {
        
        HideHint();

    }

    void Update()
    {
        if (playerInZone && UserInput.instance.controls.Player.Interact.WasPressedThisFrame())
        {

            CamManager.current.FocusCamera(transform, transform);

            

            

            if(!firstMessageRead)
            {
                DialogueWriter.instance.ShowDialogue(); 
                ShowMessageAtIndex(currentIndex);
                firstMessageRead=true;
                int randomAudioIndex = UnityEngine.Random.Range(0, glitchClips.Length);
                audioSource.PlayOneShot(glitchClips[randomAudioIndex]);
            }
            else
            {
                DialogueWriter.instance.ShowDialogue();
                int randomAudioIndex = UnityEngine.Random.Range(0, glitchClips.Length);
                audioSource.PlayOneShot(glitchClips[randomAudioIndex]);
                ShowNextMessage();
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        playerInZone = true;
        ShowHint();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player") return;
        firstMessageRead = false;
        playerInZone = false;
        HideHint();
        CamManager.current.UnfocusCamera();
    }

    void ShowHint()
    {
        screen.transform.DOMove(originalTransform.position, animationSpeed).SetEase(InEase);
        screen.transform.DOScaleX(0.1f, animationSpeed).SetEase(InEase);
        screen.transform.DOScaleY(0.1f, animationSpeed).SetEase(InEase);
        screen.transform.DOScaleZ(0.061f, animationSpeed) .SetEase(InEase);

        screenLight.SetActive(true);
    }

    void HideHint()
    {
        screen.transform.DOMove(hidingTransform.position, animationSpeed).SetEase(OutEase);
        screen.transform.DOScaleX(0f, animationSpeed).SetEase(OutEase);
        screen.transform.DOScaleY(0f, animationSpeed).SetEase(OutEase);
        screen.transform.DOScaleZ(0f, animationSpeed).SetEase(OutEase);
        screenLight.SetActive(false);
    }

    

    void ShowMessageAtIndex(int index)
    {
        if (index >= 0 && index < dialogue.Length)
        {
            dialogueText.text = dialogue[index];
        }
    }

    void ShowNextMessage()
    {
        
        currentIndex++;
        ShowMessageAtIndex(currentIndex);

        
        if (currentIndex >= dialogue.Length)
        {
            EndDialog();
        }
    }

    void EndDialog()
    {
        currentIndex = 0;
        DialogueWriter.instance.HideDialogue();
        HideHint();
        playerInZone = false;
        CamManager.current.UnfocusCamera();
    }
}
