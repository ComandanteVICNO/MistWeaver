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

    public Image transitionImage;

    public GameObject player;

    Color32 blackColor = new Color32(0,0,0,255);
    Color32 noColor = new Color32(0, 0, 0, 0);

    public string sceneToTransition;

    public float colorFadeTime;
    public float timeWhileFaded;




    // Update is called once per frame
    void Update()
    {
        movController = FindAnyObjectByType<PlayerMovController>();
        playerTrasform = movController.transform;
        rb = movController.rb;
        
        
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

        
        transitionImage.DOColor(blackColor, colorFadeTime).SetEase(Ease.Linear);
        

        yield return new WaitForSecondsRealtime(colorFadeTime);

        SceneManager.LoadScene(sceneToTransition);  

        

    }

}
