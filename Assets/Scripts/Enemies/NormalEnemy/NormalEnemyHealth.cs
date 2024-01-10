using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class NormalEnemyHealth : MonoBehaviour
{
    public GameObject enemyObject;
    public float maxHealth;
    public float currentHealth;
    public float colorChangeDuration;
    public float knockBackForce;
    public SpriteRenderer enemySprite;
    public Color32 damageColor;
    public Color32 stunnedColor;
    public Color32 originalColor;
    private Rigidbody rb;
    public bool wasAttacked;
    public bool isStunned;

    [Header("Death Stuff")]
    public Animator stateAnimator;
    public Animator spriteAnimator;
    public AnimationClip deathClip;
    public GameObject attackPoint;

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip hurtSound;

    [Header("VFX")]
    public VisualEffect Hit;
    public float vfxTimeUntilDie;
    VisualEffect vfx;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        originalColor = enemySprite.color;
        damageColor = Color.red;
        stunnedColor = Color.black;
        wasAttacked = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        wasAttacked = true;

        if (currentHealth <= 0) 
        {
            SpawnVFX();
            StartCoroutine(Death());
        }
        else
        {
            StartCoroutine(ChangeColor(damageColor));
            audioSource.PlayOneShot(hurtSound);
            SpawnVFX();
        }
    }

    public void TakeStunnedDamage(float damage)
    {
        currentHealth -= damage;
        isStunned = true;
        wasAttacked = true;

        if (currentHealth <= 0)
        {
            StartCoroutine(Death());
            SpawnVFX();
        }
        else
        {
            StartCoroutine(ChangeColor(stunnedColor));
            audioSource.PlayOneShot(hurtSound);
            SpawnVFX();
        }
    }

    IEnumerator Death()
    {

        spriteAnimator.SetBool("isAttacking", false);
        spriteAnimator.SetBool("isPatroling", false);
        spriteAnimator.SetBool("isChasing", false);
        spriteAnimator.SetBool("isDead", true);

        stateAnimator.SetBool("canChase", false);
        stateAnimator.SetBool("canAttack", false) ;
        stateAnimator.SetBool("wasAttacked", false);
        stateAnimator.SetBool("isDead", true) ;


        yield return new WaitForSecondsRealtime(deathClip.length);

        Destroy(enemyObject);

    }

    IEnumerator ChangeColor(Color32 Color)
    {
        enemySprite.color = Color;

        yield return new WaitForSeconds(colorChangeDuration);

        enemySprite.color = originalColor;

    }
    void SpawnVFX()
    {
        vfx = Instantiate(Hit);
        vfx.transform.position = transform.position;
        vfx.Play();
        Destroy(vfx.gameObject, vfxTimeUntilDie);
    }

}
