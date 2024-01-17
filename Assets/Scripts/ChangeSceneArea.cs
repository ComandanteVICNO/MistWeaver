using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;
using TMPro;
using UnityEngine.SceneManagement;

public class ChangeSceneArea : MonoBehaviour
{
    public Transform playerTrasform;
    public Rigidbody rb;
    public float waitTime;
    private PlayerMovController movController;

    public float speed;

    public Image transitionImage;

    public GameObject player;

    Color32 blackColor = new Color32(0,0,0,255);
    Color32 noColor = new Color32(0, 0, 0, 0);

    public string sceneToTransition;

    public float colorFadeTime;
    public float timeWhileFaded;

    bool playerInTheArea = false;
    public Animator animator;

    private void Start()
    {
        animator.SetBool("isWalking", false);
    }
    // Update is called once per frame
    void Update()
    {
        movController = FindAnyObjectByType<PlayerMovController>();
        playerTrasform = movController.transform;
        rb = movController.rb;

        MovePlayer();
    }

    public void CancelMovement()
    {
        movController.canMove = false;
        movController.canJump = false;
        movController.canDash = false;

        

        rb.velocity = Vector3.zero;
    }

    public void AllowMovement()
    {
        movController.canMove = true;
        movController.canJump = true;

    }





    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        {
            
            StartCoroutine(StartTransition());
        }
    }

    IEnumerator StartTransition()
    {
        CancelMovement();
        CamManager.current.FocusOnTransitionCam();
        playerInTheArea = true;
        animator.SetBool("isWalking", true);
        transitionImage.DOColor(blackColor, colorFadeTime).SetEase(Ease.Linear);
        

        yield return new WaitForSecondsRealtime(colorFadeTime);

        SceneManager.LoadScene(sceneToTransition);  

        

    }

    void MovePlayer()
    {
        if (playerInTheArea)
        {
            Vector3 movement = Vector3.right * speed;
            rb.AddForce(movement);
        }
    }

}
