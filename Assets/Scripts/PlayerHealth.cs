using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float colorChangeDuration;
    public SpriteRenderer playerSprite;
    public Color32 damageColor;
    public Color32 originalColor;
    public RectTransform playerHealthBar;
    public RectTransform playerHealthBackgroundBar;
    public float barBackgroundWaitTime;
    public float barBackgroundAnimationTime;
    float currentBarValue;

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip hurtSound;

    [Header("Animation")]
    public Animator spriteAnimator;
    public AnimationClip damagedClip;
    Coroutine damageAnimationCoroutine;
    

    void Start()
    {
        currentHealth = maxHealth;
        originalColor = playerSprite.color;
        damageColor = Color.red;

    }

    // Update is called once per frame
    void Update()
    {
        ChangeColor();
        UpdateHealth();

        
    }

    public void TakeDamage(float damage)
    {
        if (DeathHandler.instance.isDead) return;
        if(!DeathHandler.instance.canBeAttacked) return;
        currentHealth -= damage;
        StartCoroutine(UpdateHealth());
        StartCoroutine(ChangeColor());

        DamagedAnimation();

        audioSource.PlayOneShot(hurtSound);

        if (currentHealth <= 0) 
        {
            //Destroy(gameObject);
            Debug.Log("Player Dead");
        }
    }

    IEnumerator ChangeColor()
    {
        playerSprite.color = damageColor;

        yield return new WaitForSeconds(colorChangeDuration);

        playerSprite.color = originalColor;

    }


    public IEnumerator UpdateHealth()
    {
        currentBarValue = (currentHealth * 1) / maxHealth;

        playerHealthBar.localScale = new Vector3(currentBarValue, 1, 1);
        
        yield return new WaitForSeconds(barBackgroundWaitTime);

        playerHealthBackgroundBar.DOScale(new Vector3(currentBarValue, 1, 1), barBackgroundAnimationTime).SetEase(Ease.Linear);
    }

    public void HealthPickup(float health)
    {
        currentHealth += health;
        StartCoroutine(UpdateHealth());
        
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void DamagedAnimation()
    {
        if (!PlayerMovController.instance.isGrounded) return;
        if (damageAnimationCoroutine != null) return;
        StartCoroutine(DoDamageAnimation());

        Debug.Log("was damaged");
    }

    IEnumerator DoDamageAnimation()
    {
        spriteAnimator.SetBool("wasAttacked", true);

        PlayerMovController.instance.rb.velocity = Vector3.zero;

        PlayerMovController.instance.canMove = false;
        PlayerMovController.instance.canDash = false;
        PlayerMovController.instance.canJump = false;

        Debug.Log("C started");
        yield return new WaitForSecondsRealtime(damagedClip.length);
        Debug.Log("C ended");

        spriteAnimator.SetBool("wasAttacked", false);

        PlayerMovController.instance.canMove = true;
        PlayerMovController.instance.canDash = true;
        PlayerMovController.instance.canJump = true;
        damageAnimationCoroutine = null;
    }
    
}
