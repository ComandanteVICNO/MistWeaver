using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    [Header("UI References")]
    public GameObject tutorialUI;
    public TMP_Text tutorialText;

    [Header("Tutorial Strings")]
    [Multiline]public string initialString;
    [Multiline] public string moveTutorialText;
    [Multiline] public string jumpTutorialText;
    [Multiline] public string dashTutorialText;
    [Multiline] public string attackTutorialText;
    [Multiline] public string interactTutorialText;

    [Header("Confirmation Bools")]
    public bool canStart = false;
    public bool initialBool = false;
    public bool moveTutorialBool = false;
    public bool jumpTutorialBool = false;
    public bool dashTutorialBool = false;
    public bool attackTutorialBool = false;
    public bool interactTutorialBool = false;
    public bool debugBool = false;

    [Header("UI Fade")]
    public Image transitionImage;
    Color32 noColor = new Color32(0, 0, 0, 0);
    public float colorFadeTime;

    [Header("Sound")]
    public AudioSource musicSource;

    [Header("UI Refs")]
    public GameObject[] uiRefs;

    Coroutine initialCoroutine;

    private void Awake()
    {
        
    }
    void Start()
    {
        PlayerMovController.instance.canMove = false;
        PlayerMovController.instance.canDash = false;
        PlayerMovController.instance.canJump = false;
        PlayerAttack.current.isAttackAllow = false;
        musicSource.Pause();
        StartCoroutine(DoTransition());
        HideUI();
    }

    // Update is called once per frame
    void Update()
    {
        InitialText();
        MoveTutorial();
        JumpTutorial();
        DashTutorial();
        AttackTutorial();
        InteractTutorial();
        FinishTutorial();
        TutorialDebug();
    }

    public void HideUI()
    {
        foreach (GameObject uiObject in uiRefs)
        {
            uiObject.SetActive(false);
        }
    }

    public void ShowUI()
    {
        foreach (GameObject uiObject in uiRefs)
        {
            uiObject.SetActive(true);
        }
    }


    IEnumerator DoTransition()
    {
        transitionImage.DOColor(noColor, colorFadeTime).SetEase(Ease.Linear);

        yield return new WaitForSecondsRealtime(colorFadeTime);

        canStart = true;
        musicSource.Play();
        ShowUI();
     
    }

    public void InitialText()
    {
        if (!canStart) return;
        if (initialBool) return;
        tutorialText.text = initialString;
        if (initialCoroutine != null) return;
        initialCoroutine = StartCoroutine(InitialTextTiming());
    }

    IEnumerator InitialTextTiming()
    {
        yield return new WaitForSecondsRealtime(4f);
        initialBool = true;
    }

    public void MoveTutorial()
    {
        if (moveTutorialBool) return;
        if (!initialBool) return;
        tutorialText.text = moveTutorialText;
        PlayerMovController.instance.canMove = true;
        if (UserInput.instance.controls.Player.Move.WasPressedThisFrame())
        {
            
            moveTutorialBool = true;
        }

    }

    public void JumpTutorial()
    {
        if (jumpTutorialBool) return;
        if (!moveTutorialBool) return;
        tutorialText.text = jumpTutorialText;
        PlayerMovController.instance.canJump = true;
        if (UserInput.instance.controls.Player.Jump.WasPressedThisFrame())
        {
            
            jumpTutorialBool = true;
        }
    }

    public void DashTutorial()
    {
        if (dashTutorialBool) return;
        if (!jumpTutorialBool) return;
        tutorialText.text = dashTutorialText;
        PlayerMovController.instance.canDash = true;
        if (UserInput.instance.controls.Player.Dash.WasPressedThisFrame())
        {
            dashTutorialBool = true;
        }
    }

    public void AttackTutorial()
    {
        if (attackTutorialBool) return;
        if (!dashTutorialBool) return;
        tutorialText.text = attackTutorialText;
        PlayerAttack.current.canAttack = true;
        PlayerAttack.current.isAttackAllow = true;
        if(UserInput.instance.controls.Player.MainAttack.WasPressedThisFrame() || UserInput.instance.controls.Player.StunAttack.WasPressedThisFrame())
        {
            attackTutorialBool = true;
        }
    }

    public void InteractTutorial()
    {
        if (interactTutorialBool) return;
        if (!attackTutorialBool) return;
        tutorialText.text = interactTutorialText;

        if (UserInput.instance.controls.Player.Interact.WasPressedThisFrame())
        {
            interactTutorialBool = true;
            
        }

    }

    public void FinishTutorial()
    {
        if (!interactTutorialBool) return;

        tutorialUI.SetActive(false);

    }

    void TutorialDebug()
    {
        if (debugBool)
        {
            initialBool = true;
            moveTutorialBool = true;
            jumpTutorialBool = true;
            dashTutorialBool = true;
            attackTutorialBool = true;
            interactTutorialBool = true;
        }
    }
}
